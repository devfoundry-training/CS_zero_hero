// =====================================================================================================
// Task 12 - Library: the task 09 model plus reservations (from task 11)
// -----------------------------------------------------------------------------------------------------
// Reservations were written test first (see LibraryTests.cs):
//   * a member can reserve a book only when no copy is free
//   * the queue for each book is first come, first served
//   * a returned copy is held for the first member in the queue; nobody else can borrow it, and they
//     can
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    public class Library
    {
        private readonly Dictionary<int, Book> books = new Dictionary<int, Book>();
        private readonly Dictionary<int, Member> members = new Dictionary<int, Member>();
        private readonly Dictionary<int, Loan> loans = new Dictionary<int, Loan>();
        private readonly Dictionary<int, List<int>> queues = new Dictionary<int, List<int>>();   // book id -> member ids waiting
        private readonly Dictionary<int, List<int>> holds = new Dictionary<int, List<int>>();    // book id -> member ids with a copy held

        // =============================================================================================
        // ADDING AND FINDING
        // =============================================================================================

        public void AddBook(Book book)
        {
            books[book.Id] = book;
        }

        public void AddMember(Member member)
        {
            members[member.Id] = member;
        }

        // Add a loan from the records. An open loan takes a copy off the shelf; limits are not checked
        // (history is what it is).
        public void AddLoan(Loan loan)
        {
            loans[loan.Id] = loan;
            if (loan.IsOpen)
            {
                GetBook(loan.BookId).Lend();
                GetMember(loan.MemberId).Loans.Add(loan.BookId);
            }
        }

        public int BookCount => books.Count;

        public int MemberCount => members.Count;

        public int LoanCount => loans.Count;

        public List<Book> AllBooks()
        {
            return books.Values.OrderBy(book => book.Id).ToList();
        }

        public List<Member> AllMembers()
        {
            return members.Values.OrderBy(member => member.Id).ToList();
        }

        public List<Loan> AllLoans()
        {
            return loans.Values.OrderBy(loan => loan.Id).ToList();
        }

        public Book? FindById(int bookId)
        {
            return books.TryGetValue(bookId, out Book? book) ? book : null;
        }

        public Member? FindMember(int memberId)
        {
            return members.TryGetValue(memberId, out Member? member) ? member : null;
        }

        public List<Book> FindByTitle(string text)
        {
            return AllBooks().Where(book => book.Title.Contains(text, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public Book GetBook(int bookId)
        {
            return FindById(bookId) ?? throw new UnknownBookException(bookId);
        }

        public Member GetMember(int memberId)
        {
            return FindMember(memberId) ?? throw new UnknownMemberException(memberId);
        }

        // =============================================================================================
        // DATES AND MONEY
        // =============================================================================================

        public DateOnly DueDate(Loan loan)
        {
            return loan.Borrowed.AddDays(GetMember(loan.MemberId).Policy.LoanDays());
        }

        // Whole days past the due date: on the return date if the book is back, otherwise today.
        // Never negative.
        public int DaysLate(Loan loan, DateOnly today)
        {
            DateOnly end = loan.Returned ?? today;   // ?? : the returned date if there is one, else today
            return Math.Max(0, end.DayNumber - DueDate(loan).DayNumber);
        }

        // The fine in pence.
        public int Fine(Loan loan, DateOnly today)
        {
            return GetMember(loan.MemberId).Policy.FineFor(DaysLate(loan, today));
        }

        public string Status(Loan loan, DateOnly today)
        {
            DateOnly due = DueDate(loan);
            if (loan.Returned != null)
            {
                int late = DaysLate(loan, today);
                return late == 0 ? "returned on time" : $"returned {Formatting.Plural(late, "day", "days")} late";
            }
            if (today < due)
            {
                return $"on loan, {Formatting.Plural(due.DayNumber - today.DayNumber, "day", "days")} left";
            }
            if (today == due)
            {
                return "due today";
            }
            return $"overdue by {Formatting.Plural(today.DayNumber - due.DayNumber, "day", "days")}";
        }

        // Open loans past their due date: most days late first, then by id.
        public List<Loan> Overdue(DateOnly today)
        {
            return loans.Values
                .Where(loan => loan.IsOpen && DaysLate(loan, today) > 0)
                .OrderByDescending(loan => DaysLate(loan, today))
                .ThenBy(loan => loan.Id)
                .ToList();
        }

        // =============================================================================================
        // THE RULES
        // =============================================================================================

        public Loan Borrow(int memberId, int bookId, DateOnly today)
        {
            // The order of the checks is part of the brief
            Member member = GetMember(memberId);
            Book book = GetBook(bookId);
            int limit = member.Policy.MaxLoans();
            if (member.Loans.Count >= limit)
            {
                throw new MemberLimitReachedException(member.Name, limit);
            }
            List<int> held = ListFor(holds, bookId);
            if (held.Contains(memberId))
            {
                held.Remove(memberId);   // the copy held for this member
            }
            else if (book.Available - held.Count <= 0)
            {
                throw new BookNotAvailableException(book.Title);   // every free copy is held for someone else
            }
            book.Lend();   // throws BookNotAvailableException
            member.Loans.Add(bookId);
            int newId = (loans.Count == 0 ? 0 : loans.Keys.Max()) + 1;
            Loan loan = new Loan(newId, bookId, memberId, today);
            loans[loan.Id] = loan;
            return loan;
        }

        public Loan ReturnBook(int memberId, int bookId, DateOnly today)
        {
            Member member = GetMember(memberId);
            Book book = GetBook(bookId);
            Loan? loan = AllLoans().FirstOrDefault(x => x.IsOpen && x.MemberId == memberId && x.BookId == bookId);
            if (loan == null)
            {
                throw new NotOnLoanException(book.Title, member.Name);
            }
            loan.Returned = today;
            member.Loans.Remove(bookId);
            book.TakeBack();
            List<int> queue = ListFor(queues, bookId);
            if (queue.Count > 0)
            {
                // The copy that came back is held for the first member in the queue
                ListFor(holds, bookId).Add(queue[0]);
                queue.RemoveAt(0);
            }
            return loan;
        }

        // =============================================================================================
        // RESERVATIONS (TASK 11, WRITTEN TEST FIRST)
        // =============================================================================================

        // The list for a book id, made empty the first time it is asked for.
        private static List<int> ListFor(Dictionary<int, List<int>> lists, int bookId)
        {
            if (!lists.TryGetValue(bookId, out List<int>? list))
            {
                list = new List<int>();
                lists[bookId] = list;
            }
            return list;
        }

        // Copies on the shelf that are not held for anyone.
        public int FreeCopies(int bookId)
        {
            return GetBook(bookId).Available - ListFor(holds, bookId).Count;
        }

        // Join the queue for a book. Returns the place in the queue (1 is next).
        public int Reserve(int memberId, int bookId)
        {
            Member member = GetMember(memberId);
            Book book = GetBook(bookId);
            if (FreeCopies(bookId) > 0)
            {
                throw new ReservationNotNeededException(book.Title);
            }
            List<int> queue = ListFor(queues, bookId);
            if (queue.Contains(memberId))
            {
                throw new AlreadyReservedException(member.Name, book.Title);
            }
            queue.Add(memberId);
            return queue.Count;
        }

        // Copies of the lists, so callers can look but not change them
        public List<int> QueueFor(int bookId)
        {
            return new List<int>(ListFor(queues, bookId));
        }

        public List<int> HeldFor(int bookId)
        {
            return new List<int>(ListFor(holds, bookId));
        }
    }
}
