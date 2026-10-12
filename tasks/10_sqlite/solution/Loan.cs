// =====================================================================================================
// Task 10 - Loan: who borrowed what, when, and when it came back (from task 09)
// -----------------------------------------------------------------------------------------------------
// DateOnly is a calendar day with no time of day and no time zone, which is exactly what a library
// loan needs. AddDays moves across month and year ends (and leap days) correctly, and DayNumber turns a
// date into a whole number of days, so subtracting two of them gives the days in between.
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    public class Loan
    {
        public int Id { get; }
        public int BookId { get; }
        public int MemberId { get; }
        public DateOnly Borrowed { get; }

        // DateOnly? can hold a date or null: null while the book is still out
        public DateOnly? Returned { get; set; }

        public Loan(int id, int bookId, int memberId, DateOnly borrowed, DateOnly? returned = null)
        {
            Id = id;
            BookId = bookId;
            MemberId = memberId;
            Borrowed = borrowed;
            Returned = returned;
        }

        public bool IsOpen => Returned == null;
    }
}
