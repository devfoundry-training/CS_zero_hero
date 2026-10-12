// =====================================================================================================
// Task 08 - Persistence (solution, Core + Stretch)
// -----------------------------------------------------------------------------------------------------
// Session 8: file I/O. Builds on task 07: the data now comes from the files in the data folder, and the
// library is saved to CSV and JSON in a folder called out (made in the current folder). Core reads the
// CSV back; reading the JSON back is the Stretch. The new work is in Storage.cs.
//
// Run (from the repo root):
//       dotnet run --project tasks/08_persistence/solution -- tasks/data               (Core, checked)
//       dotnet run --project tasks/08_persistence/solution -- tasks/data --stretch     (Core, then Stretch)
// The data folder defaults to tasks/data.
// -----------------------------------------------------------------------------------------------------

using System.Text;

namespace LibraryManager
{
    internal class Program
    {
        const string OutFolder = "out";

        // Same ids with the same field values, in the same order. Tuples compare field by field.
        static bool SameBooks(Library a, Library b)
        {
            static List<(int, string, string, int, string, int, string, int)> Rows(Library library)
            {
                return library.AllBooks()
                    .Select(x => (x.Id, x.Title, x.Author, x.Year, x.Genre, x.Copies, x.FormatName, x.Length))
                    .ToList();
            }
            return Rows(a).SequenceEqual(Rows(b));
        }

        static bool SameMembers(Library a, Library b)
        {
            static List<string> Rows(Library library)
            {
                return library.AllMembers()
                    .Select(m => $"{m.Id}|{m.Name}|{m.Email}|{m.Type}|{m.Joined}|{string.Join(" ", m.Loans)}")
                    .ToList();
            }
            return Rows(a).SequenceEqual(Rows(b));
        }

        // Load a file that might be missing or broken, and say what happened.
        static void TryLoad(string name, Func<string, LoadResult> loader, string path)
        {
            Console.WriteLine($"Loading {name}");
            LoadResult result;
            try
            {
                result = loader(path);
            }
            catch (IOException error) when (error is FileNotFoundException || error is DirectoryNotFoundException)
            {
                Console.WriteLine("  file not found, starting with an empty catalogue");
                return;
            }
            catch (InvalidDataException error)
            {
                Console.WriteLine($"  {error.Message}, starting with an empty catalogue");
                return;
            }
            foreach (string problem in result.Problems)
            {
                Console.WriteLine($"  {problem}");
            }
            Console.WriteLine($"  loaded {result.Books.Count} books, skipped {result.Skipped} lines");
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string[] positional = args.Where(a => !a.StartsWith("--")).ToArray();
            string data = positional.Length > 0 ? positional[0] : Path.Combine("tasks", "data");
            string booksOut = Path.Combine(OutFolder, "books.csv");
            string jsonOut = Path.Combine(OutFolder, "library.json");

            // =========================================================================================
            // CORE 1: LOAD THE REAL DATA FROM DISK
            // =========================================================================================

            LoadResult books = Storage.LoadBooksCsv(Path.Combine(data, "books.csv"));
            LoadResult members = Storage.LoadMembersCsv(Path.Combine(data, "members.csv"));
            Console.WriteLine($"Loaded {books.Books.Count} books from books.csv");
            Console.WriteLine($"Loaded {members.Members.Count} members from members.csv");
            books.Members.AddRange(members.Members);
            Library library = Storage.BuildLibrary(books);

            // =========================================================================================
            // CORE 2 AND 3: SAVE AS CSV AND JSON, THEN LOAD THE CSV BACK AND COMPARE
            // -----------------------------------------------------------------------------------------
            // Paths in the output always use "/", whatever Path.Combine uses on this machine.
            // -----------------------------------------------------------------------------------------

            Storage.SaveBooksCsv(library, booksOut);
            Console.WriteLine($"Saved {library.BookCount} books to out/books.csv");
            Storage.SaveJson(library, jsonOut);
            Console.WriteLine($"Saved {library.BookCount} books and {library.MemberCount} members to out/library.json");

            Library fromCsv = Storage.BuildLibrary(Storage.LoadBooksCsv(booksOut));
            string verdict = SameBooks(library, fromCsv) ? "same as before" : "DIFFERENT";
            Console.WriteLine($"Reloaded out/books.csv: {fromCsv.BookCount} books, {verdict}");

            // =========================================================================================
            // CORE 4 AND 5: A MISSING FILE AND A CORRUPT CSV
            // =========================================================================================

            TryLoad("missing.csv", Storage.LoadBooksCsv, Path.Combine(data, "missing.csv"));
            TryLoad("books_corrupt.csv", Storage.LoadBooksCsv, Path.Combine(data, "books_corrupt.csv"));

            // =========================================================================================
            // STRETCH: READ THE JSON BACK, COPE WITH BROKEN JSON, AND KEEP LOANS ACROSS A RESTART
            // =========================================================================================

            if (args.Contains("--stretch"))
            {
                Console.WriteLine("Stretch");
                Library fromJson = Storage.BuildLibrary(Storage.LoadJson(jsonOut));
                bool same = SameBooks(library, fromJson) && SameMembers(library, fromJson);
                Console.WriteLine($"Reloaded out/library.json: {fromJson.BookCount} books and {fromJson.MemberCount} members, "
                                  + (same ? "same as before" : "DIFFERENT"));
                TryLoad("library_corrupt.json", Storage.LoadJson, Path.Combine(data, "library_corrupt.json"));
                library.Borrow(101, 4);
                library.Borrow(101, 7);
                Storage.SaveJson(library, jsonOut);
                Library restarted = Storage.BuildLibrary(Storage.LoadJson(jsonOut));
                Console.WriteLine($"after restart: {restarted.FindMember(101)}");
                Console.WriteLine($"after restart: {restarted.FindById(4)}");
            }
        }
    }
}
