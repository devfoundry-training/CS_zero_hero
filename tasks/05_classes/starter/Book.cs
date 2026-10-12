// =====================================================================================================
// Task 05 - Book (starter)
// -----------------------------------------------------------------------------------------------------
// Replaces the task 04 record: a Book that checks its own data and knows how many copies are on loan.
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    public class Book
    {
        // Get-only properties: set in the constructor, read-only from outside
        public int Id { get; }
        public string Title { get; }
        public string Author { get; }
        public int Year { get; }
        public string Genre { get; }
        public int Copies { get; }

        // TODO: a private int field for the copies on loan. Private: only Lend() and TakeBack() may
        // change it.

        // TODO (Core 3): check the values and throw new ArgumentException(message) for a bad one, with
        // exactly the messages in the brief, then set the properties.
        public Book(int id, string title, string author, int year, string genre, int copies)
        {
            throw new NotImplementedException("Core 1 and 3: the Book constructor");
        }

        // TODO: copies minus the copies on loan
        public int Available => throw new NotImplementedException("Core 1: Book.Available");

        // TODO: lend one copy if one is available; true if it worked
        public bool Lend()
        {
            throw new NotImplementedException("Core 1: Book.Lend");
        }

        // TODO: take one copy back if any are on loan; true if it worked
        public bool TakeBack()
        {
            throw new NotImplementedException("Core 1: Book.TakeBack");
        }

        // TODO: #ID TITLE by AUTHOR (YEAR), AVAILABLE of COPIES available
        public override string ToString()
        {
            throw new NotImplementedException("Core 1: Book.ToString");
        }
    }
}
