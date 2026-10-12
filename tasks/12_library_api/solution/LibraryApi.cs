// =====================================================================================================
// Task 12 - LibraryApi: the library over HTTP, JSON in and out
// -----------------------------------------------------------------------------------------------------
// Core endpoints:
//   GET  /books              every book, by id
//   GET  /books?title=TEXT   books whose title contains TEXT (any case)
//   POST /loans              {"member_id": 101, "book_id": 4}
// Stretch endpoints:
//   GET  /books/ID               one book
//   GET  /books/ID/openlibrary   Open Library's first match for it
// Anything else is a 404 with {"error": "not found"}.
// -----------------------------------------------------------------------------------------------------

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LibraryManager
{
    // The JSON shapes. Results.Json writes property names in camelCase (Id -> "id"); the names with an
    // underscore are given exactly with [JsonPropertyName].
    public record BookJson(int Id, string Title, string Author, int Year, string Genre, string Format, int Copies, int Available);

    public record LoanJson(
        int Id,
        [property: JsonPropertyName("book_id")] int BookId,
        [property: JsonPropertyName("member_id")] int MemberId,
        string Borrowed,
        string Due);

    public record ErrorJson(string Error);

    public record OpenLibraryJson(
        int Id,
        string Title,
        [property: JsonPropertyName("open_library")] Dictionary<string, object?>? OpenLibrary);

    public class LibraryApi
    {
        public const string BadBody = "body must be a JSON object with whole-number member_id and book_id";

        private readonly Library library;
        private readonly DateOnly today;

        // Requests can arrive on several threads at once: one lock makes each request see and change the
        // library on its own
        private readonly object gate = new object();

        public LibraryApi(Library library, DateOnly today)
        {
            this.library = library;
            this.today = today;
        }

        public void MapRoutes(WebApplication app)
        {
            // The query string value is decoded for us: war%20of arrives as "war of"
            app.MapGet("/books", (string? title) => ListBooks(title));
            app.MapGet("/books/{id:int}", (int id) => GetBook(id));
            app.MapGet("/books/{id:int}/openlibrary", (int id) => LookUpOpenLibrary(id));
            app.MapPost("/loans", (HttpRequest request) => CreateLoan(request));
            // Everything that matched nothing above
            app.MapFallback("{*path}", () => Results.Json(new ErrorJson("not found"), statusCode: 404));
        }

        private BookJson ToJson(Book book)
        {
            // Available counts copies held for reservations (task 11) as not available
            return new BookJson(book.Id, book.Title, book.Author, book.Year, book.Genre, book.FormatName, book.Copies, library.FreeCopies(book.Id));
        }

        public IResult ListBooks(string? title)
        {
            lock (gate)
            {
                List<Book> books = string.IsNullOrEmpty(title) ? library.AllBooks() : library.FindByTitle(title);
                return Results.Json(books.Select(ToJson).ToList());
            }
        }

        public IResult GetBook(int id)
        {
            lock (gate)
            {
                Book? book = library.FindById(id);
                if (book == null)
                {
                    return Results.Json(new ErrorJson($"no book with id {id}"), statusCode: 404);
                }
                return Results.Json(ToJson(book));
            }
        }

        // The body must be a JSON object whose member_id and book_id are whole numbers: not strings,
        // not decimals. Reading it ourselves with JsonDocument means a bad body becomes our 400.
        public static bool TryReadIds(string body, out int memberId, out int bookId)
        {
            memberId = 0;
            bookId = 0;
            try
            {
                using JsonDocument document = JsonDocument.Parse(body);
                JsonElement root = document.RootElement;
                return root.ValueKind == JsonValueKind.Object
                    && root.TryGetProperty("member_id", out JsonElement member)
                    && member.ValueKind == JsonValueKind.Number
                    && member.TryGetInt32(out memberId)   // false for 4.5 or 4.0
                    && root.TryGetProperty("book_id", out JsonElement book)
                    && book.ValueKind == JsonValueKind.Number
                    && book.TryGetInt32(out bookId);
            }
            catch (JsonException)
            {
                return false;
            }
        }

        public async Task<IResult> CreateLoan(HttpRequest request)
        {
            using StreamReader reader = new StreamReader(request.Body, Encoding.UTF8);
            string body = await reader.ReadToEndAsync();
            if (!TryReadIds(body, out int memberId, out int bookId))
            {
                return Results.Json(new ErrorJson(BadBody), statusCode: 400);
            }
            lock (gate)
            {
                try
                {
                    Loan loan = library.Borrow(memberId, bookId, today);
                    LoanJson created = new LoanJson(loan.Id, loan.BookId, loan.MemberId, Formatting.Iso(loan.Borrowed), Formatting.Iso(library.DueDate(loan)));
                    return Results.Json(created, statusCode: 201);
                }
                catch (LibraryException error) when (error is UnknownMemberException || error is UnknownBookException)
                {
                    return Results.Json(new ErrorJson(error.Message), statusCode: 404);
                }
                catch (LibraryException error)
                {
                    return Results.Json(new ErrorJson(error.Message), statusCode: 409);   // a rule said no: a conflict
                }
            }
        }

        // Stretch 3: ask Open Library about one of our books.
        public async Task<IResult> LookUpOpenLibrary(int id)
        {
            Book? book;
            lock (gate)
            {
                book = library.FindById(id);
            }
            if (book == null)
            {
                return Results.Json(new ErrorJson($"no book with id {id}"), statusCode: 404);
            }
            try
            {
                string text = await OpenLibrary.SearchAsync(book.Title, limit: 1);
                return Results.Json(new OpenLibraryJson(book.Id, book.Title, OpenLibrary.FirstMatch(text)));
            }
            catch (Exception error) when (error is HttpRequestException || error is TaskCanceledException || error is JsonException)
            {
                // TaskCanceledException is what HttpClient throws when the timeout runs out
                return Results.Json(new ErrorJson("Open Library did not answer"), statusCode: 502);
            }
        }
    }
}
