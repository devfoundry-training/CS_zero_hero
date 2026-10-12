// =====================================================================================================
// Task 12 - Loan policies: one interface, three (plus one) implementations (from task 06)
// -----------------------------------------------------------------------------------------------------
// Library and Member only ever talk to ILoanPolicy, never to a concrete policy class. Adding a policy
// means adding a class and one line in Policies.For, nothing else.
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    public interface ILoanPolicy
    {
        string Name { get; }

        int LoanDays();

        int FinePerDay();   // pence charged for each day late

        int MaxLoans();

        // A default interface method: shared behaviour that every policy gets for free.
        int FineFor(int daysLate)
        {
            return Math.Max(0, daysLate) * FinePerDay();
        }

        string Describe()
        {
            string fine = FinePerDay() == 0 ? "no fines" : $"{Formatting.Money(FinePerDay())} a day";
            return $"{Name}: {LoanDays()} days, {fine}, up to {Formatting.Plural(MaxLoans(), "loan", "loans")}";
        }
    }

    public class StandardPolicy : ILoanPolicy
    {
        public string Name => "standard";
        public int LoanDays() => 14;
        public int FinePerDay() => 25;
        public int MaxLoans() => 3;
    }

    public class StudentPolicy : ILoanPolicy
    {
        public string Name => "student";
        public int LoanDays() => 21;
        public int FinePerDay() => 10;
        public int MaxLoans() => 5;
    }

    public class StaffPolicy : ILoanPolicy
    {
        public string Name => "staff";
        public int LoanDays() => 28;
        public int FinePerDay() => 0;
        public int MaxLoans() => 10;
    }

    // Stretch 1: a new policy, added without touching Library or Member.
    public class GuestPolicy : ILoanPolicy
    {
        public string Name => "guest";
        public int LoanDays() => 7;
        public int FinePerDay() => 50;
        public int MaxLoans() => 1;
    }

    public static class Policies
    {
        // One shared object per policy: a policy has no state, so every member can point at the same one
        public static readonly ILoanPolicy[] All = { new StandardPolicy(), new StudentPolicy(), new StaffPolicy() };

        // The only place that maps a member type name to a policy.
        public static ILoanPolicy For(string memberType)
        {
            foreach (ILoanPolicy policy in All)
            {
                if (policy.Name == memberType)
                {
                    return policy;
                }
            }
            throw new ArgumentException("member type must be standard, student or staff");
        }
    }
}
