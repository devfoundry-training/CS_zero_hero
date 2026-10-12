// =====================================================================================================
// Task 04 - Dynamic catalogue (solution, Core + Stretch)
// -----------------------------------------------------------------------------------------------------
// Session 4: collections. Builds on task 03: the fixed shelf of 10 becomes a List of all 20 books,
// plus a Dictionary and a HashSet.
//
// Run:  dotnet run --project tasks/04_dynamic_catalogue/solution                 (Core output, checked)
//       dotnet run --project tasks/04_dynamic_catalogue/solution -- --stretch    (Core, then the Stretch)
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    // A record is a small class for data: the compiler writes the constructor, the read-only
    // properties, ToString and value equality for us. Behaviour comes with real classes in task 05.
    public record Book(int Id, string Title, string Author, int Year, string Genre, int Copies, string Format, int Length);

    internal class DynamicCatalogue
    {
        // =============================================================================================
        // THE DATA: THE LINES OF data/books.csv, COPIED IN
        // ---------------------------------------------------------------------------------------------
        // Reading files comes in task 08. Until then the program carries its own copy of the CSV
        // lines, header first.
        // ---------------------------------------------------------------------------------------------

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

        static Book ParseBook(string line)
        {
            string[] f = line.Split(',');
            return new Book(int.Parse(f[0]), f[1], f[2], int.Parse(f[3]), f[4], int.Parse(f[5]), f[6], int.Parse(f[7]));
        }

        static void Main(string[] args)
        {
            // =========================================================================================
            // CORE 1: LOAD EVERY LINE INTO A LIST
            // =========================================================================================

            List<Book> books = new List<Book>();
            for (int i = 1; i < BookLines.Length; i++)   // start at 1 to skip the header row
            {
                books.Add(ParseBook(BookLines[i]));
            }
            Console.WriteLine($"Loaded {books.Count} books");

            // =========================================================================================
            // CORE 2: INDEX BY GENRE (A DICTIONARY OF GENRE TO A LIST OF TITLES)
            // -----------------------------------------------------------------------------------------
            // StringComparer.OrdinalIgnoreCase sorts A-Z character by character, ignoring case. The
            // default comparer is culture-aware, so never rely on it for a fixed order.
            // -----------------------------------------------------------------------------------------

            Dictionary<string, List<string>> byGenre = new Dictionary<string, List<string>>();
            foreach (Book book in books)
            {
                if (!byGenre.TryGetValue(book.Genre, out List<string>? titles))
                {
                    titles = new List<string>();
                    byGenre[book.Genre] = titles;
                }
                titles.Add(book.Title);
            }

            List<string> genres = new List<string>(byGenre.Keys);
            genres.Sort(StringComparer.OrdinalIgnoreCase);
            Console.WriteLine($"Genres ({byGenre.Count})");
            foreach (string genre in genres)
            {
                List<string> titles = byGenre[genre];
                Console.WriteLine($"{genre} ({titles.Count}): {string.Join(", ", titles)}");
            }

            // =========================================================================================
            // CORE 3: UNIQUE AUTHORS (A SET DROPS THE DUPLICATES FOR US)
            // =========================================================================================

            HashSet<string> authors = new HashSet<string>();
            foreach (Book book in books)
            {
                authors.Add(book.Author);   // Add returns false and changes nothing for a duplicate
            }
            List<string> sortedAuthors = new List<string>(authors);
            sortedAuthors.Sort(StringComparer.OrdinalIgnoreCase);
            Console.WriteLine($"Authors ({authors.Count} unique across {books.Count} books)");
            foreach (string author in sortedAuthors)
            {
                Console.WriteLine(author);
            }

            // =========================================================================================
            // CORE 4 AND 5: BORROW AND RETURN, COUNTED IN DICTIONARIES KEYED BY BOOK ID
            // =========================================================================================

            Dictionary<int, Book> catalogue = new Dictionary<int, Book>();
            foreach (Book book in books)
            {
                catalogue[book.Id] = book;
            }
            Dictionary<int, int> onLoan = new Dictionary<int, int>();
            Dictionary<int, int> borrowedCount = new Dictionary<int, int>();
            Dictionary<int, int> returnedCount = new Dictionary<int, int>();
            int borrows = 0;
            int returns = 0;
            int refused = 0;

            Console.WriteLine("Borrowing");
            foreach ((string action, int bookId) in Actions)
            {
                Book book = catalogue[bookId];
                int outNow = onLoan.GetValueOrDefault(bookId, 0);
                if (action == "borrow")
                {
                    if (outNow < book.Copies)
                    {
                        onLoan[bookId] = outNow + 1;
                        borrowedCount[bookId] = borrowedCount.GetValueOrDefault(bookId, 0) + 1;
                        borrows++;
                        Console.WriteLine($"borrow {bookId}: OK, {book.Copies - outNow - 1} left");
                    }
                    else
                    {
                        refused++;
                        Console.WriteLine($"borrow {bookId}: refused, none available");
                    }
                }
                else
                {
                    if (outNow > 0)
                    {
                        onLoan[bookId] = outNow - 1;
                        returnedCount[bookId] = returnedCount.GetValueOrDefault(bookId, 0) + 1;
                        returns++;
                        Console.WriteLine($"return {bookId}: OK, {book.Copies - outNow + 1} left");
                    }
                    else
                    {
                        refused++;
                        Console.WriteLine($"return {bookId}: refused, none on loan");
                    }
                }
            }

            Console.WriteLine("Counts");
            Console.WriteLine($"borrows: {borrows}");
            Console.WriteLine($"returns: {returns}");
            Console.WriteLine($"refused: {refused}");
            List<int> borrowedIds = new List<int>(borrowedCount.Keys);
            borrowedIds.Sort();
            foreach (int bookId in borrowedIds)
            {
                Console.WriteLine($"book {bookId}: borrowed {borrowedCount[bookId]}, returned {returnedCount.GetValueOrDefault(bookId, 0)}");
            }

            // =========================================================================================
            // STRETCH: BUSY AUTHORS, BOOKS PER DECADE, THE BIGGEST GENRE
            // =========================================================================================

            if (args.Contains("--stretch"))
            {
                Console.WriteLine("Stretch");

                Dictionary<string, int> perAuthor = new Dictionary<string, int>();
                foreach (Book book in books)
                {
                    perAuthor[book.Author] = perAuthor.GetValueOrDefault(book.Author, 0) + 1;
                }
                List<string> busy = new List<string>();
                foreach (string author in sortedAuthors)
                {
                    if (perAuthor[author] > 1)
                    {
                        busy.Add(author);
                    }
                }
                Console.WriteLine($"Authors with more than one book: {string.Join(", ", busy)}");

                // A SortedDictionary keeps its keys in order as they are added
                SortedDictionary<int, int> perDecade = new SortedDictionary<int, int>();
                foreach (Book book in books)
                {
                    int decade = book.Year / 10 * 10;   // 1813 -> 1810
                    perDecade[decade] = perDecade.GetValueOrDefault(decade, 0) + 1;
                }
                foreach (KeyValuePair<int, int> entry in perDecade)
                {
                    Console.WriteLine($"{entry.Key}s: {entry.Value}");
                }

                // Most books wins; a tie goes to the genre first in A-Z order
                string biggest = "";
                foreach (string genre in genres)
                {
                    if (biggest == "" || byGenre[genre].Count > byGenre[biggest].Count)
                    {
                        biggest = genre;
                    }
                }
                Console.WriteLine($"Biggest genre: {biggest} ({byGenre[biggest].Count} books)");
            }
        }
    }
}
