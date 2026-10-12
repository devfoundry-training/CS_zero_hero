// =====================================================================================================
// Task 05 - Member (starter)
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    public class Member
    {
        public int Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string Type { get; }
        public string Joined { get; }   // YYYY-MM-DD, kept as text in this task

        // The book ids on loan. The list can change, but it is never replaced.
        public List<int> Loans { get; } = new List<int>();

        // TODO (Core 3): the type must be standard, student or staff, or throw
        // new ArgumentException("member type must be standard, student or staff"). Then set the properties.
        public Member(int id, string name, string email, string type, string joined)
        {
            throw new NotImplementedException("Core 2 and 3: the Member constructor");
        }

        // TODO: #ID NAME (TYPE), N loans   ("1 loan" for one)
        public override string ToString()
        {
            throw new NotImplementedException("Core 2: Member.ToString");
        }
    }
}
