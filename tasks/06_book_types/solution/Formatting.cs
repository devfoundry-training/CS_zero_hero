// =====================================================================================================
// Task 06 - Formatting: small text helpers shared by the whole program
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    public static class Formatting
    {
        // "1 loan" or "3 loans": the number and the right noun.
        public static string Plural(int count, string singular, string many)
        {
            return $"{count} {(count == 1 ? singular : many)}";
        }

        // Whole pence to pounds: 125 -> "£1.25". Integer maths only: / gives the pounds, % the pence,
        // and D2 pads the pence to two digits.
        public static string Money(int pence)
        {
            return $"£{pence / 100}.{pence % 100:D2}";
        }
    }
}
