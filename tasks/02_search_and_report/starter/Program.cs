// =====================================================================================================
// Task 02 - Search and report (starter)
// -----------------------------------------------------------------------------------------------------
// Session 2: loops and strings. The brief is in ../README.md.
//
// Run:    dotnet run --project tasks/02_search_and_report/starter
// Check:  python tasks/check/check.py 02 -- dotnet run --project tasks/02_search_and_report/starter
//
// The shelf and the helpers from task 01 are here already. Fill in each TODO; the program says
// "not implemented" until they are all done.
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    internal class SearchAndReport
    {
        const int Size = 10;

        // The shelf from task 01: parallel arrays, never resized
        static readonly int[] Ids = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        static readonly string[] Titles =
        {
            "Pride and Prejudice",
            "Nineteen Eighty-Four",
            "The Hobbit",
            "Dune",
            "Robinson Crusoe",
            "The Time Machine",
            "Animal Farm",
            "Emma",
            "The Left Hand of Darkness",
            "Murder on the Orient Express",
        };
        static readonly int[] Years = { 1813, 1949, 1937, 1965, 1719, 1895, 1945, 1815, 1969, 1934 };
        static readonly int[] Copies = { 3, 2, 0, 4, 1, 2, 5, 1, 0, 2 };

        static readonly string[] SearchTerms = { "the", "TIME", "zzz" };

        static void Main(string[] args)
        {
            try
            {
                PrintReport();
                foreach (string term in SearchTerms)
                {
                    PrintSearch(term);
                }
                PrintWordCount();
                PrintLongestTitle();
            }
            catch (NotImplementedException error)
            {
                Console.WriteLine($"not implemented: {error.Message}");
            }
        }

        // Core 1: "Report", a header row, a row of dashes, then one row per book.
        // Widths: ID 2 (right), Title 30 (left), Year 4, Copies 6 (right), two spaces between columns.
        // Hint: $"{id,2}  {title,-30}" and new string('-', 30).
        static void PrintReport()
        {
            throw new NotImplementedException("Core 1: PrintReport");
        }

        // Core 2: search the titles for the term, ignoring case, with a while loop that collects the
        // matches into a fixed array plus a count. Then print
        //   Search "TERM": N matches   (or 1 match, or no matches)
        // and each matching title indented by two spaces.
        static void PrintSearch(string term)
        {
            throw new NotImplementedException("Core 2: PrintSearch");
        }

        // Core 3: "Words in titles: N". Words are separated by spaces.
        static void PrintWordCount()
        {
            throw new NotImplementedException("Core 3: PrintWordCount");
        }

        // Core 4: "Longest title: TITLE (N characters)". The first one wins a tie.
        static void PrintLongestTitle()
        {
            throw new NotImplementedException("Core 4: PrintLongestTitle");
        }

        // =============================================================================================
        // FROM TASK 01 (NOT NEEDED FOR THE CORE, HANDY FOR THE STRETCH AND CHALLENGE)
        // =============================================================================================

        static string EraOf(int year)
        {
            return (year / 100) switch
            {
                17 => "18th century",
                18 => "19th century",
                19 => "20th century",
                20 => "21st century",
                _ => "unknown era",
            };
        }

        static string CopiesText(int count)
        {
            if (count == 0)
            {
                return "out of stock";
            }
            else if (count == 1)
            {
                return "1 copy";
            }
            else
            {
                return $"{count} copies";
            }
        }
    }
}
