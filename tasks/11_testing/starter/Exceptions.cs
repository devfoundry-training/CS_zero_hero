// =====================================================================================================
// Task 11 - Our own exceptions: one base class, one subclass per problem (from task 07)
// -----------------------------------------------------------------------------------------------------
// Catch LibraryException to handle every library problem in one place; catch a subclass when one
// problem needs special handling. Each exception builds its own message, so the text is written once,
// here, and never repeated at the places that throw.
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    // Base class for every rule the library can refuse
    public class LibraryException : Exception
    {
        public LibraryException(string message) : base(message)
        {
        }

        // innerException keeps the original problem as the cause (exception chaining)
        public LibraryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class UnknownBookException : LibraryException
    {
        public UnknownBookException(int bookId) : base($"no book with id {bookId}")
        {
        }
    }

    public class UnknownMemberException : LibraryException
    {
        public UnknownMemberException(int memberId) : base($"no member with id {memberId}")
        {
        }
    }

    public class BookNotAvailableException : LibraryException
    {
        public BookNotAvailableException(string title) : base($"{title} has no copies available")
        {
        }
    }

    public class MemberLimitReachedException : LibraryException
    {
        public MemberLimitReachedException(string name, int limit)
            : base($"{name} already has {Formatting.Plural(limit, "loan", "loans")} (limit {limit})")
        {
        }
    }

    public class NotOnLoanException : LibraryException
    {
        public NotOnLoanException(string title, string name) : base($"{title} is not on loan to {name}")
        {
        }
    }

    // The user typed something we cannot understand
    public class CommandException : LibraryException
    {
        public CommandException(string message) : base(message)
        {
        }

        public CommandException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
