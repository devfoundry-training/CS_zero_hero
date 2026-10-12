// =====================================================================================================
// Task 12 - Expose the library (starter)
// -----------------------------------------------------------------------------------------------------
// Session 12: APIs. The brief is in ../README.md. The task 11 model is here already, and the server
// starts; the routes go in LibraryApi.cs.
//
// Run the server (from the repo root):
//         dotnet run --project tasks/12_library_api/starter -- tasks/data [PORT]
//         then try:  curl "http://localhost:8080/books?title=the"   (Ctrl+C stops the server)
// Check:  python tasks/check/check.py 12 -- dotnet run --project tasks/12_library_api/starter -- tasks/data {port}
// Stretch: dotnet run --project tasks/12_library_api/starter -- --summarise tasks/data/openlibrary_dune.json
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
                try
                {
                    foreach (string line in OpenLibrary.Summarise(File.ReadAllText(args[1])))
                    {
                        Console.WriteLine(line);
                    }
                }
                catch (NotImplementedException error)
                {
                    Console.WriteLine($"not implemented: {error.Message}");
                }
                return;
            }

            string data = args.Length > 0 ? args[0] : Path.Combine("tasks", "data");
            int port = args.Length > 1 ? int.Parse(args[1]) : DefaultPort;

            // Every start loads a fresh library from the CSV files, so every run starts the same
            LibraryApi api = new LibraryApi(Storage.LoadAll(data), DefaultToday);

            // Our own arguments are not passed to CreateBuilder: they are not ASP.NET settings
            WebApplicationBuilder builder = WebApplication.CreateBuilder();
            builder.Logging.ClearProviders();   // keep the console quiet: no log line per request
            WebApplication app = builder.Build();
            api.MapRoutes(app);

            Console.WriteLine($"Library API on http://localhost:{port} (today is {Formatting.Iso(DefaultToday)})");
            Console.WriteLine("not implemented: the routes in LibraryApi.MapRoutes (every request gets a 501 for now)");
            await app.RunAsync($"http://localhost:{port}");
        }
    }
}
