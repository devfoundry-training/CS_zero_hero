// =====================================================================================================
// Task 10 - Formatting: small text helpers shared by the whole program (from task 06)
// -----------------------------------------------------------------------------------------------------

using System.Globalization;

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

        // A date as YYYY-MM-DD. InvariantCulture: the same text on every machine, whatever its settings.
        public static string Iso(DateOnly day)
        {
            return day.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }

        // "2026-10-12" -> a DateOnly. ParseExact accepts that one shape and nothing else.
        public static DateOnly ParseIso(string text)
        {
            return DateOnly.ParseExact(text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }
}
