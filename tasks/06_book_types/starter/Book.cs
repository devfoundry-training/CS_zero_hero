// =====================================================================================================
// Task 06 - Book (starter: the task 05 solution)
// -----------------------------------------------------------------------------------------------------
// TODO (Core 1):
//   * make the class abstract and the constructor protected
//   * add Length (an int, pages or minutes) to the constructor: "length must be positive"
//   * add public abstract string FormatName { get; } and public abstract string LengthText();
//   * add Describe(), written once here: #ID TITLE: FORMAT, LENGTH TEXT
// Then write PrintBook, EBook and AudioBook in BookTypes.cs.
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    // IComparable<Book> gives books a natural order (Stretch 2), so a List<Book> can Sort() itself.
    public class Book : IComparable<Book>
    {
        // Get-only properties can only be set in the constructor: from outside they are read-only.
        public int Id { get; }
        public string Title { get; }
        public string Author { get; }
        public int Year { get; }
        public string Genre { get; }
        public int Copies { get; }

        // Private: only Lend() and TakeBack() can change it, so it can never go below 0 or above Copies
        private int onLoan;

        public Book(int id, string title, string author, int year, string genre, int copies)
        {
            // Validation in the constructor: a Book that exists is a valid Book.
            // ArgumentException(message) with no parameter name, so the message stays exactly as written.
            if (id <= 0)
            {
                throw new ArgumentException("book id must be positive");
            }
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("title must not be blank");
            }
            if (year < 1450 || year > 2100)
            {
                throw new ArgumentException("year must be between 1450 and 2100");
            }
            if (copies < 0)
            {
                throw new ArgumentException("copies must not be negative");
            }
            Id = id;
            Title = title;
            Author = author;
            Year = year;
            Genre = genre;
            Copies = copies;
        }

        // A computed property: worked out every time it is read, never stored
        public int Available => Copies - onLoan;

        public bool Lend()
        {
            if (Available == 0)
            {
                return false;
            }
            onLoan++;
            return true;
        }

        public bool TakeBack()
        {
            if (onLoan == 0)
            {
                return false;
            }
            onLoan--;
            return true;
        }

        public override string ToString()
        {
            return $"#{Id} {Title} by {Author} ({Year}), {Available} of {Copies} available";
        }

        // =============================================================================================
        // STRETCH 1: IDENTITY IS THE ID
        // ---------------------------------------------------------------------------------------------
        // Override Equals and GetHashCode together, or a Dictionary or HashSet will get confused.
        // ---------------------------------------------------------------------------------------------

        public override bool Equals(object? obj)
        {
            return obj is Book other && other.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        // =============================================================================================
        // STRETCH 2: NATURAL ORDER IS TITLE A-Z (IGNORING CASE), THEN YEAR
        // =============================================================================================

        public int CompareTo(Book? other)
        {
            if (other is null)
            {
                return 1;
            }
            int byTitle = StringComparer.OrdinalIgnoreCase.Compare(Title, other.Title);
            return byTitle != 0 ? byTitle : Year.CompareTo(other.Year);
        }
    }
}
