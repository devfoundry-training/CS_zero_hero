// =====================================================================================================
// Task 07 - Our own exceptions (starter)
// -----------------------------------------------------------------------------------------------------
// One base class, one subclass per problem. Each subclass builds its own message from what it is given.
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    // Base class for every rule the library can refuse
    public class LibraryException : Exception
    {
        public LibraryException(string message) : base(message)
        {
        }

        // innerException keeps the original problem as the cause (Stretch 2)
        public LibraryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    // The user typed something we cannot understand: the message is passed in
    public class CommandException : LibraryException
    {
        public CommandException(string message) : base(message)
        {
        }

        public CommandException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    // TODO (Core 1): one class per problem, each extending LibraryException and building its message:
    //   UnknownBookException(int bookId)                 no book with id ID
    //   UnknownMemberException(int memberId)             no member with id ID
    //   BookNotAvailableException(string title)          TITLE has no copies available
    //   MemberLimitReachedException(string name, int limit)
    //                                                    NAME already has N loans (limit N)
    //   NotOnLoanException(string title, string name)    TITLE is not on loan to NAME
    // For example:
    //     public class UnknownBookException : LibraryException
    //     {
    //         public UnknownBookException(int bookId) : base($"no book with id {bookId}")
    //         {
    //         }
    //     }
}
