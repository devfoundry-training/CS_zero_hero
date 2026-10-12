// =====================================================================================================
// 10_EXCEPTIONS.cs
// -----------------------------------------------------------------------------------------------------
// PURPOSE:
// Teach how to handle runtime errors (exceptions) gracefully in C#.
//
// Exceptions are unexpected events that disrupt program flow - such as missing files,
// invalid input, or division by zero. Instead of crashing, we can catch and handle them.
// -----------------------------------------------------------------------------------------------------
//
// LESSON OBJECTIVES:
// - Understand try/catch/finally blocks
// - Learn how to throw and rethrow exceptions
// - Handle multiple exception types
// - Create custom exceptions
// - Compare "Look Before You Leap" (LBYL) vs "Ask Forgiveness" (EAFP) styles
// -----------------------------------------------------------------------------------------------------

using System;
using System.IO;

namespace CS_Zero_Hero.exceptions
{
    internal class Exceptions
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== EXCEPTIONS IN C# ===\n");

            // =================================================================================================
            // 1. BASIC TRY / CATCH
            // -------------------------------------------------------------------------------------------------
            // Handle code that *might* fail.
            // -------------------------------------------------------------------------------------------------
            try
            {
                int x = 10;
                int y = 0;
                Console.WriteLine("Attempting division...");
                int result = x / y;
                Console.WriteLine($"Result: {result}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            // =================================================================================================
            // 2. MULTIPLE EXCEPTION TYPES
            // -------------------------------------------------------------------------------------------------
            Console.WriteLine("\n-- Multiple Exception Types --");
            try
            {
                string path = "nonexistent.txt";
                string content = File.ReadAllText(path); // may throw FileNotFoundException
                Console.WriteLine(content);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File not found: {ex.FileName}");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Access denied to file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Finally block executed (cleanup code here).");
            }

            // =================================================================================================
            // 3. THROWING EXCEPTIONS
            // -------------------------------------------------------------------------------------------------
            // You can throw exceptions manually when business logic fails.
            // -------------------------------------------------------------------------------------------------
            try
            {
                Console.WriteLine("\n-- Throwing a Custom Exception --");
                ValidateAge(-5);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Validation failed: {ex.Message}");
            }

            // =================================================================================================
            // 4. CUSTOM EXCEPTION CLASS
            // -------------------------------------------------------------------------------------------------
            try
            {
                throw new DataCorruptedException("Game save data could not be loaded.");
            }
            catch (DataCorruptedException ex)
            {
                Console.WriteLine($"Custom exception caught: {ex.Message}");
            }

            // =================================================================================================
            // 5. INNER EXCEPTIONS
            // -------------------------------------------------------------------------------------------------
            // Sometimes one exception leads to another - we can *wrap* it for context.
            // -------------------------------------------------------------------------------------------------
            try
            {
                try
                {
                    File.ReadAllText("missing.txt");
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Error while reading configuration file.", ex);
                }
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine("\nOuter exception:");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Inner exception:");
                Console.WriteLine(ex.InnerException?.Message);
            }

            // =================================================================================================
            // 6. LBYL vs. EAFP - ERROR HANDLING PHILOSOPHIES
            // -------------------------------------------------------------------------------------------------
            // These are two common patterns for handling risky operations:
            //
            //  LBYL - "Look Before You Leap"
            //          Check conditions before doing something.
            //          [OK] Avoids exceptions
            //          [X] Race conditions if the state changes between check and action
            //
            //  EAFP - "Easier to Ask Forgiveness than Permission"
            //          Try the operation and catch exceptions if they happen.
            //          [OK] Cleaner, faster when errors are rare
            //          [X] Slightly slower if exceptions are frequent
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- LBYL Example (Check Before Accessing File) --");

            string filePath = "example.txt";

            // LBYL: explicitly check conditions before doing something
            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath);
                Console.WriteLine(text);
            }
            else
            {
                Console.WriteLine("File does not exist - skipping read.");
            }

            Console.WriteLine("\n-- EAFP Example (Try It and Catch) --");

            // EAFP: try and handle errors as they occur
            try
            {
                string text = File.ReadAllText(filePath);
                Console.WriteLine(text);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found - handled gracefully.");
            }

            // When to use which:
            // - Use **LBYL** when failure is common or expected.
            // - Use **EAFP** when failure is rare and performance-critical code benefits from cleaner flow.

            // =================================================================================================
            // SUMMARY
            // -------------------------------------------------------------------------------------------------
            // - try / catch -> handle runtime errors gracefully
            // - finally -> clean up resources
            // - throw -> raise your own exceptions
            // - custom exceptions -> add domain context
            // - LBYL vs. EAFP -> two ways to manage risk
            // -------------------------------------------------------------------------------------------------
            Console.WriteLine("\n=== END OF 10_EXCEPTIONS MODULE ===");
        }

        // -------------------------------------------------------------------------------------------------
        // SUPPORTING METHOD - Demonstrates throwing exceptions
        // -------------------------------------------------------------------------------------------------
        static void ValidateAge(int age)
        {
            if (age < 0)
                throw new ArgumentOutOfRangeException(nameof(age), "Age cannot be negative.");
        }

        // -------------------------------------------------------------------------------------------------
        // CUSTOM EXCEPTION EXAMPLE
        // -------------------------------------------------------------------------------------------------
        class DataCorruptedException : Exception
        {
            public DataCorruptedException(string message) : base(message) { }
        }
    }
}
