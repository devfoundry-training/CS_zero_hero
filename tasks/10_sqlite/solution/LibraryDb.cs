// =====================================================================================================
// Task 10 - LibraryDb: the library in SQLite
// -----------------------------------------------------------------------------------------------------
// Every query uses $name parameters and passes the values separately. The database driver keeps the
// values apart from the SQL text, so a title like  x' OR '1'='1  is only ever data. Never build SQL by
// joining strings with values.
//
// The rules stay in Library.cs; the database only stores the rows.
// -----------------------------------------------------------------------------------------------------

using Microsoft.Data.Sqlite;

namespace LibraryManager
{
    // IDisposable: a using statement closes the connection for us, even when an exception is thrown.
    public sealed class LibraryDb : IDisposable
    {
        private readonly SqliteConnection connection;

        public LibraryDb(string path)
        {
            // Pooling off: when we close the connection, the file is really closed
            string connectionString = new SqliteConnectionStringBuilder { DataSource = path, Pooling = false }.ToString();
            connection = new SqliteConnection(connectionString);
            connection.Open();
            // SQLite ignores FOREIGN KEY rules unless this is switched on, once per connection
            Execute("PRAGMA foreign_keys = ON");
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        // A command with its parameters, ready to run. Values go in as parameters, never into the SQL.
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

        private void Execute(string sql, params (string Name, object? Value)[] parameters)
        {
            using SqliteCommand command = Command(sql, parameters);
            command.ExecuteNonQuery();
        }

        // =============================================================================================
        // SETTING UP
        // =============================================================================================

        // Run schema.sql: it drops and recreates every table. ExecuteNonQuery runs every statement in
        // the text, one after another.
        public void Create(string schemaPath)
        {
            Execute(File.ReadAllText(schemaPath));
        }

        // Copy the loaded CSV rows into the tables, in one transaction: all of them or none.
        public void ImportRows(LoadResult rows)
        {
            using SqliteTransaction transaction = connection.BeginTransaction();

            // One command per table, reused for every row: only the parameter values change
            using SqliteCommand insertBook = Command(
                "INSERT INTO books (id, title, author, year, genre, copies, format, length) "
                + "VALUES ($id, $title, $author, $year, $genre, $copies, $format, $length)");
            insertBook.Transaction = transaction;
            foreach (Book b in rows.Books)
            {
                insertBook.Parameters.Clear();
                insertBook.Parameters.AddWithValue("$id", b.Id);
                insertBook.Parameters.AddWithValue("$title", b.Title);
                insertBook.Parameters.AddWithValue("$author", b.Author);
                insertBook.Parameters.AddWithValue("$year", b.Year);
                insertBook.Parameters.AddWithValue("$genre", b.Genre);
                insertBook.Parameters.AddWithValue("$copies", b.Copies);
                insertBook.Parameters.AddWithValue("$format", b.FormatName);
                insertBook.Parameters.AddWithValue("$length", b.Length);
                insertBook.ExecuteNonQuery();
            }

            using SqliteCommand insertMember = Command(
                "INSERT INTO members (id, name, email, type, joined) VALUES ($id, $name, $email, $type, $joined)");
            insertMember.Transaction = transaction;
            foreach (Member m in rows.Members)
            {
                insertMember.Parameters.Clear();
                insertMember.Parameters.AddWithValue("$id", m.Id);
                insertMember.Parameters.AddWithValue("$name", m.Name);
                insertMember.Parameters.AddWithValue("$email", m.Email);
                insertMember.Parameters.AddWithValue("$type", m.Type);
                insertMember.Parameters.AddWithValue("$joined", m.Joined);
                insertMember.ExecuteNonQuery();
            }

            using SqliteCommand insertLoan = Command(
                "INSERT INTO loans (id, book_id, member_id, borrowed, returned) VALUES ($id, $book, $member, $borrowed, $returned)");
            insertLoan.Transaction = transaction;
            foreach (Loan x in rows.Loans)
            {
                insertLoan.Parameters.Clear();
                insertLoan.Parameters.AddWithValue("$id", x.Id);
                insertLoan.Parameters.AddWithValue("$book", x.BookId);
                insertLoan.Parameters.AddWithValue("$member", x.MemberId);
                insertLoan.Parameters.AddWithValue("$borrowed", Formatting.Iso(x.Borrowed));
                insertLoan.Parameters.AddWithValue("$returned", x.Returned == null ? (object)DBNull.Value : Formatting.Iso(x.Returned.Value));
                insertLoan.ExecuteNonQuery();
            }

            transaction.Commit();   // without this, disposing the transaction rolls everything back
        }

        public int Count(string table)
        {
            // A table name cannot be a parameter, so it is checked against a fixed list instead of trusted
            if (table != "books" && table != "members" && table != "loans")
            {
                throw new ArgumentException($"unknown table {table}");
            }
            using SqliteCommand command = Command($"SELECT COUNT(*) FROM {table}");
            return Convert.ToInt32(command.ExecuteScalar());   // SQLite gives back a long
        }

        // =============================================================================================
        // QUERIES
        // =============================================================================================

        public List<(string Genre, int Count)> GenreCounts()
        {
            List<(string, int)> rows = new List<(string, int)>();
            using SqliteCommand command = Command("SELECT genre, COUNT(*) FROM books GROUP BY genre ORDER BY genre");
            using SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                rows.Add((reader.GetString(0), reader.GetInt32(1)));
            }
            return rows;
        }

        // (id, title, available, copies) for books with nothing on the shelf.
        public List<(int Id, string Title, int Available, int Copies)> UnavailableBooks()
        {
            List<(int, string, int, int)> rows = new List<(int, string, int, int)>();
            using SqliteCommand command = Command(
                @"SELECT b.id, b.title, b.copies - COUNT(l.id) AS available, b.copies
                  FROM books b
                  LEFT JOIN loans l ON l.book_id = b.id AND l.returned IS NULL
                  GROUP BY b.id, b.title, b.copies
                  HAVING b.copies - COUNT(l.id) = 0
                  ORDER BY b.id");
            using SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                rows.Add((reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3)));
            }
            return rows;
        }

        public List<(int Id, string Title, int Year)> SearchTitles(string text)
        {
            List<(int, string, int)> rows = new List<(int, string, int)>();
            // The % wildcards go into the value, not into the SQL
            using SqliteCommand command = Command(
                "SELECT id, title, year FROM books WHERE LOWER(title) LIKE $pattern ORDER BY id",
                ("$pattern", $"%{text.ToLowerInvariant()}%"));
            using SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                rows.Add((reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
            }
            return rows;
        }

        public List<(int Id, string Name, int Open)> OpenLoansPerMember()
        {
            List<(int, string, int)> rows = new List<(int, string, int)>();
            using SqliteCommand command = Command(
                @"SELECT m.id, m.name, COUNT(l.id)
                  FROM members m
                  LEFT JOIN loans l ON l.member_id = m.id AND l.returned IS NULL
                  GROUP BY m.id, m.name
                  ORDER BY m.id");
            using SqliteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                rows.Add((reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
            }
            return rows;
        }

        // =============================================================================================
        // LOADING THE MODEL AND SAVING CHANGES
        // =============================================================================================

        public Library LoadLibrary()
        {
            Library library = new Library();
            using (SqliteCommand command = Command("SELECT id, title, author, year, genre, copies, format, length FROM books ORDER BY id"))
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string[] fields = new string[8];
                    for (int i = 0; i < 8; i++)
                    {
                        fields[i] = Convert.ToString(reader.GetValue(i), System.Globalization.CultureInfo.InvariantCulture)!;
                    }
                    library.AddBook(BookFactory.MakeBook(fields));
                }
            }
            using (SqliteCommand command = Command("SELECT id, name, email, type, joined FROM members ORDER BY id"))
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    library.AddMember(new Member(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4)));
                }
            }
            using (SqliteCommand command = Command("SELECT id, book_id, member_id, borrowed, returned FROM loans ORDER BY id"))
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    DateOnly? returned = reader.IsDBNull(4) ? null : Formatting.ParseIso(reader.GetString(4));
                    library.AddLoan(new Loan(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2),
                                             Formatting.ParseIso(reader.GetString(3)), returned));
                }
            }
            return library;
        }

        public void InsertLoan(Loan loan)
        {
            Execute("INSERT INTO loans (id, book_id, member_id, borrowed, returned) VALUES ($id, $book, $member, $borrowed, NULL)",
                    ("$id", loan.Id), ("$book", loan.BookId), ("$member", loan.MemberId), ("$borrowed", Formatting.Iso(loan.Borrowed)));
        }

        public void MarkReturned(Loan loan)
        {
            if (loan.Returned == null)
            {
                throw new ArgumentException("loan has not been returned");
            }
            Execute("UPDATE loans SET returned = $returned WHERE id = $id",
                    ("$returned", Formatting.Iso(loan.Returned.Value)), ("$id", loan.Id));
        }

        // =============================================================================================
        // STRETCH
        // =============================================================================================

        // Stretch 1: the whole borrow inside one transaction. Insert the loan first, then check stock in
        // SQL. If the check fails the transaction rolls back and the insert disappears.
        // Returns the new loan id, or null when refused.
        public int? BorrowInTransaction(int memberId, int bookId, DateOnly today)
        {
            using SqliteTransaction transaction = connection.BeginTransaction();

            using SqliteCommand nextId = Command("SELECT COALESCE(MAX(id), 0) + 1 FROM loans");
            nextId.Transaction = transaction;
            int newId = Convert.ToInt32(nextId.ExecuteScalar());

            using SqliteCommand insert = Command(
                "INSERT INTO loans (id, book_id, member_id, borrowed, returned) VALUES ($id, $book, $member, $borrowed, NULL)",
                ("$id", newId), ("$book", bookId), ("$member", memberId), ("$borrowed", Formatting.Iso(today)));
            insert.Transaction = transaction;
            insert.ExecuteNonQuery();

            using SqliteCommand openLoans = Command(
                "SELECT COUNT(*) FROM loans WHERE book_id = $book AND returned IS NULL", ("$book", bookId));
            openLoans.Transaction = transaction;
            using SqliteCommand copies = Command("SELECT copies FROM books WHERE id = $book", ("$book", bookId));
            copies.Transaction = transaction;
            if (Convert.ToInt32(openLoans.ExecuteScalar()) > Convert.ToInt32(copies.ExecuteScalar()))
            {
                transaction.Rollback();   // the INSERT above is undone
                return null;
            }
            transaction.Commit();
            return newId;
        }

        // Stretch 2: an index on loans(member_id). "Open loans per member" joins members to loans on
        // member_id; without an index SQLite reads every loan for every member, with it SQLite jumps
        // straight to that member's loans. The bigger the loans table, the more it helps.
        public void AddMemberIndex()
        {
            Execute("CREATE INDEX IF NOT EXISTS idx_loans_member_id ON loans (member_id)");
        }
    }
}
