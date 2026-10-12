// =====================================================================================================
// Task 12 - Storage: the CSV files, now with loans (from task 09)
// -----------------------------------------------------------------------------------------------------
// The task 08 CSV loaders plus loans.csv. Saving is unchanged from task 08 and left out here. Dates are
// text in the files (YYYY-MM-DD) and DateOnly values in the program; the conversion happens here and
// nowhere else.
// -----------------------------------------------------------------------------------------------------

using System.Text.RegularExpressions;

namespace LibraryManager
{
    // What a load produced: the good rows and the problems.
    public class LoadResult
    {
        public List<Book> Books { get; } = new List<Book>();
        public List<Member> Members { get; } = new List<Member>();
        public List<Loan> Loans { get; } = new List<Loan>();
        public List<string> Problems { get; } = new List<string>();
        public int Skipped { get; private set; }

        public void Skip(int lineNumber, string reason)
        {
            Problems.Add($"line {lineNumber}: {reason}");
            Skipped++;
        }
    }

    public static class Storage
    {
        const int BookFieldCount = 8;
        const int MemberFieldCount = 5;
        const int LoanFieldCount = 5;

        static readonly Regex WholeNumberPattern = new Regex("^-?[0-9]+$");

        static int WholeNumber(string text, string name)
        {
            if (!WholeNumberPattern.IsMatch(text) || !int.TryParse(text, out int value))
            {
                throw new ArgumentException($"{name} must be a whole number, got '{text}'");
            }
            return value;
        }

        // "2026-10-12" -> a DateOnly, or an ArgumentException with a message for the report.
        static DateOnly ParseDate(string text, string name)
        {
            try
            {
                return Formatting.ParseIso(text);
            }
            catch (FormatException)
            {
                throw new ArgumentException($"{name} must be a date YYYY-MM-DD, got '{text}'");
            }
        }

        // (line number, fields) for every data row with the right number of fields. Rows with the
        // wrong number are reported and skipped; blank lines are skipped silently.
        static List<(int LineNumber, string[] Row)> ReadRows(string path, int width, LoadResult result)
        {
            List<(int, string[])> rows = new List<(int, string[])>();
            string[] lines = File.ReadAllLines(path);   // throws FileNotFoundException
            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i].Length == 0)
                {
                    continue;
                }
                string[] row = lines[i].Split(',');
                if (row.Length != width)
                {
                    result.Skip(i + 1, $"expected {width} fields, found {row.Length}");
                    continue;
                }
                rows.Add((i + 1, row));
            }
            return rows;
        }

        // The optional result lets several files load into one LoadResult.
        public static LoadResult LoadBooksCsv(string path, LoadResult? result = null)
        {
            result ??= new LoadResult();   // ??= : make a new one only if none was passed in
            HashSet<int> seen = new HashSet<int>();
            foreach ((int lineNumber, string[] row) in ReadRows(path, BookFieldCount, result))
            {
                Book book;
                try
                {
                    WholeNumber(row[0], "id");
                    WholeNumber(row[3], "year");
                    WholeNumber(row[5], "copies");
                    WholeNumber(row[7], "length");
                    book = BookFactory.MakeBook(row);
                }
                catch (ArgumentException error)
                {
                    result.Skip(lineNumber, error.Message);
                    continue;
                }
                if (!seen.Add(book.Id))
                {
                    result.Skip(lineNumber, $"duplicate book id {book.Id}");
                    continue;
                }
                result.Books.Add(book);
            }
            return result;
        }

        public static LoadResult LoadMembersCsv(string path, LoadResult? result = null)
        {
            result ??= new LoadResult();
            foreach ((int lineNumber, string[] row) in ReadRows(path, MemberFieldCount, result))
            {
                try
                {
                    ParseDate(row[4], "joined");
                    result.Members.Add(new Member(WholeNumber(row[0], "id"), row[1], row[2], row[3], row[4]));
                }
                catch (ArgumentException error)
                {
                    result.Skip(lineNumber, error.Message);
                }
            }
            return result;
        }

        public static LoadResult LoadLoansCsv(string path, LoadResult? result = null)
        {
            result ??= new LoadResult();
            foreach ((int lineNumber, string[] row) in ReadRows(path, LoanFieldCount, result))
            {
                try
                {
                    // An empty returned field means the book is still out: an optional date
                    DateOnly? returned = row[4] == "" ? null : ParseDate(row[4], "returned");
                    result.Loans.Add(new Loan(WholeNumber(row[0], "id"), WholeNumber(row[1], "book_id"),
                                              WholeNumber(row[2], "member_id"), ParseDate(row[3], "borrowed"), returned));
                }
                catch (ArgumentException error)
                {
                    result.Skip(lineNumber, error.Message);
                }
            }
            return result;
        }

        public static Library BuildLibrary(LoadResult result)
        {
            Library library = new Library();
            foreach (Book book in result.Books)
            {
                library.AddBook(book);
            }
            foreach (Member member in result.Members)
            {
                library.AddMember(member);
            }
            foreach (Loan loan in result.Loans)
            {
                library.AddLoan(loan);
            }
            return library;
        }

        // books.csv, members.csv and loans.csv from one folder.
        public static Library LoadAll(string dataFolder)
        {
            LoadResult result = new LoadResult();
            LoadBooksCsv(Path.Combine(dataFolder, "books.csv"), result);
            LoadMembersCsv(Path.Combine(dataFolder, "members.csv"), result);
            LoadLoansCsv(Path.Combine(dataFolder, "loans.csv"), result);
            return BuildLibrary(result);
        }
    }
}
