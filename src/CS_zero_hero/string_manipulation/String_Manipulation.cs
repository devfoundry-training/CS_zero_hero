// =====================================================================================================
// 08_STRING_MANIPULATION.cs
// -----------------------------------------------------------------------------------------------------
// PURPOSE:
// Dive deeper into real-world string manipulation techniques in C#.
//
// By now, you understand what strings are and how they behave (immutable, reference types).
// This module explores how to *work* with them: slicing, searching, parsing, replacing, joining,
// splitting, and comparing strings efficiently and safely.
// -----------------------------------------------------------------------------------------------------
//
// LESSON OBJECTIVES:
// - Master substring extraction, search, and pattern matching
// - Learn replace, remove, insert, and pad operations
// - Handle trimming, joining, and splitting data
// - Understand efficient scanning and text building
// - Explore performance and memory considerations
// -----------------------------------------------------------------------------------------------------

using System;
using System.Text;
using System.Globalization;

namespace CS_Zero_Hero.string_manipulation
{
    internal class StringManipulation
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ADVANCED STRING MANIPULATION ===\n");

            // =================================================================================================
            // 1. SUBSTRINGS - EXTRACTING SECTIONS
            // -------------------------------------------------------------------------------------------------
            // Substring() lets you extract part of a string using start index and optional length.
            // -------------------------------------------------------------------------------------------------

            string sentence = "The quick brown fox jumps over the lazy dog";
            string firstWord = sentence.Substring(0, 3);    // "The"
            string animal = sentence.Substring(16, 3);      // "fox"
            string toEnd = sentence.Substring(31);          // "the lazy dog"

            Console.WriteLine($"First word: {firstWord}");
            Console.WriteLine($"Animal: {animal}");
            Console.WriteLine($"To end: {toEnd}");

            // NOTE: Indexing errors (e.g. too high start/length) cause exceptions - always validate lengths.

            // C# 8+ RANGE OPERATOR (cleaner slicing)
            Console.WriteLine("\n-- Range Operator --");
            Console.WriteLine(sentence[..9]);    // from start to index 9
            Console.WriteLine(sentence[4..9]);   // "quick"
            Console.WriteLine(sentence[^3..]);   // last 3 characters

            // =================================================================================================
            // 2. REPLACING, REMOVING, AND INSERTING
            // -------------------------------------------------------------------------------------------------
            // Replace() and Remove() are your primary tools for editing strings.
            // -------------------------------------------------------------------------------------------------

            string replaced = sentence.Replace("fox", "cat");
            Console.WriteLine($"\nReplaced: {replaced}");

            string removed = sentence.Remove(0, 4);     // removes first 4 characters ("The ")
            Console.WriteLine($"Removed first word: {removed}");

            string inserted = sentence.Insert(10, "small ");  // insert after "quick "
            Console.WriteLine($"Inserted: {inserted}");

            // WHY IT MATTERS:
            // - Replace() is powerful for sanitizing or templating text.
            // - Remove() and Insert() are great for parsing or reconstructing data.

            // =================================================================================================
            // 3. SPLIT() AND JOIN() - WORKING WITH ARRAYS OF WORDS
            // -------------------------------------------------------------------------------------------------
            // Strings can be broken into parts and reassembled easily.
            // -------------------------------------------------------------------------------------------------

            string csv = "apple,banana,cherry,grape";
            string[] fruits = csv.Split(',');

            Console.WriteLine("\n-- Split Example --");
            foreach (string f in fruits)
            {
                Console.WriteLine($"Fruit: {f}");
            }

            string joined = string.Join(" | ", fruits);
            Console.WriteLine($"\nJoined: {joined}");

            // Split() is also great for parsing file formats, input data, or logs.

            // =================================================================================================
            // 4. SEARCHING AND SCANNING
            // -------------------------------------------------------------------------------------------------
            // Methods like IndexOf(), LastIndexOf(), Contains(), and StartsWith()
            // help locate specific substrings or characters.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- Searching and Scanning --");

            string phrase = "C# makes string work fun and powerful.";

            Console.WriteLine($"Contains 'fun': {phrase.Contains("fun")}");
            Console.WriteLine($"IndexOf 'string': {phrase.IndexOf("string")}");
            Console.WriteLine($"LastIndexOf 'and': {phrase.LastIndexOf("and")}");
            Console.WriteLine($"StartsWith 'C#': {phrase.StartsWith("C#")}");
            Console.WriteLine($"EndsWith '.': {phrase.EndsWith(".")}");

            // Case-insensitive search:
            Console.WriteLine($"Contains 'POWERFUL' (ignore case): {phrase.Contains("POWERFUL", StringComparison.OrdinalIgnoreCase)}");

            // For cultural/language-sensitive text, use CultureInfo-aware comparisons.

            // =================================================================================================
            // 5. CLEANING AND NORMALIZATION
            // -------------------------------------------------------------------------------------------------
            // Trim, Pad, and Normalize are essential for preparing text for display or comparison.
            // -------------------------------------------------------------------------------------------------

            string messy = "   Hello World!   ";
            Console.WriteLine($"\nTrimmed: '{messy.Trim()}'");

            Console.WriteLine($"PadLeft(20): '{messy.Trim().PadLeft(20)}'");
            Console.WriteLine($"PadRight(20, '.') : '{messy.Trim().PadRight(20, '.')}'");

            // Normalize ensures consistent character encoding (important in globalized apps)
            string accented = "café";
            string normalized = accented.Normalize(NormalizationForm.FormC);
            Console.WriteLine($"Normalized: {normalized}");

            // =================================================================================================
            // 6. PARSING AND EXTRACTION PATTERNS
            // -------------------------------------------------------------------------------------------------
            // Example: Extracting key/value pairs or parsing structured strings.
            // -------------------------------------------------------------------------------------------------

            string data = "name=Luke;role=Jedi;rank=Master";

            string[] pairs = data.Split(';');
            foreach (string p in pairs)
            {
                string[] kv = p.Split('=');
                string key = kv[0];
                string value = kv.Length > 1 ? kv[1] : "";
                Console.WriteLine($"{key,-6} -> {value}");
            }

            // TIP: Split with StringSplitOptions.RemoveEmptyEntries to clean whitespace or blank tokens.

            // =================================================================================================
            // 7. STRING COMPARISONS & CULTURE
            // -------------------------------------------------------------------------------------------------
            // Compare strings with or without case sensitivity.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- Comparison Examples --");
            string s1 = "resume";
            string s2 = "résumé";

            bool equalInvariant = string.Equals(s1, s2, StringComparison.InvariantCultureIgnoreCase);
            Console.WriteLine($"Invariant comparison: {equalInvariant}");

            bool equalOrdinal = string.Equals(s1, s2, StringComparison.OrdinalIgnoreCase);
            Console.WriteLine($"Ordinal comparison: {equalOrdinal}");

            // Ordinal = byte-by-byte (fastest)
            // InvariantCulture = linguistically aware
            // Use Ordinal for technical data, Invariant for user-facing text.

            // =================================================================================================
            // 8. ADVANCED: CHARACTER AND TOKEN ANALYSIS
            // -------------------------------------------------------------------------------------------------
            // Loop through characters for low-level processing (e.g., counting or filtering).
            // -------------------------------------------------------------------------------------------------

            string sample = "aBcDeF123!";
            int digits = 0, letters = 0;

            foreach (char c in sample)
            {
                if (char.IsDigit(c)) digits++;
                if (char.IsLetter(c)) letters++;
            }

            Console.WriteLine($"\nLetters: {letters}, Digits: {digits}");

            // You can also use char methods like IsWhiteSpace, IsUpper, ToUpperInvariant, etc.

            // =================================================================================================
            // 9. PERFORMANCE - STRINGBUILDER VS STRING
            // -------------------------------------------------------------------------------------------------
            // StringBuilder is faster for large concatenations or loops.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- Performance Note --");

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 5; i++)
            {
                sb.Append($"Line {i}\n");
            }

            string result = sb.ToString();
            Console.WriteLine(result);

            // Use StringBuilder when:
            // - Building large text dynamically
            // - Modifying strings inside loops
            // - Reducing memory pressure from multiple immutable string allocations

            // =================================================================================================
            // 10. CHAINING OPERATIONS (REAL EXAMPLE)
            // -------------------------------------------------------------------------------------------------
            // Combining multiple methods together is common in real-world text processing.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- Real Example: Data Cleaning --");

            string raw = "  john,DOE ,  developer  ";

            string cleaned = string.Join(" ",
                raw.Trim()
                   .ToLower()
                   .Replace(",", " ")
                   .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            );

            Console.WriteLine($"Before: '{raw}'");
            Console.WriteLine($"After:  '{cleaned}'");

            // =================================================================================================
            // SUMMARY
            // -------------------------------------------------------------------------------------------------
            // - Substring, slicing, and range extraction
            // - Replace, Insert, Remove, and Rebuild
            // - Split and Join for text parsing
            // - IndexOf, Contains, and LastIndexOf for searching
            // - Trim, Pad, and Normalize for cleanup
            // - StringBuilder for performance
            // - Chaining for powerful text pipelines
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n=== END OF 08_STRING_MANIPULATION MODULE ===");
        }
    }
}
