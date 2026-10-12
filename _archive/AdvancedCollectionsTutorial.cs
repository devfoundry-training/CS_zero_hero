using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_zero_hero
{
    /// <summary>
    /// This class demonstrates various collection types in .NET,
    /// from legacy non-generic collections to modern generic types.
    /// It includes practical examples, use-cases, and commentary
    /// to aid understanding of each collection's behavior and purpose.
    /// </summary>
    public class AdvancedCollectionExamples
    {
        // ----------------------------------------
        // 1. Early .NET collection (non-generic)
        // ----------------------------------------
        /// <summary>
        /// Demonstrates ArrayList, a non-generic collection used before .NET 2.0
        /// Supports mixed types but lacks compile-time safety and requires casting.
        /// </summary>
        public static void ShowArrayList()
        {
            Console.WriteLine("Early .NET (pre-Generics): ArrayList\n");

            // ArrayList can store any object, but this flexibility
            // comes at the cost of type-safety and performance.
            ArrayList list = new ArrayList();
            list.Add("Apple"); // string
            list.Add(42);        // int

            // Iteration requires treating elements as objects
            foreach (var item in list)
            {
                Console.WriteLine($"Item: {item} (Type: {item.GetType().Name})");
            }

            // Because elements are stored as objects,
            // you must cast back to the original type, risking runtime errors.
            Console.WriteLine("WARNING: Not type-safe. Avoid unless working with legacy APIs.");
        }

        // ----------------------------------------
        // 2. Generic List<T> with iteration and filtering
        // ----------------------------------------
        /// <summary>
        /// Shows List<string>, a strongly-typed, resizable array.
        /// Demonstrates simple iteration and LINQ-based filtering. Langauge intergrated query
        /// Introduced in .NET 2.0 to improve safety and performance.
        /// </summary>
        public static void ShowGenericList()
        {
            Console.WriteLine("\nGeneric List<string> (Introduced in .NET 2.0)\n");

            // Initialize with collection initializer syntax
            List<string> names = new List<string> { "Alice", "Bob", "Charlie", "Dave" };

            // 1) Iterate over elements without casting
            Console.WriteLine("All names:");
            foreach (var name in names)
            {
                Console.WriteLine($"- {name}");
            }

            // 2) Use LINQ to filter items (e.g., names starting with 'A')
            var filtered = names.Where(n => n.StartsWith("A")).ToList();
            Console.WriteLine("\nNames starting with 'A':");
            foreach (var name in filtered)
            {
                Console.WriteLine($"- {name}");
            }
        }

        // ----------------------------------------
        // 3. Queue<T> - First In, First Out (FIFO)
        // ----------------------------------------
        /// <summary>
        /// Demonstrates Queue<string>, where elements are processed
        /// in the order they were added (FIFO). Ideal for task scheduling,
        /// print jobs, or breadth-first algorithms.
        /// </summary>
        public static void ShowQueue()
        {
            Console.WriteLine("\nQueue<T> (First In, First Out - FIFO)");

            // Enqueue tasks in the order they arrive
            Queue<string> tasks = new Queue<string>();
            tasks.Enqueue("Task 1");
            tasks.Enqueue("Task 2");
            tasks.Enqueue("Task 3");

            // Dequeue processes in the same sequence as enqueued
            while (tasks.Count > 0)
            {
                string task = tasks.Dequeue();
                Console.WriteLine($"Processing: {task}");
            }

            Console.WriteLine("[OK] Queue is ideal for task scheduling, print jobs, event handling.");
        }

        // ----------------------------------------
        // 4. Dictionary<TKey, TValue> - Key-value lookup
        // ----------------------------------------
        /// <summary>
        /// Demonstrates Dictionary<string,int>, a hash-based lookup
        /// collection providing fast retrieval by key. Useful for maps,
        /// caches, and configuration stores.
        /// </summary>
        public static void ShowDictionary()
        {
            Console.WriteLine("\nDictionary<TKey, TValue> (Fast lookups by key)");

            // Initialize dictionary with key/value pairs
            Dictionary<string, int> inventory = new Dictionary<string, int>
            {
                { "Apple", 5 },
                { "Banana", 2 }
            };

            // Iterate entries (order is undefined)
            foreach (var pair in inventory)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value} items");
            }

            // Safely retrieve value by key
            if (inventory.TryGetValue("Apple", out int qty))
            {
                Console.WriteLine($"Apples in stock: {qty}");
            }

            Console.WriteLine("[OK] Useful for mappings like userID -> userData, word -> definition.");
        }

        // ----------------------------------------
        // 5. LinkedList<T> - Doubly-linked list
        // ----------------------------------------
        /// <summary>
        /// Demonstrates LinkedList<string>, a doubly-linked list where
        /// insertion and removal at any position is O(1). Used for
        /// navigation histories, undo/redo stacks, and complex structures.
        /// </summary>
        public static void ShowLinkedList()
        {
            Console.WriteLine("\nLinkedList<T> (Efficient insertion/removal from anywhere)");

            LinkedList<string> path = new LinkedList<string>();
            path.AddLast("Start");
            path.AddLast("Middle");
            path.AddLast("End");

            // Insert a checkpoint after "Start"
            var node = path.Find("Start");
            path.AddAfter(node, "Checkpoint");

            // Display the linked list sequence
            foreach (var step in path)
            {
                Console.WriteLine($"Step: {step}");
            }

            Console.WriteLine("[OK] Great for navigation history, undo/redo stacks, graph structures.");
        }

        // ----------------------------------------
        // 6. HashSet<T> - Unique value collection
        // ----------------------------------------
        /// <summary>
        /// Demonstrates HashSet<string>, which stores unique elements
        /// and provides fast membership testing. Ideal for eliminating duplicates.
        /// </summary>
        public static void ShowHashSet()
        {
            Console.WriteLine("\nHashSet<T> (Unique values only)");

            HashSet<string> emails = new HashSet<string>
            {
                "user@example.com",
                "admin@example.com"
            };

            // Attempt to add a duplicate - ignored
            emails.Add("user@example.com");

            // Display contents (order is undefined)
            foreach (var email in emails)
            {
                Console.WriteLine($"Email: {email}");
            }

            Console.WriteLine("[OK] Great for uniqueness checks, set operations (union, intersect).");
        }

        // ----------------------------------------
        // 7. SortedList<TKey, TValue> - Sorted by key
        // ----------------------------------------
        /// <summary>
        /// Demonstrates SortedList<string,int>, which maintains
        /// sorted order by key with fast index-based lookup.
        /// Useful when you need both sorted enumeration and quick access.
        /// </summary>
        public static void ShowSortedList()
        {
            Console.WriteLine("\nSortedList<TKey, TValue> (Automatically sorted by key)");

            SortedList<string, int> scores = new SortedList<string, int>
            {
                { "Bob", 88 },
                { "Alice", 95 },
                { "Charlie", 70 }
            };

            foreach (var entry in scores)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }

            Console.WriteLine("[OK] Useful when sorted order matters and fast lookup is needed.");
        }

        // ----------------------------------------
        // 8. "TreeMap" equivalent in .NET: SortedDictionary
        // ----------------------------------------
        /// <summary>
        /// Demonstrates SortedDictionary<int,string>, a
        /// balanced-tree implementation (like Java's TreeMap),
        /// keeping keys in sorted order with log(n) operations.
        /// </summary>
        public static void ShowSortedDictionary()
        {
            Console.WriteLine("\nSortedDictionary<TKey, TValue> (TreeMap equivalent)");

            SortedDictionary<int, string> idToName = new SortedDictionary<int, string>
            {
                { 101, "Alice" },
                { 103, "Charlie" },
                { 102, "Bob" }
            };

            foreach (var entry in idToName)
            {
                Console.WriteLine($"ID: {entry.Key} -> Name: {entry.Value}");
            }

            Console.WriteLine("[OK] Maintains sorted keys with tree-like performance - similar to TreeMap in Java.");
        }

        // ----------------------------------------
        // 9. Collection Framework Overview
        // ----------------------------------------
        /// <summary>
        /// Provides a quick summary of the main .NET collection types
        /// and their primary use-cases for reference.
        /// </summary>
        public static void CollectionSummary()
        {
            Console.WriteLine("\nCollection Framework Overview:");
            Console.WriteLine("- List<T>: Resizable array; use for ordered data and indexing.");
            Console.WriteLine("- Dictionary<TKey, TValue>: Fast key-value lookups; use for maps.");
            Console.WriteLine("- LinkedList<T>: Efficient insertions/deletions anywhere.");
            Console.WriteLine("- Queue<T>: FIFO; use for task scheduling and message queues.");
            Console.WriteLine("- Stack<T>: LIFO; use for undo stacks and depth-first operations.");
            Console.WriteLine("- HashSet<T>: Unique-value storage; use for set operations.");
            Console.WriteLine("- SortedList<TKey, TValue>: Sorted key-value pairs with fast lookup.");
            Console.WriteLine("- SortedDictionary<TKey, TValue>: Tree-based sorted keys (TreeMap equivalent).");
        }
    }
}
