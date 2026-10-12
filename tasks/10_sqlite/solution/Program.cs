// =====================================================================================================
// Task 10 - SQLite storage (solution, Core + Stretch)
// -----------------------------------------------------------------------------------------------------
// Session 10: databases. Builds on task 09: the CSV files are imported once into out/library.db
// (rebuilt on every run from data/schema.sql), and from then on the database is the store. The new
// work is in LibraryDb.cs.
//
// Run (from the repo root):
//       dotnet run --project tasks/10_sqlite/solution -- tasks/data [TODAY]              (Core, checked)
//       dotnet run --project tasks/10_sqlite/solution -- tasks/data [TODAY] --stretch    (Core, then Stretch)
// -----------------------------------------------------------------------------------------------------

using System.Text;

namespace LibraryManager
{
    internal class Program
    {
        static readonly DateOnly DefaultToday = new DateOnly(2026, 10, 12);
        static readonly string DbPath = Path.Combine("out", "library.db");

        // Exactly the task 09 report, now fed from the database.
        static void OverdueReport(Library library, DateOnly today)
        {
            List<Loan> overdue = library.Overdue(today);
            Console.WriteLine($"Overdue on {Formatting.Iso(today)}: {Formatting.Plural(overdue.Count, "loan", "loans")}");
            foreach (Loan loan in overdue)
            {
                Book book = library.GetBook(loan.BookId);
                Member member = library.GetMember(loan.MemberId);
                Console.WriteLine($"{Formatting.Plural(library.DaysLate(loan, today), "day", "days")}: {book.Title}, "
                                  + $"{member.Name} ({member.Type}), due {Formatting.Iso(library.DueDate(loan))}, "
                                  + $"fine {Formatting.Money(library.Fine(loan, today))}");
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string[] positional = args.Where(a => !a.StartsWith("--")).ToArray();
            string data = positional.Length > 0 ? positional[0] : Path.Combine("tasks", "data");
            DateOnly today = positional.Length > 1 ? Formatting.ParseIso(positional[1]) : DefaultToday;

            // =========================================================================================
            // CORE 1: A FRESH DATABASE FROM schema.sql, FILLED FROM THE CSV FILES
            // =========================================================================================

            Directory.CreateDirectory("out");
            File.Delete(DbPath);   // start clean so every run is the same (no error if it is not there)
            using LibraryDb db = new LibraryDb(DbPath);
            db.Create(Path.Combine(data, "schema.sql"));
            Console.WriteLine("Created out/library.db from schema.sql");

            LoadResult rows = new LoadResult();
            Storage.LoadBooksCsv(Path.Combine(data, "books.csv"), rows);
            Storage.LoadMembersCsv(Path.Combine(data, "members.csv"), rows);
            Storage.LoadLoansCsv(Path.Combine(data, "loans.csv"), rows);
            db.ImportRows(rows);
            Console.WriteLine($"Imported {db.Count("books")} books, {db.Count("members")} members, {db.Count("loans")} loans");

            // =========================================================================================
            // CORE 2: QUESTIONS ANSWERED IN SQL
            // =========================================================================================

            Console.WriteLine("Books per genre");
            foreach ((string genre, int count) in db.GenreCounts())
            {
                Console.WriteLine($"{genre}: {count}");
            }

            Console.WriteLine("Nothing on the shelf");
            foreach ((int id, string title, int available, int copies) in db.UnavailableBooks())
            {
                Console.WriteLine($"#{id} {title}: {available} of {copies} available");
            }

            foreach (string text in new[] { "the", "x' OR '1'='1" })
            {
                List<(int Id, string Title, int Year)> found = db.SearchTitles(text);
                Console.WriteLine($"Search \"{text}\": {Formatting.Plural(found.Count, "book", "books")}");
                foreach ((int id, string title, int year) in found)
                {
                    Console.WriteLine($"  #{id} {title} ({year})");
                }
            }

            Console.WriteLine("Open loans per member");
            foreach ((int id, string name, int open) in db.OpenLoansPerMember())
            {
                Console.WriteLine($"#{id} {name}: {open}");
            }

            // =========================================================================================
            // CORE 3: THE TASK 09 RULES DECIDE, THE DATABASE REMEMBERS
            // =========================================================================================

            Library library = db.LoadLibrary();
            Console.WriteLine($"New loans on {Formatting.Iso(today)}");
            (string Action, int MemberId, int BookId)[] script = { ("borrow", 105, 15), ("borrow", 105, 16), ("return", 102, 14) };
            foreach ((string action, int memberId, int bookId) in script)
            {
                try
                {
                    if (action == "borrow")
                    {
                        Loan loan = library.Borrow(memberId, bookId, today);
                        db.InsertLoan(loan);
                        Console.WriteLine($"OK: {library.GetMember(memberId).Name} borrowed {library.GetBook(bookId).Title}, "
                                          + $"due {Formatting.Iso(library.DueDate(loan))}, saved as loan {loan.Id}");
                    }
                    else
                    {
                        Loan loan = library.ReturnBook(memberId, bookId, today);
                        db.MarkReturned(loan);
                        Console.WriteLine($"OK: {library.GetMember(memberId).Name} returned {library.GetBook(bookId).Title}, "
                                          + $"fine {Formatting.Money(library.Fine(loan, today))}, saved");
                    }
                }
                catch (LibraryException error)
                {
                    Console.WriteLine($"Error: {error.Message}");
                }
            }

            // Read everything back from the database to prove it was saved
            Library reloaded = db.LoadLibrary();
            int openLoans = reloaded.AllLoans().Count(x => x.IsOpen);
            Console.WriteLine($"Reloaded: {reloaded.BookCount} books, {reloaded.MemberCount} members, "
                              + $"{reloaded.LoanCount} loans ({openLoans} open)");
            OverdueReport(reloaded, today);

            // =========================================================================================
            // STRETCH: A BORROW THAT ROLLS BACK, AND AN INDEX
            // =========================================================================================

            if (args.Contains("--stretch"))
            {
                Console.WriteLine("Stretch");
                int before = db.Count("loans");
                int? refused = db.BorrowInTransaction(101, 3, today);
                Console.WriteLine($"borrow 101 3 in a transaction: {(refused == null ? "refused, rolled back" : "OK")}");
                Console.WriteLine($"loans before {before}, after {db.Count("loans")}");
                int? newId = db.BorrowInTransaction(101, 4, today);
                Console.WriteLine($"borrow 101 4 in a transaction: saved as loan {newId}");
                db.AddMemberIndex();
                Console.WriteLine("index idx_loans_member_id created on loans (member_id)");
            }
        }
    }
}
