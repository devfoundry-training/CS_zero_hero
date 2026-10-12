// =====================================================================================================
// Task 12 - Expose the library (solution, Core + Stretch)
// -----------------------------------------------------------------------------------------------------
// Session 12: APIs. Builds on task 11. An ASP.NET Core minimal API: the routes are in LibraryApi.cs.
//
// Run the server (from the repo root):
//       dotnet run --project tasks/12_library_api/solution -- tasks/data [PORT] [TODAY]
//       (PORT defaults to 8080, TODAY to 2026-10-12)
// Then try:  curl "http://localhost:8080/books?title=the"
// Stop it with Ctrl+C.
//
// Stretch, offline:  dotnet run --project tasks/12_library_api/solution -- --summarise FILE
//                    prints the summary of a saved Open Library response
// Stretch, online:   dotnet run --project tasks/12_library_api/solution -- --lookup TITLE
// -----------------------------------------------------------------------------------------------------

using System.Text;

namespace LibraryManager
{
    internal class Program
    {
        const int DefaultPort = 8080;
        static readonly DateOnly DefaultToday = new DateOnly(2026, 10, 12);

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            if (args.Length == 2 && args[0] == "--summarise")
            {
                foreach (string line in OpenLibrary.Summarise(File.ReadAllText(args[1])))
                {
                    Console.WriteLine(line);
                }
                return;
            }
            if (args.Length == 2 && args[0] == "--lookup")
            {
                try
                {
                    foreach (string line in OpenLibrary.Summarise(await OpenLibrary.SearchAsync(args[1])))
                    {
                        Console.WriteLine(line);
                    }
                }
                catch (Exception error) when (error is HttpRequestException || error is TaskCanceledException)
                {
                    Console.WriteLine($"Open Library did not answer: {error.Message}");
                }
                return;
            }

            string data = args.Length > 0 ? args[0] : Path.Combine("tasks", "data");
            int port = args.Length > 1 ? int.Parse(args[1]) : DefaultPort;
            DateOnly today = args.Length > 2 ? Formatting.ParseIso(args[2]) : DefaultToday;

            // Every start loads a fresh library from the CSV files, so every run of the check sees the
            // same data
            LibraryApi api = new LibraryApi(Storage.LoadAll(data), today);

            // Our own arguments are not passed to CreateBuilder: they are not ASP.NET settings
            WebApplicationBuilder builder = WebApplication.CreateBuilder();
            builder.Logging.ClearProviders();   // keep the console quiet: no log line per request
            WebApplication app = builder.Build();
            api.MapRoutes(app);

            Console.WriteLine($"Library API on http://localhost:{port} (today is {Formatting.Iso(today)})");
            await app.RunAsync($"http://localhost:{port}");
        }
    }
}
