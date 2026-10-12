// =====================================================================================================
// 06_COLLECTIONS.cs
// -----------------------------------------------------------------------------------------------------
// PURPOSE:
// Introduce C# collections - the classes and interfaces used to store and manage groups of data.
//
// Collections provide dynamic storage for related items. Unlike arrays, they can resize automatically,
// perform lookups efficiently, and provide specialized data-handling behavior.
// -----------------------------------------------------------------------------------------------------
//
// LESSON OBJECTIVES:
// - Understand the role of collections in C#
// - Learn the core collection types: List<T>, Dictionary<TKey, TValue>, Queue<T>, Stack<T>, and HashSet<T>
// - Explore their most common methods (Add, Remove, Contains, Count, etc.)
// - Understand the concept of generics (<T>)
// - Compare collection use cases and efficiency
// -----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace CS_Zero_Hero.collections
{
    internal class Collections
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== COLLECTIONS IN C# ===\n");

            // =================================================================================================
            // 1. ARRAYS VS COLLECTIONS
            // -------------------------------------------------------------------------------------------------
            // Arrays are fixed in size and type-safe. Collections (like List<T>) are flexible and dynamic.
            // -------------------------------------------------------------------------------------------------

            string[] arrayFruits = { "Apple", "Banana", "Cherry" };
            Console.WriteLine("-- Array Example --");
            Console.WriteLine($"First fruit: {arrayFruits[0]}");
            Console.WriteLine($"Array length: {arrayFruits.Length}");

            // =================================================================================================
            // 2. LIST<T> - RESIZABLE, INDEXED COLLECTION
            // -------------------------------------------------------------------------------------------------
            // List<T> is the most commonly used collection.
            // It allows random access, insertion, and removal of elements.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- List<T> Example --");

            List<string> fruits = new List<string> { "Apple", "Banana", "Cherry" };
            fruits.Add("Durian");
            fruits.Insert(1, "Mango");   // Insert at index 1
            fruits.Remove("Banana");     // Remove by value
            fruits.RemoveAt(0);          // Remove by index

            Console.WriteLine($"Contains 'Cherry'? {fruits.Contains("Cherry")}");
            Console.WriteLine($"Total fruits: {fruits.Count}");

            Console.WriteLine("Current list contents:");
            foreach (string f in fruits)
                Console.WriteLine($" - {f}");

            fruits.Sort();
            fruits.Reverse();

            Console.WriteLine("\nSorted & Reversed:");
            foreach (string f in fruits)
                Console.WriteLine($" - {f}");

            // =================================================================================================
            // 3. DICTIONARY<TKey, TValue> - KEY/VALUE PAIRS
            // -------------------------------------------------------------------------------------------------
            // Ideal for associating unique keys (like IDs) with values (like names).
            // Provides fast lookups by key.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- Dictionary<TKey, TValue> Example --");

            Dictionary<int, string> studentGrades = new Dictionary<int, string>
            {
                {1, "Alice"},
                {2, "Bob"},
                {3, "Charlie"}
            };

            Console.WriteLine($"Student #2: {studentGrades[2]}");

            studentGrades[2] = "Bobby"; // Modify existing value

            if (studentGrades.ContainsKey(3))
                Console.WriteLine("Student #3 exists!");

            foreach (var kvp in studentGrades)
                Console.WriteLine($"ID: {kvp.Key}, Name: {kvp.Value}");

            studentGrades.Remove(1);

            // =================================================================================================
            // 4. QUEUE<T> - FIRST-IN, FIRST-OUT (FIFO)
            // -------------------------------------------------------------------------------------------------
            // Useful for processing tasks in the order they arrive (e.g. print queues).
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- Queue<T> Example --");

            Queue<string> tasks = new Queue<string>();
            tasks.Enqueue("Task 1");
            tasks.Enqueue("Task 2");
            tasks.Enqueue("Task 3");

            Console.WriteLine($"Next to process: {tasks.Peek()}");

            while (tasks.Count > 0)
            {
                string next = tasks.Dequeue();
                Console.WriteLine($"Processing: {next}");
            }

            // =================================================================================================
            // 5. STACK<T> - LAST-IN, FIRST-OUT (LIFO)
            // -------------------------------------------------------------------------------------------------
            // Used for undo/redo, backtracking, and nested evaluation.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- Stack<T> Example --");

            Stack<string> history = new Stack<string>();
            history.Push("Page 1");
            history.Push("Page 2");
            history.Push("Page 3");

            Console.WriteLine($"Top of stack: {history.Peek()}");

            while (history.Count > 0)
                Console.WriteLine($"Going back from: {history.Pop()}");

            // =================================================================================================
            // 6. HASHSET<T> - UNIQUE ELEMENT COLLECTION
            // -------------------------------------------------------------------------------------------------
            // A HashSet stores unique items and supports efficient set operations:
            //   Union, Intersection, Difference, Symmetric Difference
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- HashSet<T> Example --");

            HashSet<string> employees = new HashSet<string> { "Luke", "Ed", "Bob", "Mary" };
            HashSet<string> students = new HashSet<string> { "Luke", "Alice", "Bob", "Peter" };

            Console.WriteLine("Employees: " + string.Join(", ", employees));
            Console.WriteLine("Students:  " + string.Join(", ", students));

            // Union -> all unique elements from both sets
            var union = new HashSet<string>(employees);
            union.UnionWith(students);
            Console.WriteLine("\nUnion (A or B): " + string.Join(", ", union));

            // Intersection -> elements common to both sets
            var intersect = new HashSet<string>(employees);
            intersect.IntersectWith(students);
            Console.WriteLine("Intersection (A and B): " + string.Join(", ", intersect));

            // Difference -> elements in A but not in B
            var diff = new HashSet<string>(employees);
            diff.ExceptWith(students);
            Console.WriteLine("Difference (A - B): " + string.Join(", ", diff));

            // Symmetric Difference -> elements unique to each set
            var symDiff = new HashSet<string>(employees);
            symDiff.SymmetricExceptWith(students);
            Console.WriteLine("Symmetric Difference (A xor B): " + string.Join(", ", symDiff));

            // =================================================================================================
            // 7. COLLECTION INTERFACES
            // -------------------------------------------------------------------------------------------------
            // IEnumerable<T> - allows foreach iteration
            // ICollection<T> - adds Count, Add, Remove, Clear
            // IList<T> - adds index-based access
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- Interfaces Summary --");
            Console.WriteLine("IEnumerable<T>: supports foreach (read-only iteration)");
            Console.WriteLine("ICollection<T>: adds Count, Add, Remove, Clear");
            Console.WriteLine("IList<T>: adds index access and ordering");

            PrintAll(fruits);

            // =================================================================================================
            // SUMMARY
            // -------------------------------------------------------------------------------------------------
            // - List<T> -> dynamic, ordered, indexed
            // - Dictionary<TKey, TValue> -> key-based lookup
            // - Queue<T> -> first-in, first-out
            // - Stack<T> -> last-in, first-out
            // - HashSet<T> -> unique elements and set operations
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n=== END OF 06_COLLECTIONS MODULE ===");
        }

        // -------------------------------------------------------------------------------------------------
        // SUPPORTING METHOD - Demonstrate IEnumerable parameter
        // -------------------------------------------------------------------------------------------------
        static void PrintAll(IEnumerable<string> collection)
        {
            Console.WriteLine("\nPrinting via IEnumerable:");
            foreach (string item in collection)
                Console.WriteLine($" - {item}");
        }
    }
}
