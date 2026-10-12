// =====================================================================================================
// Task 07 - Errors done properly (solution, Core + Stretch)
// -----------------------------------------------------------------------------------------------------
// Session 7: exceptions. Builds on task 06. Reads commands, one per line, and never crashes:
//   borrow MEMBER_ID BOOK_ID | return MEMBER_ID BOOK_ID
//   loans MEMBER_ID          | quit
//
// Run:  dotnet run --project tasks/07_errors/solution < tasks/data/input_07.txt
//       dotnet run --project tasks/07_errors/solution -- --stretch < tasks/data/input_07.txt
//       dotnet run --project tasks/07_errors/solution -- --stretch --debug < tasks/data/input_07.txt
// (In PowerShell: Get-Content tasks/data/input_07.txt | dotnet run --project tasks/07_errors/solution)
// -----------------------------------------------------------------------------------------------------

using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace LibraryManager
{
    internal class Program
    {
        static readonly string[] BookLines =
        {
            "id,title,author,year,genre,copies,format,length",
            "1,Pride and Prejudice,Jane Austen,1813,Classic,3,print,432",
            "2,Nineteen Eighty-Four,George Orwell,1949,Dystopian,2,print,328",
            "3,The Hobbit,J. R. R. Tolkien,1937,Fantasy,0,print,310",
            "4,Dune,Frank Herbert,1965,Science Fiction,4,print,412",
            "5,Robinson Crusoe,Daniel Defoe,1719,Adventure,1,print,320",
            "6,The Time Machine,H. G. Wells,1895,Science Fiction,2,ebook,118",
            "7,Animal Farm,George Orwell,1945,Dystopian,5,print,112",
            "8,Emma,Jane Austen,1815,Classic,1,audio,1150",
            "9,The Left Hand of Darkness,Ursula K. Le Guin,1969,Science Fiction,0,print,304",
            "10,Murder on the Orient Express,Agatha Christie,1934,Crime,2,print,256",
            "11,The Murder of Roger Ackroyd,Agatha Christie,1926,Crime,1,ebook,312",
            "12,A Wizard of Earthsea,Ursula K. Le Guin,1968,Fantasy,2,print,183",
            "13,Brave New World,Aldous Huxley,1932,Dystopian,2,audio,480",
            "14,The Martian,Andy Weir,2011,Science Fiction,3,print,369",
            "15,Small Island,Andrea Levy,2004,Historical,1,print,441",
            "16,Wolf Hall,Hilary Mantel,2009,Historical,2,audio,1440",
            "17,The Remains of the Day,Kazuo Ishiguro,1989,Literary,2,print,258",
            "18,Never Let Me Go,Kazuo Ishiguro,2005,Literary,1,ebook,288",
            "19,Frankenstein,Mary Shelley,1818,Gothic,2,print,280",
            "20,The War of the Worlds,H. G. Wells,1898,Science Fiction,3,audio,410",
        };

        static readonly string[] MemberLines =
        {
            "id,name,email,type,joined",
            "101,Amira Khan,amira.khan@example.com,standard,2023-01-15",
            "102,Ben Taylor,ben.taylor@example.com,student,2024-09-02",
            "103,Chloe Evans,chloe.evans@example.com,staff,2019-05-20",
            "104,Dev Patel,dev.patel@example.com,student,2025-09-10",
            "105,Ella Brown,ella.brown@example.com,standard,2021-03-08",
            "106,Femi Adeyemi,femi.adeyemi@example.com,standard,2022-11-30",
            "107,Grace Wilson,grace.wilson@example.com,staff,2018-07-01",
            "108,Harry Jones,harry.jones@example.com,student,2026-01-12",
            "109,Ben Taylor,ben.taylor2@example.com,standard,2025-02-14",
        };

        // A whole number is an optional minus sign and the digits 0 to 9, nothing else.
        static readonly Regex WholeNumber = new Regex("^-?[0-9]+$");

        static Library LoadLibrary()
        {
            Library library = new Library();
            foreach (string line in BookLines.Skip(1))
            {
                library.AddBook(BookFactory.MakeBook(line.Split(',')));
            }
            foreach (string line in MemberLines.Skip(1))
            {
                string[] f = line.Split(',');
                library.AddMember(new Member(int.Parse(f[0]), f[1], f[2], f[3], f[4]));
            }
            return library;
        }

        // =============================================================================================
        // TURNING TEXT INTO COMMANDS
        // =============================================================================================

        // "4" -> 4. Anything else becomes a CommandException we wrote. int.TryParse alone would also
        // accept " 4" and "+4", so the pattern is checked first. int.Parse is used (not TryParse) so the
        // runtime's own FormatException or OverflowException can be kept as the cause (Stretch 2).
        static int ParseId(string text, string what)
        {
            try
            {
                if (!WholeNumber.IsMatch(text))
                {
                    throw new FormatException($"'{text}' is not digits with an optional minus sign");
                }
                return int.Parse(text, CultureInfo.InvariantCulture);   // OverflowException if it is too big
            }
            catch (Exception error) when (error is FormatException || error is OverflowException)
            {
                // The second argument keeps the original exception as the InnerException
                throw new CommandException($"{what} must be a whole number, got '{text}'", error);
            }
        }

        // Do one command and return the line to print. Throws LibraryException (or a subclass) when
        // the command cannot be done.
        static string RunCommand(Library library, string[] words)
        {
            string command = words[0].ToLowerInvariant();
            string[] args = words.Skip(1).ToArray();

            switch (command)
            {
                case "borrow":
                case "return":
                {
                    if (args.Length != 2)
                    {
                        throw new CommandException($"usage: {command} MEMBER_ID BOOK_ID");
                    }
                    int memberId = ParseId(args[0], "member id");
                    int bookId = ParseId(args[1], "book id");
                    if (command == "borrow")
                    {
                        (Member member, Book book) = library.Borrow(memberId, bookId);
                        return $"OK: {member.Name} borrowed {book.Title}, due in {member.Policy.LoanDays()} days";
                    }
                    (Member returner, Book returned) = library.ReturnBook(memberId, bookId);
                    return $"OK: {returner.Name} returned {returned.Title}";
                }
                case "loans":
                {
                    if (args.Length != 1)
                    {
                        throw new CommandException("usage: loans MEMBER_ID");
                    }
                    Member member = library.GetMember(ParseId(args[0], "member id"));
                    if (member.Loans.Count == 0)
                    {
                        return $"{member.Name} has no loans";
                    }
                    List<string> titles = member.Loans.Select(bookId => library.GetBook(bookId).Title).ToList();
                    return $"{member.Name} has {Formatting.Plural(titles.Count, "loan", "loans")}: {string.Join(", ", titles)}";
                }
                default:
                    throw new CommandException($"unknown command '{words[0]}'");
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool stretch = args.Contains("--stretch");
            bool debug = args.Contains("--debug");

            Library library = LoadLibrary();
            Console.WriteLine($"Library Manager: {library.BookCount} books, {library.MemberCount} members");
            int commands = 0;
            int ok = 0;
            int errors = 0;
            try
            {
                string? line;
                while ((line = Console.ReadLine()) != null)   // null at the end of the input
                {
                    // Split on any run of spaces; RemoveEmptyEntries drops the empty pieces
                    string[] words = line.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
                    if (words.Length == 0)   // blank lines are skipped without a word
                    {
                        continue;
                    }
                    Console.WriteLine($"> {string.Join(" ", words)}");
                    if (words[0].ToLowerInvariant() == "quit")
                    {
                        break;
                    }
                    commands++;
                    try
                    {
                        Console.WriteLine(RunCommand(library, words));
                        ok++;
                    }
                    catch (LibraryException error)   // the specific type first...
                    {
                        errors++;
                        Console.WriteLine($"Error: {error.Message}");
                        if (debug && error.InnerException != null)
                        {
                            // Stretch 2: the chained exception
                            Console.WriteLine($"  cause: {error.InnerException.GetType().Name}: {error.InnerException.Message}");
                        }
                    }
                    catch (Exception)   // ...then anything else: a bug of ours. Report it and keep going
                    {
                        errors++;
                        Console.WriteLine("Error: unexpected problem");
                    }
                }
            }
            finally
            {
                // finally runs however the loop ends: quit, the end of the input or an exception
                if (stretch)
                {
                    Console.WriteLine($"Session: {commands} commands, {ok} OK, {errors} errors");
                }
                Console.WriteLine("Goodbye");
            }
        }
    }
}
