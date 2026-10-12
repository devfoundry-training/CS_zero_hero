// =====================================================================================================
// TASK 10B - EXCEPTION HANDLING (APPLIED PRACTICE)
// -----------------------------------------------------------------------------------------------------
// GOAL:
// Practise identifying and handling specific exceptions in realistic code.
//
// This task reinforces:
// - Matching exception types to problems
// - Using try / catch / finally blocks correctly
// - Understanding LBYL (Look Before You Leap) and EAFP (Easier to Ask Forgiveness than Permission)
// -----------------------------------------------------------------------------------------------------
//
// INSTRUCTIONS:
//
// 1. Create a new console project:
//
//        dotnet new console -n ExceptionHandlingAdvanced
//        cd ExceptionHandlingAdvanced
//
// 2. Replace Program.cs with this file and complete the missing pieces (___).
// -----------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Text;

namespace CS_Zero_Hero.tasks.exceptions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ADVANCED EXCEPTION HANDLING PRACTICE ===\n");

            // =================================================================================================
            // 1. BASIC TRY / CATCH / FINALLY
            // -------------------------------------------------------------------------------------------------
            // Divide by zero is a common runtime error.
            // TODO:
            // - Use the correct exception type in the catch block.
            // - Add a message to the finally block.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("-- Division Example --");

            int x = 10;
            int y = 0;

            try
            {
                Console.WriteLine("Attempting division...");
                int result = x / y; // this will throw an exception
                Console.WriteLine($"Result: {result}");
            }
            catch (___) // TODO: which exception type handles dividing by zero?
            {
                Console.WriteLine("You cannot divide by zero!");
            }
            catch (___) // TODO: fallback catch-all for unexpected exceptions
            {
                Console.WriteLine("Something else went wrong.");
            }
            finally
            {
                Console.WriteLine("TODO: Add a message explaining why finally always runs.");
            }

            // =================================================================================================
            // 2. THROWING EXCEPTIONS MANUALLY
            // -------------------------------------------------------------------------------------------------
            // Sometimes you need to stop execution and explain *why* it failed.
            // TODO:
            // - Use 'throw' to manually raise an exception.
            // - Catch it and print the message.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- Manual Exception Throw --");

            try
            {
                // TODO: Throw a generic exception with a helpful message.
                throw new ___("Something went wrong - explain the reason here!");
            }
            catch (___ ex)
            {
                Console.WriteLine($"Caught: {ex.Message}");
            }

            // =================================================================================================
            // 3. LBYL - LOOK BEFORE YOU LEAP
            // -------------------------------------------------------------------------------------------------
            // Check for a file before attempting to read.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- LBYL Example --");

            string path = "example.txt";

            if (___) // TODO: condition to check if the file exists
            {
                string text = File.ReadAllText(path);
                Console.WriteLine($"File contents:\n{text}");
            }
            else
            {
                Console.WriteLine("File not found, skipping read operation.");
            }

            // =================================================================================================
            // 4. EAFP - EASIER TO ASK FORGIVENESS THAN PERMISSION
            // -------------------------------------------------------------------------------------------------
            // Try first, catch errors if they occur.
            // TODO:
            // - Wrap in try/catch
            // - Handle FileNotFoundException specifically
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- EAFP Example --");

            try
            {
                string text = File.ReadAllText(path, Encoding.UTF8);
                Console.WriteLine(text);
            }
            catch (___ ex) // TODO: what specific file-related exception fits here?
            {
                Console.WriteLine($"File missing: {ex.Message}");
            }
            catch (___ ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

            // =================================================================================================
            // 5. CUSTOM VALIDATION METHOD (CHALLENGE)
            // -------------------------------------------------------------------------------------------------
            // Validate a user input using ArgumentOutOfRangeException.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- Validation Example --");

            Console.Write("Enter your age: ");
            int.TryParse(Console.ReadLine(), out int age);

            try
            {
                ValidateAge(age);
                Console.WriteLine("Valid age entered.");
            }
            catch (___ ex) // TODO: what exception type is thrown for out-of-range values?
            {
                Console.WriteLine($"Validation failed: {ex.Message}");
            }

            Console.WriteLine("\n=== END OF EXCEPTION HANDLING PRACTICE ===");
        }

        // -------------------------------------------------------------------------------------------------
        // SUPPORTING METHOD - CUSTOM VALIDATION
        // -------------------------------------------------------------------------------------------------
        static void ValidateAge(int age)
        {
            // TODO: Throw an appropriate exception if the age is negative or unrealistic.
            if (age < 0 || age > 120)
                throw new ___(nameof(age), "Age must be between 0 and 120.");
        }
    }
}
