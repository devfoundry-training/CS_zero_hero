// =====================================================================================================
// Task 03 - Shelf: every task 01 and 02 feature as a function
// -----------------------------------------------------------------------------------------------------
// Stretch 1: the functions live in their own static class, in their own file, and Program.cs calls
// them. Nothing here prints: every function returns a value (a string, a number or the lines to print)
// and Program.cs decides what to print.
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    public static class Shelf
    {
        public const int Size = 10;
        public const int OldBefore = 1950;

        // static readonly: set once when the class is first used, never replaced
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
        // SMALL HELPERS (TASK 01)
        // =============================================================================================

        // The century a year belongs to, from year / 100 (integer division).
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

        // "out of stock", "1 copy" or "N copies".
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

        public static bool IsOld(int year)
        {
            return year < OldBefore;
        }

        // "1 match" or "4 matches": the number and the right noun.
        public static string Plural(int count, string singular, string many)
        {
            return $"{count} {(count == 1 ? singular : many)}";
        }

        // =============================================================================================
        // FEATURES (TASKS 01 AND 02)
        // =============================================================================================

        public static string CatalogueLine(int index)
        {
            string line = $"{Ids[index]} | {Titles[index]} | {Years[index]} | {CopiesText(Copies[index])} | {EraOf(Years[index])}";
            if (IsOld(Years[index]))
            {
                line += " | pre-1950";
            }
            return line;
        }

        public static List<string> Catalogue()
        {
            List<string> lines = new List<string> { $"Catalogue ({Size} books)" };
            for (int i = 0; i < Size; i++)
            {
                lines.Add(CatalogueLine(i));
            }
            return lines;
        }

        // Titles containing the term, ignoring case, in shelf order.
        public static List<string> Search(string term)
        {
            List<string> found = new List<string>();
            foreach (string title in Titles)
            {
                if (title.Contains(term, StringComparison.OrdinalIgnoreCase))
                {
                    found.Add(title);
                }
            }
            return found;
        }

        public static List<string> SearchReport(string term)
        {
            List<string> found = Search(term);
            if (found.Count == 0)
            {
                return new List<string> { $"Search \"{term}\": no matches" };
            }
            List<string> lines = new List<string> { $"Search \"{term}\": {Plural(found.Count, "match", "matches")}" };
            foreach (string title in found)
            {
                lines.Add($"  {title}");
            }
            return lines;
        }

        public static List<string> Report()
        {
            List<string> lines = new List<string>
            {
                "Report",
                $"{"ID",2}  {"Title",-30}  {"Year",-4}  {"Copies",6}",
                $"{new string('-', 2)}  {new string('-', 30)}  {new string('-', 4)}  {new string('-', 6)}",
            };
            for (int i = 0; i < Size; i++)
            {
                lines.Add($"{Ids[i],2}  {Titles[i],-30}  {Years[i],-4}  {Copies[i],6}");
            }
            return lines;
        }

        public static int TotalCopies()
        {
            int total = 0;
            foreach (int count in Copies)
            {
                total += count;
            }
            return total;
        }

        public static int CountOutOfStock()
        {
            int count = 0;
            foreach (int copies in Copies)
            {
                if (copies == 0)
                {
                    count++;
                }
            }
            return count;
        }

        public static int CountOld()
        {
            int count = 0;
            foreach (int year in Years)
            {
                if (IsOld(year))
                {
                    count++;
                }
            }
            return count;
        }

        public static int AverageYear()
        {
            int total = 0;
            foreach (int year in Years)
            {
                total += year;
            }
            return total / Size;   // integer division
        }

        public static int WordCount()
        {
            int words = 0;
            foreach (string title in Titles)
            {
                words += title.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
            }
            return words;
        }

        public static string LongestTitle()
        {
            string longest = Titles[0];
            for (int i = 1; i < Size; i++)
            {
                if (Titles[i].Length > longest.Length)
                {
                    longest = Titles[i];
                }
            }
            return longest;
        }

        // Stretch 2: two results from one function, as a tuple of array positions.
        public static (int Oldest, int Newest) OldestAndNewest()
        {
            int oldest = 0;
            int newest = 0;
            for (int i = 1; i < Size; i++)
            {
                if (Years[i] < Years[oldest])
                {
                    oldest = i;
                }
                if (Years[i] > Years[newest])
                {
                    newest = i;
                }
            }
            return (oldest, newest);
        }

        // The default value means callers can leave the argument out: Stats() is the same as Stats(false).
        public static List<string> Stats(bool includeStretch = false)
        {
            string longest = LongestTitle();
            List<string> lines = new List<string>
            {
                "Stats",
                $"books: {Size}",
                $"copies: {TotalCopies()}",
                $"out of stock: {CountOutOfStock()}",
                $"pre-1950: {CountOld()}",
                $"average year: {AverageYear()}",
                $"words in titles: {WordCount()}",
                $"longest title: {longest} ({longest.Length} characters)",
            };
            if (includeStretch)
            {
                (int oldest, int newest) = OldestAndNewest();   // deconstruct the tuple into two variables
                lines.Add($"oldest: {Titles[oldest]} ({Years[oldest]})");
                lines.Add($"newest: {Titles[newest]} ({Years[newest]})");
            }
            return lines;
        }
    }
}
