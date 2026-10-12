// =====================================================================================================
// Task 11 - Test the rules (solution, Core + Stretch)
// -----------------------------------------------------------------------------------------------------
// Session 11: testing with MSTest. Every test class builds a fresh library in [TestInitialize], which
// runs before EACH test, so no test depends on another one or on the order they run in. Dates are fixed
// values: nothing reads the real clock.
//
// Run (from the repo root):
//       dotnet test tasks/11_testing/solution --logger "console;verbosity=detailed"
// The detailed logger prints the name of every test it runs.
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    // The fixture: books 1-8 of books.csv and members 101-103, no loans
    public static class Fixture
    {
        public const int Amira = 101;   // standard
        public const int Ben = 102;     // student
        public const int Chloe = 103;   // staff

        public static readonly DateOnly Today = new DateOnly(2026, 10, 12);

        static readonly string[] Books =
        {
            "1,Pride and Prejudice,Jane Austen,1813,Classic,3,print,432",
            "2,Nineteen Eighty-Four,George Orwell,1949,Dystopian,2,print,328",
            "3,The Hobbit,J. R. R. Tolkien,1937,Fantasy,0,print,310",
            "4,Dune,Frank Herbert,1965,Science Fiction,4,print,412",
            "5,Robinson Crusoe,Daniel Defoe,1719,Adventure,1,print,320",
            "6,The Time Machine,H. G. Wells,1895,Science Fiction,2,ebook,118",
            "7,Animal Farm,George Orwell,1945,Dystopian,5,print,112",
            "8,Emma,Jane Austen,1815,Classic,1,audio,1150",
        };

        public static Library Build()
        {
            Library library = new Library();
            foreach (string line in Books)
            {
                library.AddBook(BookFactory.MakeBook(line.Split(',')));
            }
            library.AddMember(new Member(Amira, "Amira Khan", "amira.khan@example.com", "standard", "2023-01-15"));
            library.AddMember(new Member(Ben, "Ben Taylor", "ben.taylor@example.com", "student", "2024-09-02"));
            library.AddMember(new Member(Chloe, "Chloe Evans", "chloe.evans@example.com", "staff", "2019-05-20"));
            return library;
        }
    }

    // =================================================================================================
    // BORROWING
    // =================================================================================================

    [TestClass]
    public class BorrowingTests
    {
        private Library library = null!;   // set before every test by Setup()

        [TestInitialize]
        public void Setup()
        {
            library = Fixture.Build();
        }

        [TestMethod]
        public void BorrowReducesAvailable()
        {
            library.Borrow(Fixture.Amira, 4, Fixture.Today);
            Assert.AreEqual(3, library.GetBook(4).Available);
        }

        [TestMethod]
        public void ReturnRestoresAvailable()
        {
            library.Borrow(Fixture.Amira, 4, Fixture.Today);
            library.ReturnBook(Fixture.Amira, 4, Fixture.Today);
            Assert.AreEqual(4, library.GetBook(4).Available);
        }

        [TestMethod]
        public void BorrowUnknownBookRaises()
        {
            Assert.ThrowsExactly<UnknownBookException>(() => library.Borrow(Fixture.Amira, 99, Fixture.Today));
        }

        [TestMethod]
        public void BorrowUnknownMemberRaises()
        {
            Assert.ThrowsExactly<UnknownMemberException>(() => library.Borrow(999, 1, Fixture.Today));
        }

        [TestMethod]
        public void BorrowWithNoCopiesRaises()
        {
            Assert.ThrowsExactly<BookNotAvailableException>(() => library.Borrow(Fixture.Amira, 3, Fixture.Today));
        }

        [TestMethod]
        public void ReturnBookNotOnLoanRaises()
        {
            Assert.ThrowsExactly<NotOnLoanException>(() => library.ReturnBook(Fixture.Amira, 4, Fixture.Today));
        }

        [TestMethod]
        public void StandardLimitIsThreeLoans()
        {
            foreach (int bookId in new[] { 1, 2, 4 })
            {
                library.Borrow(Fixture.Amira, bookId, Fixture.Today);
            }
            Assert.ThrowsExactly<MemberLimitReachedException>(() => library.Borrow(Fixture.Amira, 7, Fixture.Today));
        }

        [TestMethod]
        public void StudentLimitIsFiveLoans()
        {
            foreach (int bookId in new[] { 1, 2, 4, 6, 7 })
            {
                library.Borrow(Fixture.Ben, bookId, Fixture.Today);
            }
            Assert.ThrowsExactly<MemberLimitReachedException>(() => library.Borrow(Fixture.Ben, 8, Fixture.Today));
        }
    }

    // =================================================================================================
    // DATES AND FINES (EACH TEST BUILDS A LOAN DIRECTLY WITH THE DATES IT NEEDS)
    // =================================================================================================

    [TestClass]
    public class DateAndFineTests
    {
        private Library library = null!;

        [TestInitialize]
        public void Setup()
        {
            library = Fixture.Build();
        }

        private DateOnly Due(int memberId, DateOnly borrowed)
        {
            return library.DueDate(new Loan(1, 1, memberId, borrowed));
        }

        [TestMethod]
        public void DueDateStandardIs14Days()
        {
            Assert.AreEqual(new DateOnly(2026, 10, 26), Due(Fixture.Amira, new DateOnly(2026, 10, 12)));
        }

        [TestMethod]
        public void DueDateStudentIs21Days()
        {
            Assert.AreEqual(new DateOnly(2026, 11, 2), Due(Fixture.Ben, new DateOnly(2026, 10, 12)));
        }

        [TestMethod]
        public void DueDateStaffIs28Days()
        {
            Assert.AreEqual(new DateOnly(2026, 11, 9), Due(Fixture.Chloe, new DateOnly(2026, 10, 12)));
        }

        [TestMethod]
        public void DueDateCrossesMonthAndYearEnd()
        {
            Assert.AreEqual(new DateOnly(2027, 1, 10), Due(Fixture.Ben, new DateOnly(2026, 12, 20)));
        }

        [TestMethod]
        public void DueDateInLeapYear()
        {
            Assert.AreEqual(new DateOnly(2028, 3, 5), Due(Fixture.Amira, new DateOnly(2028, 2, 20)));
        }

        [TestMethod]
        public void NotOverdueOnDueDate()
        {
            Loan loan = new Loan(1, 1, Fixture.Amira, new DateOnly(2026, 9, 28));   // due 2026-10-12
            Assert.AreEqual(0, library.DaysLate(loan, Fixture.Today));
            Assert.AreEqual(0, library.Fine(loan, Fixture.Today));
        }

        [TestMethod]
        public void FineOneDayLateStandard()
        {
            Loan loan = new Loan(1, 1, Fixture.Amira, new DateOnly(2026, 9, 27));   // due 2026-10-11
            Assert.AreEqual(25, library.Fine(loan, Fixture.Today));
        }

        [TestMethod]
        public void FineTenDaysLateStudent()
        {
            Loan loan = new Loan(1, 1, Fixture.Ben, new DateOnly(2026, 9, 11));   // due 2026-10-02
            Assert.AreEqual(100, library.Fine(loan, Fixture.Today));
        }

        [TestMethod]
        public void StaffNeverFined()
        {
            Loan loan = new Loan(1, 1, Fixture.Chloe, new DateOnly(2026, 1, 1));
            Assert.AreEqual(0, library.Fine(loan, Fixture.Today));
        }

        [TestMethod]
        public void LateReturnFineUsesReturnDate()
        {
            // Due 2026-10-01, back 2026-10-05: 4 days late, whatever today is
            Loan loan = new Loan(1, 1, Fixture.Amira, new DateOnly(2026, 9, 17), returned: new DateOnly(2026, 10, 5));
            Assert.AreEqual(100, library.Fine(loan, new DateOnly(2026, 12, 25)));
        }

        [TestMethod]
        public void FormatMoney()
        {
            Assert.AreEqual("£0.00", Formatting.Money(0));
            Assert.AreEqual("£0.05", Formatting.Money(5));
            Assert.AreEqual("£1.25", Formatting.Money(125));
            Assert.AreEqual("£10.00", Formatting.Money(1000));
        }
    }

    // =================================================================================================
    // RESERVATIONS: WRITTEN FIRST, WATCHED FAIL, THEN MADE TO PASS (TDD)
    // =================================================================================================

    [TestClass]
    public class ReservationTests
    {
        private Library library = null!;

        [TestInitialize]
        public void Setup()
        {
            library = Fixture.Build();
            library.Borrow(Fixture.Amira, 5, Fixture.Today);   // the only copy of Robinson Crusoe
        }

        [TestMethod]
        public void ReserveBookWithNoFreeCopiesJoinsQueue()
        {
            Assert.AreEqual(1, library.Reserve(Fixture.Ben, 5));
            Assert.AreEqual(2, library.Reserve(Fixture.Chloe, 5));
        }

        [TestMethod]
        public void ReserveBookWithFreeCopiesRaises()
        {
            Assert.ThrowsExactly<ReservationNotNeededException>(() => library.Reserve(Fixture.Ben, 1));
        }

        [TestMethod]
        public void ReserveSameBookTwiceRaises()
        {
            library.Reserve(Fixture.Ben, 5);
            Assert.ThrowsExactly<AlreadyReservedException>(() => library.Reserve(Fixture.Ben, 5));
        }

        [TestMethod]
        public void ReturnedCopyIsHeldForFirstInQueue()
        {
            library.Reserve(Fixture.Ben, 5);
            library.ReturnBook(Fixture.Amira, 5, Fixture.Today);
            Assert.ThrowsExactly<BookNotAvailableException>(() => library.Borrow(Fixture.Chloe, 5, Fixture.Today));
        }

        [TestMethod]
        public void FirstInQueueCanBorrowHeldCopy()
        {
            library.Reserve(Fixture.Ben, 5);
            library.ReturnBook(Fixture.Amira, 5, Fixture.Today);
            library.Borrow(Fixture.Ben, 5, Fixture.Today);
            Assert.AreEqual(0, library.GetBook(5).Available);
            CollectionAssert.AreEqual(new List<int>(), library.HeldFor(5));
        }

        [TestMethod]
        public void QueueIsFirstComeFirstServed()
        {
            library.Reserve(Fixture.Chloe, 5);
            library.Reserve(Fixture.Ben, 5);
            library.ReturnBook(Fixture.Amira, 5, Fixture.Today);
            CollectionAssert.AreEqual(new List<int> { Fixture.Chloe }, library.HeldFor(5));
            CollectionAssert.AreEqual(new List<int> { Fixture.Ben }, library.QueueFor(5));
        }
    }

    // =================================================================================================
    // STRETCH: THE FILE LOADER, AND A TABLE OF CASES WITH [DataRow]
    // =================================================================================================

    [TestClass]
    public class StretchTests
    {
        // Walk up from the folder the tests run in to the first folder holding data/books.csv.
        private static string FindDataFolder()
        {
            DirectoryInfo? folder = new DirectoryInfo(AppContext.BaseDirectory);
            while (folder != null)
            {
                string candidate = Path.Combine(folder.FullName, "data");
                if (File.Exists(Path.Combine(candidate, "books.csv")))
                {
                    return candidate;
                }
                folder = folder.Parent;
            }
            throw new FileNotFoundException("could not find the data folder");
        }

        [TestMethod]
        public void CorruptCsvLoadsGoodLinesOnly()
        {
            LoadResult result = Storage.LoadBooksCsv(Path.Combine(FindDataFolder(), "books_corrupt.csv"));
            CollectionAssert.AreEqual(new List<int> { 1, 2, 4, 6, 10 }, result.Books.Select(b => b.Id).ToList());
            Assert.AreEqual(5, result.Skipped);
        }

        // One test method, run once per [DataRow]: (member, days late, expected pence)
        [TestMethod]
        [DataRow(Fixture.Amira, 0, 0)]
        [DataRow(Fixture.Amira, 1, 25)]
        [DataRow(Fixture.Amira, 20, 500)]
        [DataRow(Fixture.Ben, 1, 10)]
        [DataRow(Fixture.Ben, 30, 300)]
        [DataRow(Fixture.Chloe, 365, 0)]
        public void FinesTable(int memberId, int daysLate, int expectedPence)
        {
            ILoanPolicy policy = Fixture.Build().GetMember(memberId).Policy;
            Assert.AreEqual(expectedPence, policy.FineFor(daysLate));
        }
    }
}
