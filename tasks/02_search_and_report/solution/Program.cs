// =====================================================================================================
// Task 02 - Search and report (solution, Core + Stretch)
// -----------------------------------------------------------------------------------------------------
// Session 2: loops and strings. Builds on task 01 (the same shelf of fixed arrays).
//
// Run:  dotnet run --project tasks/02_search_and_report/solution                 (Core output, checked)
//       dotnet run --project tasks/02_search_and_report/solution -- --stretch    (Core, then the Stretch)
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    internal class SearchAndReport
    {
        const int Size = 10;

        static void Main(string[] args)
        {
            // The shelf from task 01: parallel arrays, never resized
            int[] ids = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            string[] titles =
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
            int[] years = { 1813, 1949, 1937, 1965, 1719, 1895, 1945, 1815, 1969, 1934 };
            int[] copies = { 3, 2, 0, 4, 1, 2, 5, 1, 0, 2 };

            string[] searchTerms = { "the", "TIME", "zzz" };

            // =========================================================================================
            // CORE 1: A FORMATTED REPORT TABLE
            // -----------------------------------------------------------------------------------------
            // Column widths: id 2 (right), title 30 (left), year 4, copies 6 (right), two spaces between
            // columns. In {value,N} a positive N pads on the left (right-aligns) and a negative N pads
            // on the right (left-aligns).
            // -----------------------------------------------------------------------------------------

            Console.WriteLine("Report");
            Console.WriteLine($"{"ID",2}  {"Title",-30}  {"Year",-4}  {"Copies",6}");
            Console.WriteLine($"{new string('-', 2)}  {new string('-', 30)}  {new string('-', 4)}  {new string('-', 6)}");
            for (int i = 0; i < Size; i++)
            {
                Console.WriteLine($"{ids[i],2}  {titles[i],-30}  {years[i],-4}  {copies[i],6}");
            }

            // =========================================================================================
            // CORE 2: CASE-INSENSITIVE SEARCH
            // -----------------------------------------------------------------------------------------
            // StringComparison.OrdinalIgnoreCase compares character by character and ignores case.
            // A while loop collects the matches into a fixed array plus a count.
            // -----------------------------------------------------------------------------------------

            foreach (string term in searchTerms)
            {
                string[] found = new string[Size];
                int count = 0;
                int i = 0;
                while (i < Size)
                {
                    if (titles[i].Contains(term, StringComparison.OrdinalIgnoreCase))
                    {
                        found[count] = titles[i];
                        count++;
                    }
                    i++;
                }

                if (count == 0)
                {
                    Console.WriteLine($"Search \"{term}\": no matches");
                    continue;
                }
                string noun = count == 1 ? "match" : "matches";
                Console.WriteLine($"Search \"{term}\": {count} {noun}");
                for (int j = 0; j < count; j++)
                {
                    Console.WriteLine($"  {found[j]}");
                }
            }

            // =========================================================================================
            // CORE 3: COUNT THE WORDS IN ALL TITLES
            // -----------------------------------------------------------------------------------------
            // A word is a run of characters between spaces, so "Eighty-Four" stays one word.
            // RemoveEmptyEntries means two spaces in a row never make an empty "word".
            // -----------------------------------------------------------------------------------------

            int words = 0;
            foreach (string title in titles)
            {
                words += title.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
            }
            Console.WriteLine($"Words in titles: {words}");

            // =========================================================================================
            // CORE 4: THE LONGEST TITLE (THE FIRST ONE WINS A TIE)
            // =========================================================================================

            int longest = 0;
            for (int i = 1; i < Size; i++)
            {
                if (titles[i].Length > titles[longest].Length)   // > not >=, so a tie keeps the first
                {
                    longest = i;
                }
            }
            Console.WriteLine($"Longest title: {titles[longest]} ({titles[longest].Length} characters)");

            // =========================================================================================
            // STRETCH: INITIALS, THE SHORTEST TITLE, A COMPACT LIST
            // =========================================================================================

            if (args.Contains("--stretch"))
            {
                Console.WriteLine("Stretch");
                foreach (string title in titles)
                {
                    string initials = "";
                    foreach (string word in title.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                    {
                        initials += char.ToUpperInvariant(word[0]);
                    }
                    Console.WriteLine($"{initials,-6}{title}");
                }

                int shortest = 0;
                for (int i = 1; i < Size; i++)
                {
                    if (titles[i].Length < titles[shortest].Length)
                    {
                        shortest = i;
                    }
                }
                Console.WriteLine($"Shortest title: {titles[shortest]} ({titles[shortest].Length} characters)");

                // Titles longer than 20 characters are cut to their first 17 plus "..."
                for (int i = 0; i < Size; i++)
                {
                    string shortTitle = titles[i].Length <= 20 ? titles[i] : titles[i].Substring(0, 17) + "...";
                    Console.WriteLine($"{ids[i],2}  {shortTitle,-20}  {years[i]}");
                }
            }
        }
    }
}
