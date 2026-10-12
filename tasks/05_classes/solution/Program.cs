// =====================================================================================================
// Task 05 - Classes (solution, Core + Stretch)
// -----------------------------------------------------------------------------------------------------
// Session 5: classes and encapsulation. Builds on task 04: the records become Book, Member and Library
// objects with methods (Book.cs, Member.cs, Library.cs).
//
// Run:  dotnet run --project tasks/05_classes/solution                 (Core output, checked)
//       dotnet run --project tasks/05_classes/solution -- --stretch    (Core, then the Stretch)
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    internal class Program
    {
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

        static readonly string[] MemberLines =
        {
            "id,name,email,type,joined",
            "101,Amira Khan,amira.khan@example.com,standard,2023-01-15",
            "102,Ben Taylor,ben.taylor@example.com,student,2024-09-02",
            "103,Chloe Evans,chloe.evans@example.com,staff,2019-05-20",
            "104,Dev Patel,dev.patel@example.com,student,2025-09-10",
            "105,Ella Brown,ella.brown@example.com,standard,2021-03-08",
            "106,Femi Adeyemi,femi.adeyemi@example.com,standard,2022-11-30",
            "107,Grace Wilson,grace.wilson@example.com,staff,2018-07-01",
            "108,Harry Jones,harry.jones@example.com,student,2026-01-12",
            "109,Ben Taylor,ben.taylor2@example.com,standard,2025-02-14",
        };

        static Library LoadLibrary()
        {
            Library library = new Library();
            foreach (string line in BookLines.Skip(1))   // Skip(1) leaves out the header row
            {
                string[] f = line.Split(',');
                library.AddBook(new Book(int.Parse(f[0]), f[1], f[2], int.Parse(f[3]), f[4], int.Parse(f[5])));
            }
            foreach (string line in MemberLines.Skip(1))
            {
                string[] f = line.Split(',');
                library.AddMember(new Member(int.Parse(f[0]), f[1], f[2], f[3], f[4]));
            }
            return library;
        }

        static string Plural(int count, string singular, string many)
        {
            return $"{count} {(count == 1 ? singular : many)}";
        }

        static void Main(string[] args)
        {
            Library library = LoadLibrary();
            Console.WriteLine($"Library: {library.BookCount} books, {library.MemberCount} members");
            Console.WriteLine(library.FindById(4));      // WriteLine calls ToString() for us
            Console.WriteLine(library.FindMember(101));

            foreach (string text in new[] { "the", "zzz" })
            {
                List<Book> found = library.FindByTitle(text);
                if (found.Count == 0)
                {
                    Console.WriteLine($"Find \"{text}\": no books");
                    continue;
                }
                Console.WriteLine($"Find \"{text}\": {Plural(found.Count, "book", "books")}");
                foreach (Book book in found)
                {
                    Console.WriteLine($"  {book}");
                }
            }

            // null means "not found": check for it before using the result
            Book? missing = library.FindById(99);
            Console.WriteLine(missing == null ? "Find id 99: not found" : $"Find id 99: {missing}");

            Console.WriteLine("Borrowing");
            (string Action, int MemberId, int BookId)[] script =
            {
                ("borrow", 101, 4),
                ("borrow", 101, 3),
                ("borrow", 999, 1),
                ("borrow", 101, 99),
                ("borrow", 102, 5),
                ("borrow", 103, 5),
                ("return", 101, 4),
                ("return", 101, 4),
            };
            foreach ((string action, int memberId, int bookId) in script)
            {
                bool ok = action == "borrow" ? library.Borrow(memberId, bookId) : library.ReturnBook(memberId, bookId);
                Console.WriteLine($"{action} {memberId} {bookId}: {(ok ? "OK" : "refused")}");
            }

            Console.WriteLine("After borrowing");
            Console.WriteLine(library.FindById(4));
            Console.WriteLine(library.FindById(5));
            Console.WriteLine(library.FindMember(101));
            Console.WriteLine(library.FindMember(102));

            Console.WriteLine("Validation");
            Action[] attempts =
            {
                () => new Book(0, "Dune", "Frank Herbert", 1965, "Science Fiction", 4),
                () => new Book(21, "   ", "Frank Herbert", 1965, "Science Fiction", 4),
                () => new Book(21, "Dune", "Frank Herbert", 3000, "Science Fiction", 4),
                () => new Book(21, "Dune", "Frank Herbert", 1965, "Science Fiction", -1),
                () => new Member(110, "Sam Green", "sam.green@example.com", "visitor", "2026-10-12"),
            };
            foreach (Action attempt in attempts)
            {
                try
                {
                    attempt();
                    Console.WriteLine("Accepted");
                }
                catch (ArgumentException error)
                {
                    Console.WriteLine($"Rejected: {error.Message}");
                }
            }

            if (args.Contains("--stretch"))
            {
                Console.WriteLine("Stretch");
                Book copy = new Book(4, "Dune (second copy record)", "Frank Herbert", 1965, "Science Fiction", 1);
                Console.WriteLine($"same book as #4: {copy.Equals(library.FindById(4))}");
                List<Book> sorted = library.AllBooks();
                sorted.Sort();   // uses Book.CompareTo
                Console.WriteLine("first 5 by title: " + string.Join(", ", sorted.Take(5).Select(b => $"{b.Title} ({b.Year})")));
                Console.WriteLine($"available books: {library.AvailableBooks().Count}");
            }
        }
    }
}
