// =====================================================================================================
// Task 04 - Dynamic catalogue (starter)
// -----------------------------------------------------------------------------------------------------
// Session 4: collections. The brief is in ../README.md.
//
// Run:    dotnet run --project tasks/04_dynamic_catalogue/starter
// Check:  python tasks/check/check.py 04 -- dotnet run --project tasks/04_dynamic_catalogue/starter
//
// The data and the borrowing script are here already. Fill in each TODO; the program says
// "not implemented" until they are all done.
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    // A record is a small class for data: the compiler writes the constructor and read-only properties
    public record Book(int Id, string Title, string Author, int Year, string Genre, int Copies, string Format, int Length);

    internal class DynamicCatalogue
    {
        // The lines of data/books.csv, header first (reading files comes in task 08)
        static readonly string[] BookLines =
        {
            "id,title,author,year,genre,copies,format,length",
            "1,Pride and Prejudice,Jane Austen,1813,Classic,3,print,432",
            "2,Nineteen Eighty-Four,George Orwell,1949,Dystopian,2,print,328",
            "3,The Hobbit,J. R. R. Tolkien,1937,Fantasy,0,print,310",
            "4,Dune,Frank Herbert,1965,Science Fiction,4,print,412",
            "5,Robinson Crusoe,Daniel Defoe,1719,Adventure,1,print,320",
            "6,The Time Machine,H. G. Wells,1895,Science Fiction,2,ebook,118",
            "7,Animal Farm,George Orwell,1945,Dystopian,5,print,112",
            "8,Emma,Jane Austen,1815,Classic,1,audio,1150",
            "9,The Left Hand of Darkness,Ursula K. Le Guin,1969,Science Fiction,0,print,304",
            "10,Murder on the Orient Express,Agatha Christie,1934,Crime,2,print,256",
            "11,The Murder of Roger Ackroyd,Agatha Christie,1926,Crime,1,ebook,312",
            "12,A Wizard of Earthsea,Ursula K. Le Guin,1968,Fantasy,2,print,183",
            "13,Brave New World,Aldous Huxley,1932,Dystopian,2,audio,480",
            "14,The Martian,Andy Weir,2011,Science Fiction,3,print,369",
            "15,Small Island,Andrea Levy,2004,Historical,1,print,441",
            "16,Wolf Hall,Hilary Mantel,2009,Historical,2,audio,1440",
            "17,The Remains of the Day,Kazuo Ishiguro,1989,Literary,2,print,258",
            "18,Never Let Me Go,Kazuo Ishiguro,2005,Literary,1,ebook,288",
            "19,Frankenstein,Mary Shelley,1818,Gothic,2,print,280",
            "20,The War of the Worlds,H. G. Wells,1898,Science Fiction,3,audio,410",
        };

        // The borrowing script: (action, book id), applied in order
        static readonly (string Action, int BookId)[] Actions =
        {
            ("borrow", 1),
            ("borrow", 1),
            ("borrow", 4),
            ("return", 1),
            ("borrow", 3),
            ("borrow", 7),
            ("return", 9),
            ("borrow", 1),
        };

        static void Main(string[] args)
        {
            try
            {
                List<Book> books = LoadBooks();
                Console.WriteLine($"Loaded {books.Count} books");
                PrintGenres(books);
                PrintAuthors(books);
                RunBorrowing(books);
            }
            catch (NotImplementedException error)
            {
                Console.WriteLine($"not implemented: {error.Message}");
            }
        }

        // Core 1: split each data line (not the header) on commas into a Book and add it to a list.
        static List<Book> LoadBooks()
        {
            throw new NotImplementedException("Core 1: LoadBooks");
        }

        // Core 2: a Dictionary from genre to the titles in that genre, printed A-Z by genre.
        // Sort with StringComparer.OrdinalIgnoreCase.
        static void PrintGenres(List<Book> books)
        {
            throw new NotImplementedException("Core 2: PrintGenres");
        }

        // Core 3: a HashSet of authors, printed A-Z.
        static void PrintAuthors(List<Book> books)
        {
            throw new NotImplementedException("Core 3: PrintAuthors");
        }

        // Core 4 and 5: apply Actions with a Dictionary from book id to copies on loan, then print the
        // counts.
        static void RunBorrowing(List<Book> books)
        {
            throw new NotImplementedException("Core 4: RunBorrowing");
        }
    }
}
