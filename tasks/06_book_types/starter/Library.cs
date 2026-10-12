// =====================================================================================================
// Task 06 - Library (starter: the task 05 solution)
// -----------------------------------------------------------------------------------------------------
// TODO (Core 5): Borrow() must also fail when the member already has Policy.MaxLoans() books.
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    public class Library
    {
        // Keyed by id: finding a book is one lookup, not a search through a list
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

        // The book, or null when there is no such id ("not found" in C#).
        public Book? FindById(int bookId)
        {
            return books.TryGetValue(bookId, out Book? book) ? book : null;
        }

        public Member? FindMember(int memberId)
        {
            return members.TryGetValue(memberId, out Member? member) ? member : null;
        }

        // Books whose title contains the text, ignoring case, by id.
        public List<Book> FindByTitle(string text)
        {
            List<Book> found = new List<Book>();
            foreach (Book book in AllBooks())
            {
                if (book.Title.Contains(text, StringComparison.OrdinalIgnoreCase))
                {
                    found.Add(book);
                }
            }
            return found;
        }

        public bool Borrow(int memberId, int bookId)
        {
            Member? member = FindMember(memberId);
            Book? book = FindById(bookId);
            if (member == null || book == null)
            {
                return false;
            }
            if (!book.Lend())
            {
                return false;
            }
            member.Loans.Add(bookId);
            return true;
        }

        public bool ReturnBook(int memberId, int bookId)
        {
            Member? member = FindMember(memberId);
            Book? book = FindById(bookId);
            if (member == null || book == null || !member.Loans.Contains(bookId))
            {
                return false;
            }
            member.Loans.Remove(bookId);
            book.TakeBack();
            return true;
        }

        // Every book, by id.
        public List<Book> AllBooks()
        {
            List<Book> all = new List<Book>(books.Values);
            all.Sort((a, b) => a.Id.CompareTo(b.Id));
            return all;
        }

        // Stretch 3: books with at least one copy on the shelf.
        public List<Book> AvailableBooks()
        {
            return AllBooks().Where(book => book.Available > 0).ToList();
        }
    }
}
