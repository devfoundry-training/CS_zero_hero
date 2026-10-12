// =====================================================================================================
// Task 03 - Functions and a menu (solution, Core + Stretch)
// -----------------------------------------------------------------------------------------------------
// Session 3: functions. Builds on task 02: every feature is now a function in Shelf.cs, and this file
// only runs the menu and prints.
//
// Run:  dotnet run --project tasks/03_functions/solution < tasks/data/input_03.txt
//       dotnet run --project tasks/03_functions/solution -- --stretch < tasks/data/input_03.txt
// (In PowerShell: Get-Content tasks/data/input_03.txt | dotnet run --project tasks/03_functions/solution)
// Type the choices yourself if you run it without the input file.
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    internal class Menu
    {
        const string MenuLine = "Menu: 1 catalogue | 2 search | 3 report | 4 stats | 0 quit";

        static void Main(string[] args)
        {
            RunMenu(args.Contains("--stretch"));
        }

        // Read one line from the keyboard (or a piped file). Returns the line without surrounding
        // spaces, or null at the end of the input. The line is echoed back as "> text" so the output
        // reads like a conversation even when the input comes from a file.
        static string? ReadLine()
        {
            string? line = Console.ReadLine();
            if (line == null)   // null only happens at the end of the input
            {
                return null;
            }
            string text = line.Trim();
            Console.WriteLine(text.Length > 0 ? $"> {text}" : ">");   // never leave a trailing space
            return text;
        }

        static void Show(List<string> lines)
        {
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }

        static void RunMenu(bool includeStretch)
        {
            Console.WriteLine("Library Manager");
            while (true)
            {
                Console.WriteLine(MenuLine);
                string? choice = ReadLine();
                if (choice == null || choice == "0")
                {
                    break;
                }

                // The choice is compared as text, so "abc" is just another unknown choice: no number
                // parsing, nothing to go wrong.
                switch (choice)
                {
                    case "1":
                        Show(Shelf.Catalogue());
                        break;
                    case "2":
                        Console.WriteLine("Search for:");
                        string? term = ReadLine();
                        if (term == null)
                        {
                            Console.WriteLine("Goodbye");
                            return;
                        }
                        Show(Shelf.SearchReport(term));
                        break;
                    case "3":
                        Show(Shelf.Report());
                        break;
                    case "4":
                        Show(Shelf.Stats(includeStretch));
                        break;
                    default:
                        Console.WriteLine($"Unknown choice: {choice}");
                        break;
                }
            }
            Console.WriteLine("Goodbye");
        }
    }
}
