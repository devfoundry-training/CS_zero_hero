// =====================================================================================================
// Task 10 - LibraryDb: the library in SQLite (starter)
// -----------------------------------------------------------------------------------------------------
// Parameterised queries only: put $name in the SQL and pass the value with
// command.Parameters.AddWithValue("$name", value). Never build SQL by joining strings with values.
// The rules stay in Library.cs; the database only stores the rows.
// -----------------------------------------------------------------------------------------------------

using Microsoft.Data.Sqlite;

namespace LibraryManager
{
    // IDisposable: a using statement closes the connection for us, even when an exception is thrown.
    public sealed class LibraryDb : IDisposable
    {
        private readonly SqliteConnection connection;

        // Done already: open the file and switch on foreign keys
        public LibraryDb(string path)
        {
            // Pooling off: when we close the connection, the file is really closed
            string connectionString = new SqliteConnectionStringBuilder { DataSource = path, Pooling = false }.ToString();
            connection = new SqliteConnection(connectionString);
            connection.Open();
            using SqliteCommand pragma = Command("PRAGMA foreign_keys = ON");
            pragma.ExecuteNonQuery();
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        // Done already: a command with its parameters, ready to run, for example
        //     using SqliteCommand command = Command("SELECT title FROM books WHERE id = $id", ("$id", 4));
        private SqliteCommand Command(string sql, params (string Name, object? Value)[] parameters)
        {
            SqliteCommand command = connection.CreateCommand();
            command.CommandText = sql;
            foreach ((string name, object? value) in parameters)
            {
                command.Parameters.AddWithValue(name, value ?? DBNull.Value);   // null becomes SQL NULL
            }
            return command;
        }

        // TODO (Core 2): run the whole of schema.sql (ExecuteNonQuery runs every statement in the text).
        public void Create(string schemaPath)
        {
            throw new NotImplementedException("Core 2: LibraryDb.Create");
        }

        // TODO (Core 3): INSERT every book, member and loan inside one transaction
        // (connection.BeginTransaction(), set command.Transaction, then Commit()).
        public void ImportRows(LoadResult rows)
        {
            throw new NotImplementedException("Core 3: LibraryDb.ImportRows");
        }

        // TODO (Core 3): SELECT COUNT(*) FROM the table. A table name cannot be a parameter: check it
        // against a fixed list first.
        public int Count(string table)
        {
            throw new NotImplementedException("Core 3: LibraryDb.Count");
        }

        // TODO (Core 4.1): books per genre, A-Z by genre (GROUP BY).
        public List<(string Genre, int Count)> GenreCounts()
        {
            throw new NotImplementedException("Core 4: LibraryDb.GenreCounts");
        }

        // TODO (Core 4.2): books where copies minus open loans is 0 (LEFT JOIN + GROUP BY + HAVING), by id.
        public List<(int Id, string Title, int Available, int Copies)> UnavailableBooks()
        {
            throw new NotImplementedException("Core 4: LibraryDb.UnavailableBooks");
        }

        // TODO (Core 4.3): WHERE LOWER(title) LIKE $pattern, with the % wildcards inside the value.
        public List<(int Id, string Title, int Year)> SearchTitles(string text)
        {
            throw new NotImplementedException("Core 4: LibraryDb.SearchTitles");
        }

        // TODO (Core 4.4): open loans per member, including members with none (LEFT JOIN), by member id.
        public List<(int Id, string Name, int Open)> OpenLoansPerMember()
        {
            throw new NotImplementedException("Core 4: LibraryDb.OpenLoansPerMember");
        }

        // TODO (Core 5): build the task 09 model from the three tables (not from the CSV files).
        public Library LoadLibrary()
        {
            throw new NotImplementedException("Core 5: LibraryDb.LoadLibrary");
        }

        // TODO (Core 5): save a new loan with one parameterised INSERT.
        public void InsertLoan(Loan loan)
        {
            throw new NotImplementedException("Core 5: LibraryDb.InsertLoan");
        }

        // TODO (Core 5): save a return with one parameterised UPDATE.
        public void MarkReturned(Loan loan)
        {
            throw new NotImplementedException("Core 5: LibraryDb.MarkReturned");
        }
    }
}
