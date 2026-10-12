// =====================================================================================================
// 05_ITERATION.cs
// -----------------------------------------------------------------------------------------------------
// PURPOSE:
// Introduce iteration - the process of repeating actions in sequence over data or time.
//
// Iteration is at the heart of programming. Whether counting numbers, processing data, or scanning
// through items in a list, loops let your program perform repetitive work efficiently.
// -----------------------------------------------------------------------------------------------------
//
// LESSON OBJECTIVES:
// - Understand how and when to use for, while, do-while, and foreach loops
// - Learn what "iteration" means and how it differs from "recursion"
// - Understand how iterators work behind foreach (IEnumerator / IEnumerable)
// - Explore basic array and list iteration patterns
// - Learn why advanced iterators (like yield) exist - efficiency, readability, memory savings
// - Compare with other languages' iterator models (Java, Python, etc.)
// -----------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace CS_Zero_Hero.iteration
{
    internal class Iteration
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ITERATION IN C# ===\n");

            // =================================================================================================
            // 1. WHAT IS ITERATION?
            // -------------------------------------------------------------------------------------------------
            // Iteration = repetition of steps until a condition is met.
            //
            // It's commonly achieved with loops:
            //   - for (known number of repeats)
            //   - while / do-while (unknown number, conditional)
            //   - foreach (iterate over a collection)
            //
            // Iteration differs from recursion - iteration repeats code, recursion calls itself.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("-- Basic Counting Loop (for) --");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Iteration: {i}");
            }

            // The for-loop is ideal for counting or fixed-length sequences
            // where you know the number of repetitions in advance.

            // =================================================================================================
            // 2. FOREACH LOOP - ITERATING OVER SEQUENCES
            // -------------------------------------------------------------------------------------------------
            // foreach automatically retrieves each element in a collection that implements IEnumerable.
            // It hides index logic, making code cleaner and less error-prone.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- Foreach with Array --");

            string[] planets = { "Mercury", "Venus", "Earth", "Mars" };

            foreach (string planet in planets)
            {
                Console.WriteLine($"Planet: {planet}");
            }

            // Foreach uses an *enumerator* (like Java's Iterator or Python's iter()) under the hood.
            // The compiler translates foreach into something like:
            //
            //    var enumerator = planets.GetEnumerator();
            //    while (enumerator.MoveNext())
            //    {
            //        var item = enumerator.Current;
            //        Console.WriteLine(item);
            //    }
            //
            // Benefit: You don't need to track indexes or check boundaries manually.

            // =================================================================================================
            // 3. EXPLICIT ITERATOR EXAMPLE (IEnumerable / IEnumerator)
            // -------------------------------------------------------------------------------------------------
            // In C#, any class that implements IEnumerable can be iterated over with foreach.
            // You can also iterate *manually* using an IEnumerator.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- Using IEnumerator Manually --");

            IEnumerator planetIterator = planets.GetEnumerator();

            while (planetIterator.MoveNext())
            {
                string current = (string)planetIterator.Current;
                Console.WriteLine($"[Iterator] {current}");
            }

            // WHY USE THIS?
            // - For fine-grained control over iteration (e.g., pause, resume, partial traversal)
            // - When building low-level APIs or working with data streams
            // - Common in custom collection classes or libraries that process large data sets

            // =================================================================================================
            // 4. WHILE LOOP - CONDITION-BASED ITERATION
            // -------------------------------------------------------------------------------------------------
            // While loops repeat as long as a condition is true.
            // Useful when the number of iterations isn't known beforehand.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- While Example --");
            int counter = 0;
            while (counter < 3)
            {
                Console.WriteLine($"Counter: {counter}");
                counter++;
            }

            // WHY USE THIS?
            // - When looping until an event happens (user input, sensor data, network signal)
            // - Offers more flexibility than for-loops in open-ended processes

            // =================================================================================================
            // 5. DO-WHILE LOOP - RUNS AT LEAST ONCE
            // -------------------------------------------------------------------------------------------------
            // The do-while loop executes first, then checks the condition.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- Do-While Example --");
            int num;
            do
            {
                Console.Write("Enter a number greater than 0: ");
                string? input = Console.ReadLine();
                int.TryParse(input, out num);
            } while (num <= 0);
            Console.WriteLine($"You entered: {num}");

            // WHY USE THIS?
            // - When you need the loop body to execute at least once
            //   (e.g., prompting user input or reading initial sensor data)

            // =================================================================================================
            // 6. LIST ITERATION - BASIC COLLECTION LOOP
            // -------------------------------------------------------------------------------------------------
            // Lists (dynamic arrays) can also be iterated over like arrays.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- ForEach over List<T> --");
            List<int> numbers = new List<int> { 10, 20, 30, 40, 50 };

            foreach (int n in numbers)
            {
                Console.WriteLine($"Value: {n}");
            }

            // Equivalent manual iteration:
            for (int i = 0; i < numbers.Count; i++)
            {
                Console.WriteLine($"At index {i}: {numbers[i]}");
            }

            // WHY USE LISTS?
            // - More flexible than arrays: resizable, searchable, and easy to modify
            // - Great for everyday data processing and iteration tasks

            // =================================================================================================
            // 7. CUSTOM ITERATOR - USING yield return
            // -------------------------------------------------------------------------------------------------
            // yield return allows you to define your own iterator logic.
            // Instead of returning all values at once, it "pauses" execution between results.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- Custom Iterator (yield return) --");

            foreach (int n in GetEvenNumbers(10))
            {
                Console.WriteLine($"Even number: {n}");
            }

            // WHY USE yield?
            // - Saves memory: generates items *on demand* instead of creating large temporary lists
            // - Ideal for large or infinite data streams
            // - Simplifies custom iteration logic without manually implementing IEnumerator
            // - Used internally by LINQ and many .NET APIs

            // =================================================================================================
            // SUMMARY
            // -------------------------------------------------------------------------------------------------
            // - for        -> fixed iteration count (best for numeric loops)
            // - while      -> repeat until condition changes (open-ended)
            // - do-while   -> runs at least once (input validation, initial setup)
            // - foreach    -> iterate cleanly over collections
            // - IEnumerator -> manual control for advanced iteration
            // - yield return -> efficient, lazy iteration for large or streaming data
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n=== END OF 05_ITERATION MODULE ===");
        }

        // -------------------------------------------------------------------------------------------------
        // SUPPORTING METHOD - Custom iterator using yield
        // -------------------------------------------------------------------------------------------------
        static IEnumerable<int> GetEvenNumbers(int max)
        {
            for (int i = 0; i <= max; i++)
            {
                if (i % 2 == 0)
                {
                    yield return i; // "pause" and return this value to the caller
                    // Execution resumes here on the next MoveNext()
                }
            }
        }
    }
}
