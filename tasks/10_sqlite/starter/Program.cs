// =====================================================================================================
// Task 10 - SQLite storage (starter)
// -----------------------------------------------------------------------------------------------------
// Session 10: databases. The brief is in ../README.md. The task 09 model and loaders are here already;
// the new work goes in LibraryDb.cs and in Run() below. The Microsoft.Data.Sqlite package is already
// in the project file.
//
// Run (from the repo root):
//         dotnet run --project tasks/10_sqlite/starter -- tasks/data [TODAY]
// Check:  python tasks/check/check.py 10 -- dotnet run --project tasks/10_sqlite/starter -- tasks/data
// The program writes out/library.db in the current folder (out/ is in .gitignore).
// -----------------------------------------------------------------------------------------------------

using System.Text;

namespace LibraryManager
{
    internal class Program
    {
        static readonly DateOnly DefaultToday = new DateOnly(2026, 10, 12);
        static readonly string DbPath = Path.Combine("out", "library.db");

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
            string[] positional = args.Where(a => !a.StartsWith("--")).ToArray();
            string data = positional.Length > 0 ? positional[0] : Path.Combine("tasks", "data");
            DateOnly today = positional.Length > 1 ? Formatting.ParseIso(positional[1]) : DefaultToday;

            // Core 2: a fresh database every run
            Directory.CreateDirectory("out");
            File.Delete(DbPath);
            using LibraryDb db = new LibraryDb(DbPath);
            db.Create(Path.Combine(data, "schema.sql"));
            Console.WriteLine("Created out/library.db from schema.sql");

            // TODO (Core 3 to 6): import the CSV files (Storage.LoadBooksCsv and friends fill one
            // LoadResult), print the counts, the four SQL answers, run the three actions on
            // {today} saving each success, then reload and print the overdue report. See the brief.
            throw new NotImplementedException($"Core 3 to 6 in Program.Run (today is {Formatting.Iso(today)})");
        }
    }
}
