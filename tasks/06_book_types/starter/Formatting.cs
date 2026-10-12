// =====================================================================================================
// Task 06 - Formatting: small text helpers shared by the whole program (starter)
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

        // TODO: whole pence to pounds with integer maths only: 125 -> "£1.25", 5 -> "£0.05".
        // Hint: $"£{pence / 100}.{pence % 100:D2}"
        public static string Money(int pence)
        {
            throw new NotImplementedException("Formatting.Money");
        }
    }
}
