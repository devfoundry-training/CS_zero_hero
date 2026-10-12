// =====================================================================================================
// Task 06 - Member (starter: the task 05 solution)
// -----------------------------------------------------------------------------------------------------
// TODO (Core 5): give each member a read-only ILoanPolicy Policy, set in the constructor from
// Policies.For(type). Policies.For can then do the type check instead of MemberTypes below.
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    public class Member
    {
        static readonly string[] MemberTypes = { "standard", "student", "staff" };

        public int Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string Type { get; }
        public string Joined { get; }   // YYYY-MM-DD, kept as text in this task

        // The list itself can be changed (Library adds and removes ids), but never replaced
        public List<int> Loans { get; } = new List<int>();

        public Member(int id, string name, string email, string type, string joined)
        {
            if (id <= 0)
            {
                throw new ArgumentException("member id must be positive");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name must not be blank");
            }
            if (!MemberTypes.Contains(type))
            {
                throw new ArgumentException("member type must be standard, student or staff");
            }
            Id = id;
            Name = name;
            Email = email;
            Type = type;
            Joined = joined;
        }

        public override string ToString()
        {
            string loans = Loans.Count == 1 ? "1 loan" : $"{Loans.Count} loans";
            return $"#{Id} {Name} ({Type}), {loans}";
        }
    }
}
