// =====================================================================================================
// Task 06 - Loan policies (starter)
// -----------------------------------------------------------------------------------------------------
// The interface is the contract from the brief. Write the three policies and the mapping from a member
// type name to a policy.
//
//   | Policy   | Loan days | Fine per day | Max loans |
//   | standard | 14        | 25p          | 3         |
//   | student  | 21        | 10p          | 5         |
//   | staff    | 28        | 0p           | 10        |
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    public interface ILoanPolicy
    {
        string Name { get; }

        int LoanDays();

        int FinePerDay();   // pence charged for each day late

        int MaxLoans();

        // A default interface method: every policy gets it for free.
        // TODO: days late times the daily fine, never below 0.
        int FineFor(int daysLate)
        {
            throw new NotImplementedException("ILoanPolicy.FineFor");
        }
    }

    // TODO: StandardPolicy, StudentPolicy and StaffPolicy, each implementing ILoanPolicy, for example
    //     public class StandardPolicy : ILoanPolicy
    //     {
    //         public string Name => "standard";
    //         public int LoanDays() => 14;
    //         ...
    //     }

    public static class Policies
    {
        // TODO: one object per policy, in the order standard, student, staff
        public static readonly ILoanPolicy[] All = { };

        // TODO: the policy for a member type name, or
        // throw new ArgumentException("member type must be standard, student or staff")
        public static ILoanPolicy For(string memberType)
        {
            throw new NotImplementedException("Policies.For");
        }
    }
}
