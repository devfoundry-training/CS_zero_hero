using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_zero_hero
{

    // ==============================
    // File: ExceptionHandling.cs
    // ==============================
    // This file demonstrates exception handling in C#.
    // Concepts shown:
    //  - Basic try/catch/finally
    //  - Multiple catch blocks for different exception types
    //  - "Look Before You Leap" (LBYL) vs "Easier to Ask Forgiveness than Permission" (EAFP)
    //  - Exception chaining (InnerException) to preserve root causes



    namespace CSTutorial
    {
        public static class ExceptionHandling
        {
            public static void methodToHold(string[] args)
            {
                string path = "nonexistent.txt";

                // Basic try/catch/finally
                StreamReader reader = null;
                try
                {
                    reader = new StreamReader(path);
                    Console.WriteLine(reader.ReadToEnd());
                }
                catch (FileNotFoundException fnfEx)
                {
                    Console.Error.WriteLine($"File not found: {fnfEx.Message}");
                }
                catch (IOException ioEx)
                {
                    Console.Error.WriteLine($"I/O error: {ioEx.Message}");
                }
                finally
                {
                    if (reader != null)
                    {
                        try
                        {
                            reader.Close();
                        }
                        catch (IOException closeEx)
                        {
                            Console.Error.WriteLine($"Error closing file: {closeEx.Message}");
                        }
                    }
                }

                // LBYL example: check before opening
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists && fileInfo.Length < 10_000)
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        Console.WriteLine(sr.ReadToEnd());
                    }
                }
                else
                {
                    Console.WriteLine("File either does not exist or is too large.");
                }

                // EAFP example: try and catch
                try
                {
                    using (StreamReader sr = new StreamReader(path))
                    {
                        Console.WriteLine(sr.ReadToEnd());
                    }
                }
                catch (Exception ex) when (ex is FileNotFoundException || ex is UnauthorizedAccessException)
                {
                    Console.Error.WriteLine($"Access issue: {ex.Message}");
                }

                // Exception chaining: preserve inner exception
                try
                {
                    ReadAndProcess(path);
                }
                catch (CustomDataException cde)
                {
                    Console.Error.WriteLine(cde.Message);
                    Console.Error.WriteLine("Inner exception details:");
                    Console.Error.WriteLine(cde.InnerException);
                }
            }

            private static void ReadAndProcess(string filePath)
            {
                try
                {
                    string content = File.ReadAllText(filePath);
                    // Imagine processing that may throw
                    ThrowIfInvalid(content);
                }
                catch (Exception ex)
                {
                    // Wrap original exception with more context
                    throw new CustomDataException($"Failed processing file '{filePath}'", ex);
                }
            }

            private static void ThrowIfInvalid(string data)
            {
                if (string.IsNullOrWhiteSpace(data))
                {
                    throw new ArgumentException("Data is empty or whitespace");
                }
            }
        }

        // Custom exception type to demonstrate chaining
        public class CustomDataException : Exception
        {
            public CustomDataException(string message, Exception inner)
                : base(message, inner) { }
        }
    }

}