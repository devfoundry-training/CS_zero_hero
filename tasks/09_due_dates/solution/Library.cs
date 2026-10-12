// =====================================================================================================
// Task 09 - Library: loans with dates
// -----------------------------------------------------------------------------------------------------
// What changed from task 08:
//   * the library keeps every Loan (Loan.cs), open or returned
//   * Borrow() and ReturnBook() take the date as a parameter, so nothing here ever reads the real
//     clock (and the tests in task 11 stay stable)
//   * due dates, days late and fines (in whole pence)
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    public class Library
    {
        private readonly Dictionary<int, Book> books = new Dictionary<int, Book>();
        private readonly Dictionary<int, Member> members = new Dictionary<int, Member>();
        private readonly Dictionary<int, Loan> loans = new Dictionary<int, Loan>();

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
            return loan;
        }
    }
}
