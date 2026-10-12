// =====================================================================================================
// Task 06 - Book types and loan policies (solution, Core + Stretch)
// -----------------------------------------------------------------------------------------------------
// Session 6: inheritance, interfaces, polymorphism. Builds on task 05:
//   * Book becomes abstract, with PrintBook, EBook and AudioBook (Book.cs)
//   * ILoanPolicy is an interface with three implementations, and each member gets the policy for
//     their member type (LoanPolicy.cs)
//
// Run:  dotnet run --project tasks/06_book_types/solution                 (Core output, checked)
//       dotnet run --project tasks/06_book_types/solution -- --stretch    (Core, then the Stretch)
// -----------------------------------------------------------------------------------------------------

using System.Text;

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

        static void Main(string[] args)
        {
            // The pound sign needs UTF-8 output, whatever the console's own setting is
            Console.OutputEncoding = Encoding.UTF8;
            Library library = LoadLibrary();

            Console.WriteLine("Formats");
            foreach (Book book in library.AllBooks())
            {
                Console.WriteLine(book.Describe());
            }

            Console.WriteLine("Policies");
            foreach (ILoanPolicy policy in Policies.All)
            {
                Console.WriteLine(policy.Describe());
            }

            Console.WriteLine("Fine for 3 days late");
            foreach (ILoanPolicy policy in Policies.All)
            {
                Console.WriteLine($"{policy.Name}: {Formatting.Money(policy.FineFor(3))}");
            }

            Console.WriteLine("Limits");
            (int MemberId, int BookId)[] borrows = { (101, 1), (101, 2), (101, 4), (101, 7), (102, 7), (103, 7) };
            foreach ((int memberId, int bookId) in borrows)
            {
                Member? member = library.FindMember(memberId);
                if (member != null && library.Borrow(memberId, bookId))
                {
                    Console.WriteLine($"borrow {memberId} {bookId}: OK, due in {member.Policy.LoanDays()} days");
                }
                else
                {
                    Console.WriteLine($"borrow {memberId} {bookId}: refused");
                }
            }

            if (args.Contains("--stretch"))
            {
                Console.WriteLine("Stretch");
                ILoanPolicy guest = new GuestPolicy();
                Console.WriteLine(guest.Describe());

                // No type checks: every book answers FormatName for itself
                Dictionary<string, int> counts = new Dictionary<string, int>();
                foreach (Book book in library.AllBooks())
                {
                    counts[book.FormatName] = counts.GetValueOrDefault(book.FormatName, 0) + 1;
                }
                string[] order = { "print", "ebook", "audio" };
                Console.WriteLine("formats: " + string.Join(", ", order.Select(name => $"{name} {counts[name]}")));
            }
        }
    }
}
