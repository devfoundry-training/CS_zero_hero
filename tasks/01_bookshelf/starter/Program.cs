// =====================================================================================================
// Task 01 - Bookshelf (starter)
// -----------------------------------------------------------------------------------------------------
// Session 1: data types, operators, conditionals, switch, arrays. The brief is in ../README.md.
//
// Run:    dotnet run --project tasks/01_bookshelf/starter
// Check:  python tasks/check/check.py 01 -- dotnet run --project tasks/01_bookshelf/starter
//
// This file builds and runs as it is. Fill in each TODO; the program says "not implemented" until the
// last one is done.
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    internal class Bookshelf
    {
        const int Size = 10;
        const int OldBefore = 1950;

        static void Main(string[] args)
        {
            try
            {
                Run();
            }
            catch (NotImplementedException error)
            {
                Console.WriteLine($"not implemented: {error.Message}");
            }
        }

        static void Run()
        {
            // =========================================================================================
            // CORE 1: THE TEN BOOKS IN FOUR FIXED-SIZE ARRAYS (GIVEN)
            // -----------------------------------------------------------------------------------------
            // Position i in every array describes the same book.
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
            // CORE 2-5: THE CATALOGUE
            // -----------------------------------------------------------------------------------------
            // TODO: print "Catalogue (10 books)", then one line per book:
            //       id | title | year | copies text | century
            //       with " | pre-1950" on the end when the year is before OldBefore.
            //       Use CopiesText() and EraOf() below.
            // -----------------------------------------------------------------------------------------

            // =========================================================================================
            // CORE 6: "REMOVE" THE EVEN IDS
            // -----------------------------------------------------------------------------------------
            // TODO: copy the odd-id books into a second pair of arrays (ids and titles) of the same size,
            //       count the slots you use, then print "Odd ids only (N books)" and "id | title" lines.
            // -----------------------------------------------------------------------------------------

            // =========================================================================================
            // CORE 7: TOTALS
            // -----------------------------------------------------------------------------------------
            // TODO: print "Totals", then books, copies, out of stock, pre-1950 and the average year
            //       (integer division).
            // -----------------------------------------------------------------------------------------

            // Delete this line when the three TODOs above are done
            throw new NotImplementedException("Core 2-7: the catalogue, the odd ids and the totals (see ../README.md)");
        }

        // Core 4: the century, from a switch expression on year / 100.
        // 17 => "18th century", 18 => "19th century", 19 => "20th century", 20 => "21st century",
        // anything else => "unknown era".
        static string EraOf(int year)
        {
            throw new NotImplementedException("Core 4: EraOf");
        }

        // Core 3: "out of stock" for 0, "1 copy" for 1, "N copies" otherwise, with if / else.
        static string CopiesText(int count)
        {
            throw new NotImplementedException("Core 3: CopiesText");
        }
    }
}
