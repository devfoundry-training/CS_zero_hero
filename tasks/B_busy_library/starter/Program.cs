// =====================================================================================================
// Task B - Busy library (starter)
// -----------------------------------------------------------------------------------------------------
// Bonus session: concurrency. The brief is in ../README.md.
//
// Run:    dotnet run --project tasks/B_busy_library/starter
// Check:  python tasks/check/check.py B -- dotnet run --project tasks/B_busy_library/starter
// Run the check several times: a race that is only sometimes wrong is still wrong.
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    // The copies of one book, shared by every thread.
    public class Shelf
    {
        public int Copies { get; }
        public int Available { get; private set; }
        public int LoansRecorded { get; private set; }

        public Shelf(int copies)
        {
            Copies = copies;
            Available = copies;
        }

        // TODO (Core 1): if a copy is available, take it, record a loan and return true; otherwise
        // return false. The whole check-and-act goes inside one lock (lock (gate) { ... } on a private
        // field such as: private readonly Lock gate = new Lock();), so only one thread at a time can be in it.
        public bool Borrow()
        {
            throw new NotImplementedException("Core 1: Shelf.Borrow");
        }

        // TODO (Core 1): put a copy back, under the same lock.
        public void GiveBack()
        {
            throw new NotImplementedException("Core 1: Shelf.GiveBack");
        }
    }

    internal class Program
    {
        const int Members1 = 8;
        const int Copies1 = 3;
        const int Members2 = 4;
        const int Copies2 = 4;
        const int Cycles = 5000;

        // TODO (Core 2): Members1 threads, one Shelf with Copies1 copies. Every thread waits at a Barrier
        // so they all try at once, then borrows once. Join every thread, then return the counts.
        static (int Borrowed, int Refused, int Available) Scenario1()
        {
            throw new NotImplementedException("Core 2: Scenario1");
        }

        // TODO (Core 3): Members2 members (Task.Run), one Shelf with Copies2 copies, each doing Cycles
        // borrow-then-give-back cycles. Task.WaitAll, then return the loans recorded and the copies left.
        static (int Loans, int Available) Scenario2()
        {
            throw new NotImplementedException("Core 3: Scenario2");
        }

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Busy library");
                Console.WriteLine($"Scenario 1: {Members1} members, 1 book with {Copies1} copies, everyone borrows at once");
                (int borrowed, int refused, int available1) = Scenario1();
                Console.WriteLine($"borrowed: {borrowed}");
                Console.WriteLine($"refused: {refused}");
                Console.WriteLine($"available at end: {available1}");

                Console.WriteLine($"Scenario 2: {Members2} members, {Copies2} copies, {Cycles} borrow-and-return cycles each");
                (int loans, int available2) = Scenario2();
                Console.WriteLine($"loans recorded: {loans}");
                Console.WriteLine($"available at end: {available2}");

                bool ok = borrowed == Copies1 && available1 == 0 && loans == Members2 * Cycles && available2 == Copies2;
                Console.WriteLine($"Result: {(ok ? "OK" : "WRONG")}");
            }
            catch (NotImplementedException error)
            {
                Console.WriteLine($"not implemented: {error.Message}");
            }
        }
    }
}
