// =====================================================================================================
// Task 08 - Storage: CSV and JSON in and out (starter)
// -----------------------------------------------------------------------------------------------------
// Loading never crashes on bad data. A bad line is reported with result.Skip(lineNumber, reason) and
// skipped; the good lines still load. A missing file throws FileNotFoundException for the caller.
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    // What a load produced: the good rows and the problems. (Done already.)
    public class LoadResult
    {
        public List<Book> Books { get; } = new List<Book>();
        public List<Member> Members { get; } = new List<Member>();
        public List<(int MemberId, int BookId)> Loans { get; } = new List<(int MemberId, int BookId)>();   // from JSON (Stretch)
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
        // TODO (Core 1 and 5): read books.csv with File.ReadAllLines. For each line after the header
        // (line 1), in this order: skip a blank line; check there are 8 fields; check id, year, copies
        // and length are whole numbers ("year must be a whole number, got 'TEXT'"); build the book with
        // BookFactory.MakeBook (it checks the format and the Book rules: catch ArgumentException); refuse
        // an id already loaded ("duplicate book id ID").
        public static LoadResult LoadBooksCsv(string path)
        {
            throw new NotImplementedException("Core 1: Storage.LoadBooksCsv");
        }

        // TODO (Core 1): the same for members.csv (5 fields: id,name,email,type,joined).
        public static LoadResult LoadMembersCsv(string path)
        {
            throw new NotImplementedException("Core 1: Storage.LoadMembersCsv");
        }

        // TODO (Core 2): the same header and columns as data/books.csv, "\n" line endings. Create the
        // folder first with Directory.CreateDirectory.
        public static void SaveBooksCsv(Library library, string path)
        {
            throw new NotImplementedException("Core 2: Storage.SaveBooksCsv");
        }

        // TODO (Core 2): {"books": [...], "members": [...]} in id order, two-space indent, keys in the
        // order shown in the brief. System.Text.Json: JsonSerializer.Serialize(value, new
        // JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase }).
        public static void SaveJson(Library library, string path)
        {
            throw new NotImplementedException("Core 2: Storage.SaveJson");
        }

        // Done already: a Library from the loaded rows. Saved loans (from JSON, Stretch 3) go back
        // through Borrow(), so the copy counts and the limits are checked as if they were new loans.
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
            foreach ((int memberId, int bookId) in result.Loans)
            {
                library.Borrow(memberId, bookId);
            }
            return library;
        }
    }
}
