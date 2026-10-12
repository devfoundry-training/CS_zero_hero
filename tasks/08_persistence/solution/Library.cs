// =====================================================================================================
// Task 08 - Library: the rules, now throwing instead of returning false (from task 07)
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
