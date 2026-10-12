// =====================================================================================================
// 09_IO.cs
// -----------------------------------------------------------------------------------------------------
// PURPOSE:
// Teach how to perform file and directory input/output operations in C#.
//
// Input/Output (I/O) is how programs read data from and write data to external sources
// such as text files, binary files, and the console.
//
// -----------------------------------------------------------------------------------------------------
//
// LESSON OBJECTIVES:
// - Understand reading and writing text files
// - Learn how to work with directories and file paths
// - Use File, FileInfo, Directory, and Path classes
// - Handle missing files or permissions safely
// -----------------------------------------------------------------------------------------------------

using System;
using System.IO;

namespace CS_Zero_Hero.io
{
    internal class IO
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== FILE I/O IN C# ===\n");

            // =================================================================================================
            // 1. WRITING TO A TEXT FILE
            // -------------------------------------------------------------------------------------------------
            // The File class provides simple static methods for quick read/write.
            // -------------------------------------------------------------------------------------------------
            string path = "sample.txt";
            string[] lines = { "Luke Skywalker", "Leia Organa", "Han Solo" };

            Console.WriteLine("Writing to file: sample.txt");
            File.WriteAllLines(path, lines);  // writes all lines to the file

            // Append a line
            File.AppendAllText(path, "\nChewbacca");

            Console.WriteLine("File written successfully.\n");

            // =================================================================================================
            // 2. READING FROM A TEXT FILE
            // -------------------------------------------------------------------------------------------------
            // Read the content back and display it.
            // -------------------------------------------------------------------------------------------------
            Console.WriteLine("-- Reading from file --");
            string[] contents = File.ReadAllLines(path);

            foreach (string line in contents)
            {
                Console.WriteLine($"Character: {line}");
            }

            // Read entire content as one string
            string allText = File.ReadAllText(path);
            Console.WriteLine("\nAs one string:");
            Console.WriteLine(allText);

            // =================================================================================================
            // 3. USING StreamWriter / StreamReader
            // -------------------------------------------------------------------------------------------------
            // These give more control over how the file is accessed (e.g., append mode).
            // -------------------------------------------------------------------------------------------------
            string path2 = "planets.txt";
            using (StreamWriter writer = new StreamWriter(path2))
            {
                writer.WriteLine("Mercury");
                writer.WriteLine("Venus");
                writer.WriteLine("Earth");
            }

            Console.WriteLine("\nReading planets with StreamReader:");
            using (StreamReader reader = new StreamReader(path2))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }

            // =================================================================================================
            // 4. DIRECTORY AND PATH UTILITIES
            // -------------------------------------------------------------------------------------------------
            // The Directory and Path classes help manage file structures.
            // -------------------------------------------------------------------------------------------------
            string dirPath = "TestDir";

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
                Console.WriteLine($"\nCreated directory: {dirPath}");
            }

            string filePath = Path.Combine(dirPath, "example.txt");
            File.WriteAllText(filePath, "This is an example file inside TestDir.");

            Console.WriteLine($"File created at: {Path.GetFullPath(filePath)}");

            Console.WriteLine("\nListing files in directory:");
            string[] files = Directory.GetFiles(dirPath);
            foreach (string f in files)
            {
                Console.WriteLine($"-> {Path.GetFileName(f)}");
            }

            // =================================================================================================
            // 5. HANDLING FILE ERRORS
            // -------------------------------------------------------------------------------------------------
            // Reading non-existent files can cause exceptions - we'll handle this properly in the next module.
            // -------------------------------------------------------------------------------------------------
            Console.WriteLine("\nAttempting to read a missing file:");
            string missing = "does_not_exist.txt";

            if (File.Exists(missing))
                Console.WriteLine(File.ReadAllText(missing));
            else
                Console.WriteLine("File not found. Skipping safely.");

            // =================================================================================================
            // SUMMARY
            // -------------------------------------------------------------------------------------------------
            // - File.ReadAllText / WriteAllText -> quick operations
            // - StreamReader / StreamWriter -> controlled access
            // - Directory / Path classes -> manage folders and paths
            // - Always check File.Exists() before reading
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n=== END OF 09_IO MODULE ===");
        }
    }
}
