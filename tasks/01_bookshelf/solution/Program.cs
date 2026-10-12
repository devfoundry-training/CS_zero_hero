// =====================================================================================================
// Task 01 - Bookshelf (solution, Core + Stretch)
// -----------------------------------------------------------------------------------------------------
// Session 1: data types, operators, conditionals, switch, arrays.
//
// Run:  dotnet run --project tasks/01_bookshelf/solution                 (Core output, checked)
//       dotnet run --project tasks/01_bookshelf/solution -- --stretch    (Core, then the Stretch)
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    internal class Bookshelf
    {
        // const values are fixed when the program is compiled and can never change
        const int Size = 10;
        const int OldBefore = 1950;

        static void Main(string[] args)
        {
            // =========================================================================================
            // THE SHELF: FOUR PARALLEL ARRAYS, ONE PER FIELD
            // -----------------------------------------------------------------------------------------
            // An array has a fixed length, set when it is created. Position i in every array describes
            // the same book.
            // -----------------------------------------------------------------------------------------

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

            // =========================================================================================
            // CORE 2-5: THE CATALOGUE WITH COPIES, CENTURY AND THE PRE-1950 FLAG
            // =========================================================================================

            Console.WriteLine($"Catalogue ({Size} books)");
            for (int i = 0; i < Size; i++)
            {
                string line = $"{ids[i]} | {titles[i]} | {years[i]} | {CopiesText(copies[i])} | {EraOf(years[i])}";
                if (years[i] < OldBefore)
                {
                    line += " | pre-1950";
                }
                Console.WriteLine(line);
            }

            // =========================================================================================
            // CORE 6: "REMOVE" THE EVEN IDS
            // -----------------------------------------------------------------------------------------
            // A fixed array cannot shrink, so we copy the books we keep into a second pair of arrays of
            // the same size and count the slots we use.
            // -----------------------------------------------------------------------------------------

            int[] keptIds = new int[Size];
            string[] keptTitles = new string[Size];
            int kept = 0;
            for (int i = 0; i < Size; i++)
            {
                if (ids[i] % 2 != 0)   // % is the remainder: odd ids leave 1
                {
                    keptIds[kept] = ids[i];
                    keptTitles[kept] = titles[i];
                    kept++;
                }
            }

            Console.WriteLine($"Odd ids only ({kept} books)");
            for (int i = 0; i < kept; i++)
            {
                Console.WriteLine($"{keptIds[i]} | {keptTitles[i]}");
            }

            // =========================================================================================
            // CORE 7: TOTALS WITH ARITHMETIC AND COMPARISON OPERATORS
            // =========================================================================================

            int totalCopies = 0;
            int outOfStock = 0;
            int oldBooks = 0;
            int yearSum = 0;
            for (int i = 0; i < Size; i++)
            {
                totalCopies += copies[i];
                yearSum += years[i];
                if (copies[i] == 0)
                {
                    outOfStock++;
                }
                if (years[i] < OldBefore)
                {
                    oldBooks++;
                }
            }

            Console.WriteLine("Totals");
            Console.WriteLine($"books: {Size}");
            Console.WriteLine($"copies: {totalCopies}");
            Console.WriteLine($"out of stock: {outOfStock}");
            Console.WriteLine($"pre-1950: {oldBooks}");
            Console.WriteLine($"average year: {yearSum / Size}");   // int / int is integer division: rounds down

            // =========================================================================================
            // STRETCH: OLDEST AND NEWEST, AND A COUNT PER CENTURY
            // =========================================================================================

            if (args.Contains("--stretch"))
            {
                int oldest = 0;
                int newest = 0;
                for (int i = 1; i < Size; i++)
                {
                    if (years[i] < years[oldest])
                    {
                        oldest = i;
                    }
                    if (years[i] > years[newest])
                    {
                        newest = i;
                    }
                }
                Console.WriteLine("Stretch");
                Console.WriteLine($"oldest: {titles[oldest]} ({years[oldest]})");
                Console.WriteLine($"newest: {titles[newest]} ({years[newest]})");

                // One counter per century, in a fixed order
                string[] eraNames = { "18th century", "19th century", "20th century", "21st century" };
                int[] eraCounts = new int[eraNames.Length];
                for (int i = 0; i < Size; i++)
                {
                    for (int e = 0; e < eraNames.Length; e++)
                    {
                        if (EraOf(years[i]) == eraNames[e])
                        {
                            eraCounts[e]++;
                        }
                    }
                }
                for (int e = 0; e < eraNames.Length; e++)
                {
                    Console.WriteLine($"{eraNames[e]}: {eraCounts[e]}");
                }
            }
        }

        // Name the century a year belongs to, using a switch expression on year / 100.
        static string EraOf(int year)
        {
            // / on two int values is integer division: 1813 / 100 is 18, never 18.13
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
