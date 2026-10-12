// =====================================================================================================
// Task B - Busy library (solution, Core + Stretch)
// -----------------------------------------------------------------------------------------------------
// Bonus session: concurrency. Many members borrow at once.
//
// The race: borrowing is "check, then act":
//     if (shelf.Available > 0)      // 1. check
//         shelf.Available--;        // 2. act
// Two threads can both pass step 1 before either does step 2, and the library lends a copy it does
// not have. Even Available-- on its own is read, subtract, write: two threads can read the same value
// and one update is lost.
//
// The fix: a lock around the whole check-and-act, so only one thread at a time can be inside it.
//
// Run:  dotnet run --project tasks/B_busy_library/solution                 (Core: the safe version, checked)
//       dotnet run --project tasks/B_busy_library/solution -- --stretch    (Core, then the race without the lock)
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    // The copies of one book, shared by every thread.
    public class Shelf
    {
        public int Copies { get; }
        public int Available { get; private set; }
        public int LoansRecorded { get; private set; }

        // A private lock means no other code can take it by accident. System.Threading.Lock (.NET 9 and
        // later) is the type made for this; on older .NET any object works: new object()
        private readonly Lock gate = new Lock();

        public Shelf(int copies)
        {
            Copies = copies;
            Available = copies;
        }

        // =============================================================================================
        // THE FIX: CHECK AND ACT TOGETHER, UNDER ONE LOCK
        // =============================================================================================

        public bool Borrow()
        {
            lock (gate)   // one thread at a time from here...
            {
                if (Available > 0)
                {
                    Available--;
                    LoansRecorded++;
                    return true;
                }
                return false;
            }   // ...to here (the lock is released however the block ends)
        }

        public void GiveBack()
        {
            lock (gate)
            {
                Available++;
            }
        }

        // =============================================================================================
        // THE RACE (STRETCH ONLY): THE SAME CODE WITH NO LOCK
        // ---------------------------------------------------------------------------------------------
        // Thread.Yield() lets another thread run at the worst moment, as a busy machine would.
        // ---------------------------------------------------------------------------------------------

        public bool BorrowUnsafe()
        {
            if (Available > 0)
            {
                Thread.Yield();
                Available--;
                int count = LoansRecorded;
                Thread.Yield();
                LoansRecorded = count + 1;
                return true;
            }
            return false;
        }

        public void GiveBackUnsafe()
        {
            int value = Available;
            Thread.Yield();
            Available = value + 1;
        }
    }

    internal class Program
    {
        const int Members1 = 8;
        const int Copies1 = 3;
        const int Members2 = 4;
        const int Copies2 = 4;
        const int Cycles = 5000;

        // Members1 threads each try once to borrow from Copies1 copies. A Barrier holds every thread
        // until all of them are ready, so they really do arrive at the same moment.
        static (int Borrowed, int Refused, int Available) Scenario1(Func<Shelf, bool> borrow)
        {
            Shelf shelf = new Shelf(Copies1);
            using Barrier start = new Barrier(Members1);
            bool[] results = new bool[Members1];   // each thread writes only its own slot

            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < Members1; i++)
            {
                int member = i;   // a fresh copy for each thread to capture
                threads.Add(new Thread(() =>
                {
                    start.SignalAndWait();
                    results[member] = borrow(shelf);
                }));
            }
            foreach (Thread thread in threads)
            {
                thread.Start();
            }
            foreach (Thread thread in threads)
            {
                thread.Join();   // wait for every thread before reading the results
            }
            int borrowed = results.Count(ok => ok);
            return (borrowed, Members1 - borrowed, shelf.Available);
        }

        // Members2 members each borrow and return Cycles times. Task.Run hands each member to the thread
        // pool (Stretch 2) and Task.WaitAll waits for all of them to finish.
        static (int Loans, int Available) Scenario2(Func<Shelf, bool> borrow, Action<Shelf> giveBack)
        {
            Shelf shelf = new Shelf(Copies2);
            Task[] members = new Task[Members2];
            for (int i = 0; i < Members2; i++)
            {
                members[i] = Task.Run(() =>
                {
                    for (int cycle = 0; cycle < Cycles; cycle++)
                    {
                        if (borrow(shelf))
                        {
                            giveBack(shelf);
                        }
                    }
                });
            }
            Task.WaitAll(members);
            return (shelf.LoansRecorded, shelf.Available);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Busy library");
            Console.WriteLine($"Scenario 1: {Members1} members, 1 book with {Copies1} copies, everyone borrows at once");
            (int borrowed, int refused, int available1) = Scenario1(shelf => shelf.Borrow());
            Console.WriteLine($"borrowed: {borrowed}");
            Console.WriteLine($"refused: {refused}");
            Console.WriteLine($"available at end: {available1}");

            Console.WriteLine($"Scenario 2: {Members2} members, {Copies2} copies, {Cycles} borrow-and-return cycles each");
            (int loans, int available2) = Scenario2(shelf => shelf.Borrow(), shelf => shelf.GiveBack());
            Console.WriteLine($"loans recorded: {loans}");
            Console.WriteLine($"available at end: {available2}");

            bool ok = borrowed == Copies1 && available1 == 0 && loans == Members2 * Cycles && available2 == Copies2;
            Console.WriteLine($"Result: {(ok ? "OK" : "WRONG")}");

            if (args.Contains("--stretch"))
            {
                // Not checked: the numbers change from run to run, which is the point
                Console.WriteLine("Stretch: the same scenarios with no lock");
                (borrowed, _, available1) = Scenario1(shelf => shelf.BorrowUnsafe());
                Console.WriteLine($"scenario 1: borrowed {borrowed} of {Copies1} copies, available at end {available1}");
                (loans, available2) = Scenario2(shelf => shelf.BorrowUnsafe(), shelf => shelf.GiveBackUnsafe());
                Console.WriteLine($"scenario 2: loans recorded {loans} of {Members2 * Cycles}, available at end {available2}");
            }
        }
    }
}
