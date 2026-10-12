// =====================================================================================================
// Task 05 - Library (starter)
// -----------------------------------------------------------------------------------------------------
// Holds the books and members, keyed by id, and the borrowing rules.
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

        // TODO: the book, or null when there is no such id (TryGetValue helps)
        public Book? FindById(int bookId)
        {
            throw new NotImplementedException("Core 4: Library.FindById");
        }

        public Member? FindMember(int memberId)
        {
            throw new NotImplementedException("Core 4: Library.FindMember");
        }

        // TODO: the books whose title contains the text, ignoring case, by id
        public List<Book> FindByTitle(string text)
        {
            throw new NotImplementedException("Core 4: Library.FindByTitle");
        }

        // TODO: false if the member or the book is unknown, or no copy is available
        public bool Borrow(int memberId, int bookId)
        {
            throw new NotImplementedException("Core 4: Library.Borrow");
        }

        // TODO: false if the member or the book is unknown, or the member does not have that book
        public bool ReturnBook(int memberId, int bookId)
        {
            throw new NotImplementedException("Core 4: Library.ReturnBook");
        }
    }
}
