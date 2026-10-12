// =====================================================================================================
// Task 06 - Book types and the factory (starter)
// -----------------------------------------------------------------------------------------------------
// TODO (Core 2): three classes that extend the abstract Book (see the TODO in Book.cs):
//   * PrintBook: format "print", length text "N pages"
//   * EBook:     format "ebook", length text "N pages"
//   * AudioBook: format "audio", length in minutes shown as "Hh MMm" (1150 is "19h 10m")
// Each one passes its constructor arguments on with : base(...).
// -----------------------------------------------------------------------------------------------------

namespace LibraryManager
{
    public static class BookFactory
    {
        // TODO (Core 3): the format column (fields[6]) decides which class to build. The fields are the
        // eight columns of books.csv: id,title,author,year,genre,copies,format,length. An unknown format:
        // throw new ArgumentException($"format must be print, ebook or audio, got '{format}'")
        public static Book MakeBook(string[] fields)
        {
            throw new NotImplementedException("Core 3: BookFactory.MakeBook");
        }
    }
}
