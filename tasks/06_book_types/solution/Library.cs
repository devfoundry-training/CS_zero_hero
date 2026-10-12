// =====================================================================================================
// Task 06 - Library: the task 05 rules, plus the loan limit from the member's policy
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

        public Book? FindById(int bookId)
        {
            return books.TryGetValue(bookId, out Book? book) ? book : null;
        }

        public Member? FindMember(int memberId)
        {
            return members.TryGetValue(memberId, out Member? member) ? member : null;
        }

        public List<Book> AllBooks()
        {
            return books.Values.OrderBy(book => book.Id).ToList();
        }

        public bool Borrow(int memberId, int bookId)
        {
            Member? member = FindMember(memberId);
            Book? book = FindById(bookId);
            if (member == null || book == null)
            {
                return false;
            }
            if (!member.CanBorrowMore())   // the policy decides the limit
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
    }
}
