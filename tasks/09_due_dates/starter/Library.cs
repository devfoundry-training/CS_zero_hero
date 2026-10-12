// =====================================================================================================
// Task 09 - Library: the rules, now throwing instead of returning false (from task 07)
// -----------------------------------------------------------------------------------------------------
// What changed from task 06:
//   * Borrow() and ReturnBook() throw a LibraryException subclass instead of returning false, so the
//     caller learns WHY
//   * GetBook() and GetMember() are like FindById() and FindMember(), but a missing id is an error
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    public class Library
    {
        private readonly Dictionary<int, Book> books = new Dictionary<int, Book>();
        private readonly Dictionary<int, Member> members = new Dictionary<int, Member>();

        public void AddBook(Book book)
        {
            books[book.Id] = book;
        }

        public void AddMember(Member member)
        {
            members[member.Id] = member;
        }

        public int BookCount => books.Count;

        public int MemberCount => members.Count;

        public List<Book> AllBooks()
        {
            return books.Values.OrderBy(book => book.Id).ToList();
        }

        public List<Member> AllMembers()
        {
            return members.Values.OrderBy(member => member.Id).ToList();
        }

        public Book? FindById(int bookId)
        {
            return books.TryGetValue(bookId, out Book? book) ? book : null;
        }

        public Member? FindMember(int memberId)
        {
            return members.TryGetValue(memberId, out Member? member) ? member : null;
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
        // TODO (TASK 09): LOANS AND DATES
        // ---------------------------------------------------------------------------------------------
        // Keep every Loan in a Dictionary<int, Loan> next to the books and members, then fill these in.
        // ---------------------------------------------------------------------------------------------

        // Add a loan from loans.csv. An open loan takes a copy off the shelf and adds the book to the
        // member's loans; limits are not checked (history is what it is).
        public void AddLoan(Loan loan)
        {
            throw new NotImplementedException("Core 2: Library.AddLoan");
        }

        // Every loan, by id.
        public List<Loan> AllLoans()
        {
            throw new NotImplementedException("Core 6: Library.AllLoans");
        }

        // Borrowed plus the member's policy loan days.
        public DateOnly DueDate(Loan loan)
        {
            throw new NotImplementedException("Core 3: Library.DueDate");
        }

        // Returned minus due for a returned loan, today minus due for an open one; never below 0.
        public int DaysLate(Loan loan, DateOnly today)
        {
            throw new NotImplementedException("Core 4: Library.DaysLate");
        }

        // Days late times the policy's fine per day, in pence.
        public int Fine(Loan loan, DateOnly today)
        {
            throw new NotImplementedException("Core 5: Library.Fine");
        }

        // "returned on time", "returned N days late", "on loan, N days left", "due today" or
        // "overdue by N days".
        public string Status(Loan loan, DateOnly today)
        {
            throw new NotImplementedException("Core 6: Library.Status");
        }

        // Open loans past their due date: most days late first, then by id.
        public List<Loan> Overdue(DateOnly today)
        {
            throw new NotImplementedException("Core 6: Library.Overdue");
        }

        // TODO (Core 7): give Borrow() and ReturnBook() a DateOnly today parameter and make them return
        // the Loan. A new loan's id is the highest loan id plus 1; returning sets the loan's Returned date.
        public (Member Member, Book Book) Borrow(int memberId, int bookId)
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
            return (member, book);
        }

        public (Member Member, Book Book) ReturnBook(int memberId, int bookId)
        {
            Member member = GetMember(memberId);
            Book book = GetBook(bookId);
            if (!member.Loans.Contains(bookId))
            {
                throw new NotOnLoanException(book.Title, member.Name);
            }
            member.Loans.Remove(bookId);
            book.TakeBack();
            return (member, book);
        }
    }
}
