// =====================================================================================================
// Task 11 - Books: an abstract base class, three concrete types and a factory (from task 07)
// -----------------------------------------------------------------------------------------------------
// From task 06. What changed: Lend() throws BookNotAvailableException instead of returning false.
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    // abstract: there is no plain "Book", only print, ebook and audio books. You cannot write new Book().
    public abstract class Book
    {
        public int Id { get; }
        public string Title { get; }
        public string Author { get; }
        public int Year { get; }
        public string Genre { get; }
        public int Copies { get; }
        public int Length { get; }   // pages for print and ebook, minutes for audio

        private int onLoan;

        // protected: only the subclasses can call this constructor
        protected Book(int id, string title, string author, int year, string genre, int copies, int length)
        {
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
            if (length <= 0)
            {
                throw new ArgumentException("length must be positive");
            }
            Id = id;
            Title = title;
            Author = author;
            Year = year;
            Genre = genre;
            Copies = copies;
            Length = length;
        }

        // Every subclass must say what it is and how long it is, in its own way
        public abstract string FormatName { get; }

        public abstract string LengthText();

        // Written once, here. Polymorphism: the same call works for every book type, and each type
        // fills in its own FormatName and LengthText().
        public string Describe()
        {
            return $"#{Id} {Title}: {FormatName}, {LengthText()}";
        }

        public int Available => Copies - onLoan;

        public void Lend()
        {
            if (Available == 0)
            {
                throw new BookNotAvailableException(Title);
            }
            onLoan++;
        }

        public void TakeBack()
        {
            onLoan--;
        }

        public override string ToString()
        {
            return $"#{Id} {Title} by {Author} ({Year}), {Available} of {Copies} available";
        }

        public override bool Equals(object? obj)
        {
            return obj is Book other && other.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class PrintBook : Book
    {
        public PrintBook(int id, string title, string author, int year, string genre, int copies, int length)
            : base(id, title, author, year, genre, copies, length)
        {
        }

        public override string FormatName => "print";

        public override string LengthText() => $"{Length} pages";
    }

    public class EBook : Book
    {
        public EBook(int id, string title, string author, int year, string genre, int copies, int length)
            : base(id, title, author, year, genre, copies, length)
        {
        }

        public override string FormatName => "ebook";

        public override string LengthText() => $"{Length} pages";
    }

    public class AudioBook : Book
    {
        public AudioBook(int id, string title, string author, int year, string genre, int copies, int length)
            : base(id, title, author, year, genre, copies, length)
        {
        }

        public override string FormatName => "audio";

        // Length is in minutes: 1150 -> 19h 10m
        public override string LengthText() => $"{Length / 60}h {Length % 60:D2}m";
    }

    public static class BookFactory
    {
        // The factory: the format column decides which class to build. The fields are the eight
        // columns of books.csv: id,title,author,year,genre,copies,format,length.
        public static Book MakeBook(string[] fields)
        {
            string format = fields[6];
            if (format != "print" && format != "ebook" && format != "audio")
            {
                throw new ArgumentException($"format must be print, ebook or audio, got '{format}'");
            }
            int id = int.Parse(fields[0]);
            int year = int.Parse(fields[3]);
            int copies = int.Parse(fields[5]);
            int length = int.Parse(fields[7]);
            return format switch
            {
                "print" => new PrintBook(id, fields[1], fields[2], year, fields[4], copies, length),
                "ebook" => new EBook(id, fields[1], fields[2], year, fields[4], copies, length),
                _ => new AudioBook(id, fields[1], fields[2], year, fields[4], copies, length),
            };
        }
    }
}
