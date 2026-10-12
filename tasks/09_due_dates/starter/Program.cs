// =====================================================================================================
// Task 09 - Loans with due dates (starter)
// -----------------------------------------------------------------------------------------------------
// Session 9: dates and times. The brief is in ../README.md. The task 08 model and loaders are here
// already, with the new pieces stubbed in Formatting.cs, Library.cs and Storage.cs.
//
// Run (from the repo root):
//         dotnet run --project tasks/09_due_dates/starter -- tasks/data [TODAY]
// Check:  python tasks/check/check.py 09 -- dotnet run --project tasks/09_due_dates/starter -- tasks/data
// TODAY defaults to 2026-10-12. Never read the real clock.
// -----------------------------------------------------------------------------------------------------

using System.Text;

namespace LibraryManager
{
    internal class Program
    {
        static readonly DateOnly DefaultToday = new DateOnly(2026, 10, 12);

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

            Library library = Storage.LoadAll(data);
            Console.WriteLine($"Today is {Formatting.Iso(today)}");

            // TODO (Core 6 and 7): using library, print the loans table, the overdue report, the fines
            // totals and the new loans, exactly as in the brief's Output format.
            throw new NotImplementedException("Core 6 and 7: the reports in Program.Run");
        }
    }
}
