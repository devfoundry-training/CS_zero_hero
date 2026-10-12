// =====================================================================================================
// Task 08 - Storage: CSV and JSON in and out
// -----------------------------------------------------------------------------------------------------
// Loading never crashes on bad data. A bad line is reported as "line N: reason" and skipped; the good
// lines still load. A missing file throws FileNotFoundException for the caller to handle.
// -----------------------------------------------------------------------------------------------------

using System.Text.Json;
using System.Text.RegularExpressions;

namespace LibraryManager
{
    // What a load produced: the good rows and the problems.
    public class LoadResult
    {
        public List<Book> Books { get; } = new List<Book>();
        public List<Member> Members { get; } = new List<Member>();
        public List<(int MemberId, int BookId)> Loans { get; } = new List<(int MemberId, int BookId)>();   // from JSON
        public List<string> Problems { get; } = new List<string>();
        public int Skipped { get; private set; }

        public void Skip(int lineNumber, string reason)
        {
            Problems.Add($"line {lineNumber}: {reason}");
            Skipped++;
        }
    }

    // The shape of out/library.json. With the camelCase naming policy, Id is written as "id" and so on.
    public record BookRecord(int Id, string Title, string Author, int Year, string Genre, int Copies, string Format, int Length);

    public record MemberRecord(int Id, string Name, string Email, string Type, string Joined, List<int> Loans);

    public record LibraryDocument(List<BookRecord> Books, List<MemberRecord> Members);

    public static class Storage
    {
        const string BookHeader = "id,title,author,year,genre,copies,format,length";
        const int BookFieldCount = 8;
        const int MemberFieldCount = 5;

        static readonly Regex WholeNumberPattern = new Regex("^-?[0-9]+$");

        static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,                               // two-space indent
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  // Id -> "id"
        };

        // Digits with an optional minus sign, nothing else. int.TryParse alone would also accept " 4" and
        // "+4", which other languages reject, so the pattern is checked first.
        static int WholeNumber(string text, string name)
        {
            if (!WholeNumberPattern.IsMatch(text) || !int.TryParse(text, out int value))
            {
                throw new ArgumentException($"{name} must be a whole number, got '{text}'");
            }
            return value;
        }

        // =============================================================================================
        // READING CSV
        // ---------------------------------------------------------------------------------------------
        // File.ReadAllLines reads UTF-8 and copes with \n and \r\n line endings. Line numbers count
        // from 1, and the header is line 1.
        // ---------------------------------------------------------------------------------------------

        public static LoadResult LoadBooksCsv(string path)
        {
            LoadResult result = new LoadResult();
            HashSet<int> seen = new HashSet<int>();
            string[] lines = File.ReadAllLines(path);   // throws FileNotFoundException
            for (int i = 1; i < lines.Length; i++)
            {
                int lineNumber = i + 1;
                if (lines[i].Length == 0)   // a blank line: skip it silently
                {
                    continue;
                }
                string[] row = lines[i].Split(',');
                if (row.Length != BookFieldCount)
                {
                    result.Skip(lineNumber, $"expected {BookFieldCount} fields, found {row.Length}");
                    continue;
                }
                Book book;
                try
                {
                    WholeNumber(row[0], "id");
                    WholeNumber(row[3], "year");
                    WholeNumber(row[5], "copies");
                    WholeNumber(row[7], "length");
                    book = BookFactory.MakeBook(row);   // the format, then the Book rules
                }
                catch (ArgumentException error)
                {
                    result.Skip(lineNumber, error.Message);
                    continue;
                }
                if (!seen.Add(book.Id))   // Add returns false when the id is already in the set
                {
                    result.Skip(lineNumber, $"duplicate book id {book.Id}");
                    continue;
                }
                result.Books.Add(book);
            }
            return result;
        }

        public static LoadResult LoadMembersCsv(string path)
        {
            LoadResult result = new LoadResult();
            HashSet<int> seen = new HashSet<int>();
            string[] lines = File.ReadAllLines(path);
            for (int i = 1; i < lines.Length; i++)
            {
                int lineNumber = i + 1;
                if (lines[i].Length == 0)
                {
                    continue;
                }
                string[] row = lines[i].Split(',');
                if (row.Length != MemberFieldCount)
                {
                    result.Skip(lineNumber, $"expected {MemberFieldCount} fields, found {row.Length}");
                    continue;
                }
                Member member;
                try
                {
                    member = new Member(WholeNumber(row[0], "id"), row[1], row[2], row[3], row[4]);
                }
                catch (ArgumentException error)
                {
                    result.Skip(lineNumber, error.Message);
                    continue;
                }
                if (!seen.Add(member.Id))
                {
                    result.Skip(lineNumber, $"duplicate member id {member.Id}");
                    continue;
                }
                result.Members.Add(member);
            }
            return result;
        }

        // =============================================================================================
        // WRITING CSV AND JSON
        // =============================================================================================

        static BookRecord ToRecord(Book book)
        {
            return new BookRecord(book.Id, book.Title, book.Author, book.Year, book.Genre, book.Copies, book.FormatName, book.Length);
        }

        static MemberRecord ToRecord(Member member)
        {
            return new MemberRecord(member.Id, member.Name, member.Email, member.Type, member.Joined, new List<int>(member.Loans));
        }

        public static void SaveBooksCsv(Library library, string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);   // does nothing if it is already there
            List<string> lines = new List<string> { BookHeader };
            foreach (Book b in library.AllBooks())
            {
                lines.Add($"{b.Id},{b.Title},{b.Author},{b.Year},{b.Genre},{b.Copies},{b.FormatName},{b.Length}");
            }
            // "\n" on every operating system: the same bytes on Windows, macOS and Linux.
            // File.WriteAllText writes UTF-8 and closes the file for us, even if something goes wrong.
            File.WriteAllText(path, string.Join("\n", lines) + "\n");
        }

        public static void SaveJson(Library library, string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);
            LibraryDocument document = new LibraryDocument(
                library.AllBooks().Select(ToRecord).ToList(),
                library.AllMembers().Select(ToRecord).ToList());
            string json = JsonSerializer.Serialize(document, JsonOptions).Replace("\r\n", "\n");
            File.WriteAllText(path, json + "\n");
        }

        // Throws FileNotFoundException, or InvalidDataException when the file is not a library.
        public static LoadResult LoadJson(string path)
        {
            string text = File.ReadAllText(path);
            LibraryDocument? document;
            try
            {
                document = JsonSerializer.Deserialize<LibraryDocument>(text, JsonOptions);
            }
            catch (JsonException)
            {
                throw new InvalidDataException("not valid JSON");
            }
            if (document == null || document.Books == null || document.Members == null)
            {
                throw new InvalidDataException("not a library file");
            }

            LoadResult result = new LoadResult();
            try
            {
                foreach (BookRecord b in document.Books)
                {
                    string[] fields = { $"{b.Id}", b.Title, b.Author, $"{b.Year}", b.Genre, $"{b.Copies}", b.Format, $"{b.Length}" };
                    result.Books.Add(BookFactory.MakeBook(fields));
                }
                foreach (MemberRecord m in document.Members)
                {
                    result.Members.Add(new Member(m.Id, m.Name, m.Email, m.Type, m.Joined));
                    foreach (int bookId in m.Loans ?? new List<int>())
                    {
                        result.Loans.Add((m.Id, bookId));
                    }
                }
            }
            catch (ArgumentException)
            {
                throw new InvalidDataException("not a library file");
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
            // Saved loans go back through Borrow(), so the copy counts and the limits are checked
            // exactly as if they were new loans
            foreach ((int memberId, int bookId) in result.Loans)
            {
                library.Borrow(memberId, bookId);
            }
            return library;
        }
    }
}
