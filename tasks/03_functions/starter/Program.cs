// =====================================================================================================
// Task 03 - Functions and a menu (starter)
// -----------------------------------------------------------------------------------------------------
// Session 3: functions. The brief is in ../README.md. The functions go in Shelf.cs; this file runs the
// menu and is the only place that prints.
//
// Run:    dotnet run --project tasks/03_functions/starter < tasks/data/input_03.txt
//         (PowerShell: Get-Content tasks/data/input_03.txt | dotnet run --project tasks/03_functions/starter)
// Check:  python tasks/check/check.py 03 -- dotnet run --project tasks/03_functions/starter
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    internal class Menu
    {
        const string MenuLine = "Menu: 1 catalogue | 2 search | 3 report | 4 stats | 0 quit";

        static void Main(string[] args)
        {
            try
            {
                RunMenu();
            }
            catch (NotImplementedException error)
            {
                Console.WriteLine($"not implemented: {error.Message}");
            }
        }

        // Read one line of input. Returns it with the spaces trimmed from both ends, or null at the end
        // of the input. TODO: echo the line as "> TEXT" (a blank line echoes as ">").
        static string? ReadLine()
        {
            string? line = Console.ReadLine();   // null at the end of the input
            if (line == null)
            {
                return null;
            }
            return line.Trim();
        }

        // TODO (Core 2-5): print "Library Manager", then loop: print MenuLine, read a choice and do what
        // it says (1 catalogue, 2 search, 3 report, 4 stats, 0 quit, anything else
        // "Unknown choice: TEXT"). Compare the choice as text. End of input counts as 0. Always finish
        // with "Goodbye".
        static void RunMenu()
        {
            throw new NotImplementedException("Core 2: the menu loop in RunMenu");
        }
    }
}
