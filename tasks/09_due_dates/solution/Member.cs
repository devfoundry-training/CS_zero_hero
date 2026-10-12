// =====================================================================================================
// Task 09 - Member: now with the loan policy for their member type (from task 06)
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    public class Member
    {
        public int Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string Type { get; }
        public string Joined { get; }
        public ILoanPolicy Policy { get; }
        public List<int> Loans { get; } = new List<int>();   // book ids on loan

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
            Id = id;
            Name = name;
            Email = email;
            Type = type;
            Joined = joined;
            Policy = Policies.For(type);   // throws for an unknown type
        }

        public bool CanBorrowMore()
        {
            return Loans.Count < Policy.MaxLoans();
        }

        public override string ToString()
        {
            return $"#{Id} {Name} ({Type}), {Formatting.Plural(Loans.Count, "loan", "loans")}";
        }
    }
}
