// =====================================================================================================
// Task 07 - Library (starter: the task 06 solution)
// -----------------------------------------------------------------------------------------------------
// TODO (Core 2): Borrow() and ReturnBook() throw the exceptions in Exceptions.cs instead of returning
// false. Borrow checks, in this order: member exists, book exists, member is under the limit, a copy
// is available. ReturnBook checks: member exists, book exists, the member has that book on loan.
// Returning (Member, Book) instead of bool gives the command loop the names it prints.
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
