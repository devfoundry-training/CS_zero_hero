// =====================================================================================================
// Task 12 Stretch - consuming a public API: Open Library search (starter)
// -----------------------------------------------------------------------------------------------------
// Summarise() takes the response TEXT, not a URL, so it can be checked offline against the recorded
// data/openlibrary_dune.json. Any field can be missing, so every read needs a fallback.
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    public static class OpenLibrary
    {
        // TODO (Stretch 1): parse the text with JsonDocument and return
        //   Open Library search: NUM_FOUND results
        //   1. TITLE by AUTHOR, AUTHOR, first published YEAR, N editions, KEY
        // with "unknown author", "first published unknown" and "0 editions" when a field is missing.
        public static List<string> Summarise(string text)
        {
            throw new NotImplementedException("Stretch 1: OpenLibrary.Summarise");
        }
    }
}
