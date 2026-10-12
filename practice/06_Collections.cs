// =====================================================================================================
// 06_COLLECTIONS_TASK.cs
// -----------------------------------------------------------------------------------------------------
// PURPOSE:
// Apply your understanding of arrays, lists, dictionaries, and sets in C#.
//
// You'll create, modify, and iterate through various collections - performing sorting, searching,
// and aggregation along the way. This exercise reinforces iteration, data storage, and efficiency.
// -----------------------------------------------------------------------------------------------------
//
// TASK OBJECTIVES:
// - Use arrays and lists to store and modify data
// - Practice searching, sorting, and filtering collections
// - Work with Dictionary<TKey, TValue> for key/value storage
// - Explore HashSet for uniqueness
// - Understand iteration and LINQ-style patterns (intro-level)
// -----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace CS_Zero_Hero.tasks.collections
{
    internal class CollectionsTask
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== COLLECTIONS PRACTICE ===\n");

            // =================================================================================================
            // 1. ARRAYS - FIXED SIZE COLLECTIONS
            // -------------------------------------------------------------------------------------------------
            // TODO: Create an array of 5 integers (1-5) and print them using a loop.

            int[] numbers = { 1, 2, 3, 4, 5 };
            Console.WriteLine("-- Array Example --");
            foreach (int n in numbers)
            {
                Console.WriteLine($"Number: {n}");
            }

            // TODO: Calculate the sum and average of the array.
            int sum = 0;
            foreach (int n in numbers) sum += n;
            double avg = (double)sum / numbers.Length;

            Console.WriteLine($"Sum = {sum}, Average = {avg}\n");

            // =================================================================================================
            // 2. LIST<T> - DYNAMIC COLLECTIONS
            // -------------------------------------------------------------------------------------------------
            // Lists grow and shrink dynamically - perfect for user-driven data.
            // -------------------------------------------------------------------------------------------------

            List<string> students = new List<string> { "Luke", "Leia", "Han" };

            Console.WriteLine("-- List Example --");
            students.Add("Rey");
            students.Remove("Han");

            // TODO: Sort alphabetically and print.
            students.Sort();
            foreach (string s in students)
            {
                Console.WriteLine(s);
            }

            // TODO: Ask the user to enter a name and check if it exists.
            Console.Write("\nEnter a student to search: ");
            string? input = Console.ReadLine();
            if (students.Contains(input ?? "", StringComparer.OrdinalIgnoreCase))
                Console.WriteLine("Found!");
            else
                Console.WriteLine("Not found.");

            // =================================================================================================
            // 3. DICTIONARY<TKey, TValue> - KEY/VALUE COLLECTIONS
            // -------------------------------------------------------------------------------------------------
            // Ideal for lookups, configurations, and mapping data relationships.
            // -------------------------------------------------------------------------------------------------

            Dictionary<string, int> scores = new Dictionary<string, int>
            {
                { "Luke", 85 },
                { "Leia", 92 },
                { "Rey", 78 }
            };

            Console.WriteLine("\n-- Dictionary Example --");
            foreach (var pair in scores)
            {
                Console.WriteLine($"{pair.Key,-10} -> {pair.Value}");
            }

            // TODO: Prompt user for a name and print the score if it exists.
            Console.Write("\nEnter a student name to view score: ");
            string? query = Console.ReadLine();

            if (query != null && scores.ContainsKey(query))
                Console.WriteLine($"{query}'s score: {scores[query]}");
            else
                Console.WriteLine("Student not found.");

            // =================================================================================================
            // 4. HASHSET<T> - UNIQUE COLLECTIONS
            // -------------------------------------------------------------------------------------------------
            // A HashSet only stores unique items (no duplicates).
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- HashSet Example --");

            HashSet<string> uniquePlanets = new HashSet<string>
            {
                "Mercury", "Venus", "Earth", "Mars", "Earth"
            };

            // TODO: Add "Jupiter" and print all planets.
            uniquePlanets.Add("Jupiter");

            foreach (string p in uniquePlanets)
                Console.WriteLine(p);

            Console.WriteLine($"Total unique planets: {uniquePlanets.Count}");

            // =================================================================================================
            // 5. COMBINED EXERCISE - SIMPLE DATA PIPELINE
            // -------------------------------------------------------------------------------------------------
            // Use Lists, Dictionaries, and loops to build a mini grade book.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- Mini Grade Book --");

            List<string> names = new List<string> { "Luke", "Leia", "Rey", "Finn" };
            Dictionary<string, int> grades = new Dictionary<string, int>();

            // TODO: Assign random grades (0-100) for each student.
            Random rand = new Random();
            foreach (string n in names)
                grades[n] = rand.Next(60, 100);

            Console.WriteLine("\nStudent Grades:");
            foreach (var g in grades)
                Console.WriteLine($"{g.Key,-10} -> {g.Value}");

            // TODO: Find highest and lowest scores.
            int maxScore = grades.Values.Max();
            int minScore = grades.Values.Min();

            Console.WriteLine($"\nTop Score: {maxScore}");
            Console.WriteLine($"Lowest Score: {minScore}");

            // =================================================================================================
            // SUMMARY
            // -------------------------------------------------------------------------------------------------
            // - Arrays - fixed, fast, basic
            // - Lists - dynamic, easy for iteration
            // - Dictionaries - fast lookups with keys
            // - HashSets - enforce uniqueness
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n=== END OF COLLECTIONS TASK ===");
        }
    }
}
