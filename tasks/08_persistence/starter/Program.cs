// =====================================================================================================
// Task 08 - Persistence (starter)
// -----------------------------------------------------------------------------------------------------
// Session 8: file I/O. The brief is in ../README.md. The task 07 model is here already; the new work
// goes in Storage.cs and in Run() below.
//
// Run (from the repo root):
//         dotnet run --project tasks/08_persistence/starter -- tasks/data
// Check:  python tasks/check/check.py 08 -- dotnet run --project tasks/08_persistence/starter -- tasks/data
// The program writes an out folder in the current folder (out/ is in .gitignore).
// -----------------------------------------------------------------------------------------------------

using System.Text;

namespace LibraryManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            try
            {
                Run(args);
            }
            catch (NotImplementedException error)
            {
                Console.WriteLine($"not implemented: {error.Message}");
            }
        }

        static void Run(string[] args)
        {
            // The data folder is the first argument, default tasks/data
            string[] positional = args.Where(a => !a.StartsWith("--")).ToArray();
            string data = positional.Length > 0 ? positional[0] : Path.Combine("tasks", "data");

            // Core 1: load the real data from disk
            LoadResult books = Storage.LoadBooksCsv(Path.Combine(data, "books.csv"));
            LoadResult members = Storage.LoadMembersCsv(Path.Combine(data, "members.csv"));
            Console.WriteLine($"Loaded {books.Books.Count} books from books.csv");
            Console.WriteLine($"Loaded {members.Members.Count} members from members.csv");
            books.Members.AddRange(members.Members);
            Library library = Storage.BuildLibrary(books);

            // TODO (Core 2 to 5): save out/books.csv and out/library.json, load out/books.csv back and
            // compare, then try missing.csv and books_corrupt.csv. The lines to print are in the brief.
            // Paths in the output always use "/".
            throw new NotImplementedException("Core 2 to 5 in Program.Run");
        }
    }
}
