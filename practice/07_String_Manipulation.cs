// =====================================================================================================
// TASK 08 - STRING MANIPULATION PRACTICE
// -----------------------------------------------------------------------------------------------------
// GOAL:
// Practice advanced string operations - searching, slicing, replacing, and cleaning text.
//
// This task focuses on *real-world* string handling problems, such as cleaning messy data,
// parsing formats, and reconstructing or transforming text.
// -----------------------------------------------------------------------------------------------------
//
// TASK OBJECTIVES:
// - Use Substring(), Replace(), Insert(), and Remove()
// - Practice searching and extracting substrings
// - Work with Split(), Join(), and string comparison
// - Build a text-cleaning pipeline
// -----------------------------------------------------------------------------------------------------

using System;
using System.Text;
using System.Globalization;

namespace CS_Zero_Hero.tasks.string_manipulation
{
    internal class StringManipTask
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== STRING MANIPULATION PRACTICE ===\n");

            // =================================================================================================
            // 1. CLEANING USER INPUT
            // -------------------------------------------------------------------------------------------------
            // TODO: Ask the user for a messy sentence, then:
            //  - Trim whitespace
            //  - Replace multiple spaces with one
            //  - Convert to proper case (first letter uppercase)
            // -------------------------------------------------------------------------------------------------

            Console.Write("Enter a messy sentence: ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("No input provided.");
                return;
            }

            // TODO: Step 1 - Trim whitespace
            string cleaned = /* your code here */;

            // TODO: Step 2 - Replace multiple spaces (hint: use Split + Join)
            // string[] words = ...
            // cleaned = ...

            // TODO: Step 3 - Capitalize the first letter only
            // cleaned = ...

            Console.WriteLine($"Cleaned: {cleaned}\n");

            // =================================================================================================
            // 2. SUBSTRING AND EXTRACTION
            // -------------------------------------------------------------------------------------------------
            // TODO: Given a sentence, extract certain sections using Substring and ranges.
            // -------------------------------------------------------------------------------------------------

            string line = "ProductID:12345;Name:Lightsaber;Price:$199.99";

            // TODO: Extract product ID (12345)
            string productId = /* your code here */;

            // TODO: Extract name (Lightsaber)
            string name = /* your code here */;

            // TODO: Extract price ($199.99) using range syntax
            string price = /* your code here */;

            Console.WriteLine($"Product: {name}");
            Console.WriteLine($"ID: {productId}");
            Console.WriteLine($"Price: {price}");

            // =================================================================================================
            // 3. REPLACEMENT, INSERTION, AND REMOVAL
            // -------------------------------------------------------------------------------------------------
            // TODO: Use Replace(), Insert(), and Remove() to alter text meaningfully.
            // -------------------------------------------------------------------------------------------------

            string sentence = "C# is difficult.";

            // TODO: Replace "difficult" with "powerful"
            string better = /* your code here */;

            // TODO: Insert "Learning " at the start
            // better = ...

            // TODO: Replace the last '.' with an '!' using Remove() and Insert()
            // better = ...

            Console.WriteLine($"\nBefore: {sentence}");
            Console.WriteLine($"After:  {better}");

            // =================================================================================================
            // 4. SPLITTING AND REBUILDING
            // -------------------------------------------------------------------------------------------------
            // TODO: Parse a CSV line and rebuild it in a formatted way.
            // -------------------------------------------------------------------------------------------------

            string csv = "Luke,Leia,Han,Rey,Finn";

            // TODO: Split by ',' and print each name on a new line
            // string[] names = ...

            Console.WriteLine("\n-- Split CSV --");
            // foreach (...)

            // TODO: Rebuild into a single line separated by ' | '
            // string rebuilt = ...

            Console.WriteLine($"Rebuilt: {rebuilt}");

            // =================================================================================================
            // 5. SEARCH AND ANALYSIS
            // -------------------------------------------------------------------------------------------------
            // TODO: Count how many times the word "code" appears in a paragraph (case-insensitive).
            // -------------------------------------------------------------------------------------------------

            string paragraph = "Code every day. Code with discipline. Great developers write clean CODE.";

            // int count = 0;
            // int pos = 0;
            // while (...)
            // {
            //     ...
            // }

            Console.WriteLine($"\nThe word 'code' appears ___ times.");

            // =================================================================================================
            // 6. MINI CHALLENGE - STRING CLEANUP PIPELINE
            // -------------------------------------------------------------------------------------------------
            // TODO: Given a raw user input, clean and normalize it using multiple string methods.
            // Steps:
            // - Trim
            // - Replace symbols with spaces
            // - Lowercase everything
            // - Remove duplicate spaces
            // -------------------------------------------------------------------------------------------------

            Console.Write("\nEnter raw text: ");
            string? raw = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(raw))
            {
                // TODO: Chain together methods to clean and normalize text
                // Example: raw.Trim().Replace(...).ToLower()...
                string processed = /* your code here */;

                // TODO: Remove duplicate spaces using Split + Join
                // string[] tokens = ...
                // processed = ...

                Console.WriteLine($"Processed: {processed}");
            }

            // =================================================================================================
            // SUMMARY
            // -------------------------------------------------------------------------------------------------
            // - Substring, Insert, Replace, Remove
            // - Split / Join for parsing and formatting
            // - Search using IndexOf / loops
            // - String cleaning and normalization pipelines
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n=== END OF STRING MANIPULATION TASK ===");
        }
    }
}
