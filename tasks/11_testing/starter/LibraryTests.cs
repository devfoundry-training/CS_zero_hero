// =====================================================================================================
// Task 11 - Test the rules (starter)
// -----------------------------------------------------------------------------------------------------
// Session 11: testing with MSTest. The brief is in ../README.md. The task 09 and 10 model is here
// already, with two example tests. Write the rest from the TODO lists below; the checker looks for
// every name, so it reports MISSING for each test you have not written yet.
//
// Run (from the repo root):
//         dotnet test tasks/11_testing/starter --logger "console;verbosity=detailed"
// Check:  python tasks/check/check.py 11 -- dotnet test tasks/11_testing/starter --logger "console;verbosity=detailed"
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    // Done already: the fixture, books 1-8 of books.csv and members 101-103, no loans
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

        // [TestInitialize] runs before EACH test: every test gets a fresh library
        [TestInitialize]
        public void Setup()
        {
            library = Fixture.Build();
        }

        // An example to copy: arrange (the fixture), act, assert
        [TestMethod]
        public void BorrowReducesAvailable()
        {
            library.Borrow(Fixture.Amira, 4, Fixture.Today);
            Assert.AreEqual(3, library.GetBook(4).Available);
        }

        // TODO: ReturnRestoresAvailable
        // TODO: BorrowUnknownBookRaises
        //       hint: Assert.ThrowsExactly<UnknownBookException>(() => library.Borrow(...));
        // TODO: BorrowUnknownMemberRaises
        // TODO: BorrowWithNoCopiesRaises
        // TODO: ReturnBookNotOnLoanRaises
        // TODO: StandardLimitIsThreeLoans
        // TODO: StudentLimitIsFiveLoans
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

        // An example to copy: a loan built directly, new Loan(id, bookId, memberId, borrowed)
        [TestMethod]
        public void DueDateStandardIs14Days()
        {
            Loan loan = new Loan(1, 1, Fixture.Amira, new DateOnly(2026, 10, 12));
            Assert.AreEqual(new DateOnly(2026, 10, 26), library.DueDate(loan));
        }

        // TODO: DueDateStudentIs21Days
        // TODO: DueDateStaffIs28Days
        // TODO: DueDateCrossesMonthAndYearEnd
        // TODO: DueDateInLeapYear
        // TODO: NotOverdueOnDueDate
        // TODO: FineOneDayLateStandard
        // TODO: FineTenDaysLateStudent
        // TODO: StaffNeverFined
        // TODO: LateReturnFineUsesReturnDate
        // TODO: FormatMoney
    }

    // =================================================================================================
    // RESERVATIONS: WRITE ONE TEST, WATCH IT FAIL, MAKE IT PASS, REPEAT (TDD)
    // -------------------------------------------------------------------------------------------------
    // In each one, 101 has first borrowed book 5, the only copy of Robinson Crusoe (see Setup).
    // -------------------------------------------------------------------------------------------------

    [TestClass]
    public class ReservationTests
    {
        private Library library = null!;

        [TestInitialize]
        public void Setup()
        {
            library = Fixture.Build();
            library.Borrow(Fixture.Amira, 5, Fixture.Today);
        }

        // A reminder that shows up in the test output (as skipped) until the list below is done.
        // Delete it when every test in the brief is written.
        [TestMethod]
        public void StillToWrite()
        {
            Assert.Inconclusive("not implemented: 23 tests are still TODO in LibraryTests.cs");
        }

        // TODO: ReserveBookWithNoFreeCopiesJoinsQueue
        // TODO: ReserveBookWithFreeCopiesRaises
        // TODO: ReserveSameBookTwiceRaises
        // TODO: ReturnedCopyIsHeldForFirstInQueue
        // TODO: FirstInQueueCanBorrowHeldCopy
        // TODO: QueueIsFirstComeFirstServed
    }
}
