// =====================================================================================================
// Task 03 - Shelf: every task 01 and 02 feature as a function (starter)
// -----------------------------------------------------------------------------------------------------
// Each function takes what it needs as parameters and RETURNS its result (a value, or the lines to
// print). Nothing here prints: Program.cs does that. The code for most of them is in your task 01 and
// task 02 programs already; move it here and make it return instead of print.
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    public static class Shelf
    {
        public const int Size = 10;
        public const int OldBefore = 1950;

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

        // =============================================================================================
        // DONE ALREADY (FROM TASK 01)
        // =============================================================================================

        public static string EraOf(int year)
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

        public static string CopiesText(int count)
        {
            if (count == 0)
            {
                return "out of stock";
            }
            if (count == 1)
            {
                return "1 copy";
            }
            return $"{count} copies";
        }

        // "1 match" or "4 matches"
        public static string Plural(int count, string singular, string many)
        {
            return $"{count} {(count == 1 ? singular : many)}";
        }

        // =============================================================================================
        // TODO: THE REST
        // =============================================================================================

        // One task 01 catalogue line for the book at this position.
        public static string CatalogueLine(int index)
        {
            throw new NotImplementedException("CatalogueLine");
        }

        // "Catalogue (10 books)" and the ten catalogue lines.
        public static List<string> Catalogue()
        {
            throw new NotImplementedException("Catalogue");
        }

        // The titles containing the term, ignoring case, in shelf order.
        public static List<string> Search(string term)
        {
            throw new NotImplementedException("Search");
        }

        // The task 02 search lines: Search "TERM": N matches, then each title indented by two spaces.
        public static List<string> SearchReport(string term)
        {
            throw new NotImplementedException("SearchReport");
        }

        // The task 02 report table, starting with "Report".
        public static List<string> Report()
        {
            throw new NotImplementedException("Report");
        }

        public static int TotalCopies()
        {
            throw new NotImplementedException("TotalCopies");
        }

        public static int AverageYear()
        {
            throw new NotImplementedException("AverageYear");
        }

        public static int WordCount()
        {
            throw new NotImplementedException("WordCount");
        }

        public static string LongestTitle()
        {
            throw new NotImplementedException("LongestTitle");
        }

        // "Stats", then books, copies, out of stock, pre-1950, average year, words in titles and
        // longest title (see Output format in the brief). Add any small helpers you need.
        public static List<string> Stats()
        {
            throw new NotImplementedException("Stats");
        }
    }
}
