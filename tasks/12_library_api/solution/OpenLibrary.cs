// =====================================================================================================
// Task 12 Stretch - consuming a public API: Open Library search
// -----------------------------------------------------------------------------------------------------
// https://openlibrary.org/search.json?title=dune
// Two halves, kept apart on purpose:
//   * Summarise() turns response TEXT into lines. No network, so it is checked against the recorded
//     data/openlibrary_dune.json
//   * SearchAsync() does the HTTP call. It needs the network, so it is never part of a check
// Any field can be missing from a real response, so every read has a fallback.
// -----------------------------------------------------------------------------------------------------

using System.Text.Json;

namespace LibraryManager
{
    public static class OpenLibrary
    {
        const string SearchUrl = "https://openlibrary.org/search.json";
        const string Fields = "key,title,author_name,first_publish_year,edition_count,number_of_pages_median";

        // One HttpClient for the whole program: creating one per request wastes connections
        static readonly HttpClient Client = CreateClient();

        static HttpClient CreateClient()
        {
            HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
            client.DefaultRequestHeaders.UserAgent.ParseAdd("ZeroToHeroLibrary/1.0 (teaching example)");
            return client;
        }

        // The raw JSON text. Throws HttpRequestException, or TaskCanceledException after 10 seconds.
        public static async Task<string> SearchAsync(string title, int limit = 3)
        {
            string url = $"{SearchUrl}?title={Uri.EscapeDataString(title)}&limit={limit}&fields={Fields}";
            return await Client.GetStringAsync(url);
        }

        // The first result as a small object for the API, or null when there are no results.
        public static Dictionary<string, object?>? FirstMatch(string text)
        {
            using JsonDocument document = JsonDocument.Parse(text);
            if (!document.RootElement.TryGetProperty("docs", out JsonElement docs) || docs.GetArrayLength() == 0)
            {
                return null;
            }
            JsonElement doc = docs[0];
            // Clone() keeps a value usable after the document is disposed
            object? Field(string name) => doc.TryGetProperty(name, out JsonElement value) ? value.Clone() : null;
            return new Dictionary<string, object?>
            {
                ["key"] = Field("key"),
                ["title"] = Field("title"),
                ["authors"] = Field("author_name") ?? new List<string>(),
                ["first_publish_year"] = Field("first_publish_year"),
                ["edition_count"] = Field("edition_count"),
            };
        }

        // Lines describing a search response.
        public static List<string> Summarise(string text)
        {
            using JsonDocument document = JsonDocument.Parse(text);
            JsonElement root = document.RootElement;

            // ToString() on a JsonElement gives a number as written, and a string without its quotes
            string Text(JsonElement element, string name, string fallback)
            {
                return element.TryGetProperty(name, out JsonElement value) && value.ValueKind != JsonValueKind.Null
                    ? value.ToString()
                    : fallback;
            }

            List<string> lines = new List<string> { $"Open Library search: {Text(root, "numFound", "0")} results" };
            if (!root.TryGetProperty("docs", out JsonElement docs))
            {
                return lines;
            }
            int number = 1;
            foreach (JsonElement doc in docs.EnumerateArray())
            {
                List<string> names = new List<string>();
                if (doc.TryGetProperty("author_name", out JsonElement authorNames) && authorNames.ValueKind == JsonValueKind.Array)
                {
                    names.AddRange(authorNames.EnumerateArray().Select(name => name.ToString()));
                }
                string authors = names.Count > 0 ? string.Join(", ", names) : "unknown author";
                lines.Add($"{number}. {Text(doc, "title", "untitled")} by {authors}, "
                          + $"first published {Text(doc, "first_publish_year", "unknown")}, "
                          + $"{Text(doc, "edition_count", "0")} editions, {Text(doc, "key", "no key")}");
                number++;
            }
            return lines;
        }
    }
}
