// =====================================================================================================
// Task 12 - LibraryApi: the library over HTTP, JSON in and out (starter)
// -----------------------------------------------------------------------------------------------------
// Core endpoints:
//   GET  /books              every book, by id
//   GET  /books?title=TEXT   books whose title contains TEXT (any case)
//   POST /loans              {"member_id": 101, "book_id": 4}
// Anything else is a 404 with {"error": "not found"}.
// Until a route is written, every request gets 501 {"error": "not implemented"}.
// -----------------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace LibraryManager
{
    // The JSON shapes. Results.Json writes property names in camelCase (Id -> "id"); names with an
    // underscore are given exactly with [JsonPropertyName].
    public record BookJson(int Id, string Title, string Author, int Year, string Genre, string Format, int Copies, int Available);

    public record LoanJson(
        int Id,
        [property: JsonPropertyName("book_id")] int BookId,
        [property: JsonPropertyName("member_id")] int MemberId,
        string Borrowed,
        string Due);

    public record ErrorJson(string Error);

    public class LibraryApi
    {
        public const string BadBody = "body must be a JSON object with whole-number member_id and book_id";

        private readonly Library library;
        private readonly DateOnly today;

        // Requests can arrive on several threads at once: take this lock around every use of library
        private readonly object gate = new object();

        public LibraryApi(Library library, DateOnly today)
        {
            this.library = library;
            this.today = today;
        }

        // TODO (Core 2): map the routes, for example
        //     app.MapGet("/books", (string? title) => ListBooks(title));
        //     app.MapPost("/loans", (HttpRequest request) => CreateLoan(request));
        // and change the fallback to a 404 {"error": "not found"}.
        public void MapRoutes(WebApplication app)
        {
            app.MapFallback("{*path}", () => Results.Json(new ErrorJson("not implemented"), statusCode: 501));
        }

        // TODO (Core 2): 200 and every book (or the books whose title contains title, ignoring case),
        // by id, as BookJson. Available is library.FreeCopies(book.Id).
        public IResult ListBooks(string? title)
        {
            throw new NotImplementedException("ListBooks");
        }

        // TODO (Core 3 and 4): read the body text yourself (StreamReader on request.Body), parse it with
        // JsonDocument, and answer 400 with BadBody unless member_id and book_id are whole numbers.
        // Then library.Borrow(...): 201 and a LoanJson, 404 for an unknown member or book, 409 for any
        // other LibraryException. Results.Json(value, statusCode: 201) sets the status.
        public Task<IResult> CreateLoan(HttpRequest request)
        {
            throw new NotImplementedException("CreateLoan");
        }
    }
}
