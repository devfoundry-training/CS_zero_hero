// ==============================
// File: FileIO.cs
// ==============================
// This file demonstrates comprehensive File I/O in C# with detailed commentary.
// Each concept is encapsulated in a public static method for clarity.

using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Threading.Tasks;

namespace CSTutorial
{
    public static class FileIO
    {
        private const string TextFilePath = "example.txt";
        private const string BinaryFilePath = "example.bin";

        //public static void Main(string[] args)
        //{
        //    // Invoke each demo in sequence
        //    WriteTextWithStreamWriter();            // Buffered text write
        //    ReadTextWithStreamReader();             // Buffered text read
        //    UseFileStaticMethods();                 // Quick file operations
        //    WriteBinaryWithFileStream();            // Low-level binary write
        //    ReadBinaryWithBinaryReader();           // Typed binary read
        //    AsyncFileReadDemo().GetAwaiter().GetResult();  // Non-blocking text read
        //    MemoryMappedFileDemo();                 // Efficient random access
        //}



        // 1) Buffered text write using StreamWriter
        //    - Internal 4K buffer reduces OS write calls
        //    - Supports text encoding (UTF-8, UTF-16, etc.) unlike raw FileStream
        //    - Ideal for writing line-oriented text or large text blocks efficiently
        public static void WriteTextWithStreamWriter()
        {
            Console.WriteLine("-- WriteTextWithStreamWriter --");
            using (var writer = new StreamWriter(
                path: TextFilePath,
                append: false,
                encoding: Encoding.UTF8,
                bufferSize: 4096)) // larger buffer for better throughput
            {
                writer.WriteLine("Hello, C#!");
                writer.WriteLine("Buffered write via StreamWriter.");
                // writer.Flush(); // optional: manually flush buffer to disk
            }
        }

        // 2) Buffered text read using StreamReader
        //    - Reads chunks of characters (default 4K) to minimize disk reads
        //    - Provides ReadLine(), ReadToEnd(), Peek(), async variants
        //    - Automatically decodes bytes to text via specified encoding
        public static void ReadTextWithStreamReader()
        {
            Console.WriteLine("-- ReadTextWithStreamReader --");
            using (var reader = new StreamReader(
                path: TextFilePath,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: true,
                bufferSize: 4096))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }

        // 3) Convenience static File methods
        //    - File.WriteAllText/File.AppendAllText: write small text files in one call
        //    - File.ReadAllText/ReadAllLines: read entire file to memory (not for large files)
        //    - Great for quick tasks, but offers less control over buffering and encoding
        public static void UseFileStaticMethods()
        {
            Console.WriteLine("-- UseFileStaticMethods --");
            // Overwrite entire file in one shot
            File.WriteAllText(TextFilePath, "Overwritten via File.WriteAllText.\n");
            // Append additional lines without manual open/close
            File.AppendAllText(TextFilePath, "Appended via File.AppendAllText.\n");
            // Read entire file into a single string
            string content = File.ReadAllText(TextFilePath);
            Console.WriteLine(content);
        }

        // 4) Binary write with FileStream
        //    - Provides byte-level control: write raw bytes directly
        //    - Configure FileMode, FileAccess, FileShare, buffer size, and FileOptions
        //    - Use when writing non-text data or custom serialization formats
        public static void WriteBinaryWithFileStream()
        {
            Console.WriteLine("-- WriteBinaryWithFileStream --");
            byte[] data = { 0xDE, 0xAD, 0xBE, 0xEF };
            using (var fs = new FileStream(
                path: BinaryFilePath,
                mode: FileMode.Create,
                access: FileAccess.Write,
                share: FileShare.None,
                bufferSize: 8192,
                options: FileOptions.WriteThrough)) // bypass OS cache
            {
                fs.Write(data, 0, data.Length);
                // fs.Flush(); // ensure data is persisted immediately
            }
        }

        // 5) Binary read with BinaryReader
        //    - Wraps a Stream to provide typed reads (ReadByte, ReadInt32, ReadBytes)
        //    - Automatically advances stream position and handles endianness considerations
        public static void ReadBinaryWithBinaryReader()
        {
            Console.WriteLine("-- ReadBinaryWithBinaryReader --");
            using (var fs = new FileStream(
                path: BinaryFilePath,
                mode: FileMode.Open,
                access: FileAccess.Read))
            using (var br = new BinaryReader(fs))
            {
                byte first = br.ReadByte();
                byte[] rest = br.ReadBytes(3);
                Console.WriteLine(
                    $"BinaryReader read: 0x{first:X2}, rest: {BitConverter.ToString(rest)}");
            }
        }

        // 6) Asynchronous text read demo
        //    - Non-blocking: frees the calling thread while waiting for I/O
        //    - Ideal for GUI or server applications to remain responsive
        public static async Task AsyncFileReadDemo()
        {
            Console.WriteLine("-- AsyncFileReadDemo --");
            string result = await File.ReadAllTextAsync(TextFilePath);
            Console.WriteLine($"[Async] Content length: {result.Length}");
        }

        // 7) Memory-mapped file example
        //    - Maps a file into memory address space for fast, random access
        //    - Excellent for large files where loading entire content is impractical
        public static void MemoryMappedFileDemo()
        {
            Console.WriteLine("-- MemoryMappedFileDemo --");
            using (var mmf = MemoryMappedFile.CreateFromFile(
                path: BinaryFilePath,
                mode: FileMode.Open))
            using (var accessor = mmf.CreateViewAccessor())
            {
                long length = accessor.Capacity;
                byte first = accessor.ReadByte(0);
                Console.WriteLine(
                    $"MemoryMappedFile first byte: 0x{first:X2} (of {length} bytes)");
            }
        }
    }
}
