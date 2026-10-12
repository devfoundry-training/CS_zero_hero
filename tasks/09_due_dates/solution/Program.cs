// =====================================================================================================
// Task 09 - Loans with due dates (solution, Core + Stretch)
// -----------------------------------------------------------------------------------------------------
// Session 9: dates and times. Builds on task 08. "Today" is an input, never the real clock, so the
// output is the same whenever and wherever it runs.
//
// Run (from the repo root):
//       dotnet run --project tasks/09_due_dates/solution -- tasks/data [TODAY]              (Core, checked)
//       dotnet run --project tasks/09_due_dates/solution -- tasks/data [TODAY] --stretch    (Core, then Stretch)
// The data folder defaults to tasks/data, TODAY to 2026-10-12.
// -----------------------------------------------------------------------------------------------------

using System.Text;

namespace LibraryManager
{
    internal class Program
    {
        static readonly DateOnly DefaultToday = new DateOnly(2026, 10, 12);

        static void LoansTable(Library library, DateOnly today)
        {
            Console.WriteLine("Loans");
            Console.WriteLine($"{"ID",2}  {"Book",-30}  {"Member",-12}  {"Borrowed",-10}  {"Due",-10}  Status");
            Console.WriteLine($"{new string('-', 2)}  {new string('-', 30)}  {new string('-', 12)}  {new string('-', 10)}  {new string('-', 10)}  {new string('-', 6)}");
            foreach (Loan loan in library.AllLoans())
            {
                Book book = library.GetBook(loan.BookId);
                Member member = library.GetMember(loan.MemberId);
                Console.WriteLine($"{loan.Id,2}  {book.Title,-30}  {member.Name,-12}  {Formatting.Iso(loan.Borrowed)}  "
                                  + $"{Formatting.Iso(library.DueDate(loan))}  {library.Status(loan, today)}");
            }
        }

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

        static void FinesReport(Library library, DateOnly today)
        {
            int onOverdue = library.AllLoans().Where(x => x.IsOpen).Sum(x => library.Fine(x, today));
            int onReturns = library.AllLoans().Where(x => !x.IsOpen).Sum(x => library.Fine(x, today));
            Console.WriteLine("Fines");
            Console.WriteLine($"on overdue loans: {Formatting.Money(onOverdue)}");
            Console.WriteLine($"on late returns: {Formatting.Money(onReturns)}");
        }

        static void NewLoans(Library library, DateOnly today)
        {
            Console.WriteLine($"New loans on {Formatting.Iso(today)}");
            (string Action, int MemberId, int BookId)[] script = { ("borrow", 105, 15), ("borrow", 105, 16), ("return", 102, 14) };
            foreach ((string action, int memberId, int bookId) in script)
            {
                try
                {
                    string name = library.GetMember(memberId).Name;
                    string title = library.GetBook(bookId).Title;
                    if (action == "borrow")
                    {
                        Loan loan = library.Borrow(memberId, bookId, today);
                        Console.WriteLine($"OK: {name} borrowed {title}, due {Formatting.Iso(library.DueDate(loan))}");
                    }
                    else
                    {
                        Loan loan = library.ReturnBook(memberId, bookId, today);
                        int late = library.DaysLate(loan, today);
                        string tail = late == 0
                            ? "on time"
                            : $"{Formatting.Plural(late, "day", "days")} late, fine {Formatting.Money(library.Fine(loan, today))}";
                        Console.WriteLine($"OK: {name} returned {title}, {tail}");
                    }
                }
                catch (LibraryException error)
                {
                    Console.WriteLine($"Error: {error.Message}");
                }
            }
        }

        // =============================================================================================
        // STRETCH: CLOSED ON SUNDAYS, AND WHAT IS DUE THIS WEEK
        // =============================================================================================

        // The library is closed on Sundays: a Sunday due date moves to the Monday.
        static DateOnly OpenDay(DateOnly day)
        {
            return day.DayOfWeek == DayOfWeek.Sunday ? day.AddDays(1) : day;
        }

        static void Stretch(Library library, DateOnly today)
        {
            Console.WriteLine("Stretch");
            foreach (Loan loan in library.AllLoans())
            {
                DateOnly due = library.DueDate(loan);
                if (OpenDay(due) != due)
                {
                    Console.WriteLine($"loan {loan.Id}: due {Formatting.Iso(due)} is a Sunday, moves to {Formatting.Iso(OpenDay(due))}");
                }
            }
            DateOnly weekEnd = today.AddDays(7);
            IEnumerable<string> dueSoon = library.AllLoans()
                .Where(x => x.IsOpen && library.DueDate(x) >= today && library.DueDate(x) <= weekEnd)
                .Select(x => library.GetBook(x.BookId).Title);
            Console.WriteLine($"Due by {Formatting.Iso(weekEnd)}: {string.Join(", ", dueSoon)}");
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string[] positional = args.Where(a => !a.StartsWith("--")).ToArray();
            string data = positional.Length > 0 ? positional[0] : Path.Combine("tasks", "data");
            DateOnly today = positional.Length > 1 ? Formatting.ParseIso(positional[1]) : DefaultToday;

            Library library = Storage.LoadAll(data);
            Console.WriteLine($"Today is {Formatting.Iso(today)}");
            LoansTable(library, today);
            OverdueReport(library, today);
            FinesReport(library, today);
            NewLoans(library, today);
            if (args.Contains("--stretch"))
            {
                Stretch(library, today);
            }
        }
    }
}
