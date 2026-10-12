// =====================================================================================================
// 17_LINQ.cs
// -----------------------------------------------------------------------------------------------------
// PURPOSE:
// Introduce Language Integrated Query (LINQ) - a feature that brings SQL-like querying
// capabilities directly into C# for working with collections and data.
//
// LINQ lets you query, filter, transform, and combine data in a clear, expressive, and type-safe way.
// -----------------------------------------------------------------------------------------------------
//
// LESSON OBJECTIVES:
// - Understand what LINQ is and why it's useful
// - Learn the two syntaxes: Query syntax and Method (Lambda) syntax
// - Use LINQ to filter, project, sort, and group data
// - Apply LINQ on arrays, lists, and custom objects
// - Compare query comprehension vs fluent method style
// -----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;  // Required for LINQ extensions

namespace CS_Zero_Hero.linq
{
    internal class LinqDemo
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== LANGUAGE INTEGRATED QUERY (LINQ) ===\n");

            // =================================================================================================
            // 1. WHAT IS LINQ?
            // -------------------------------------------------------------------------------------------------
            // LINQ stands for Language Integrated Query.
            //
            // It allows you to query in-memory collections (like arrays, lists, or dictionaries)
            // using a SQL-like syntax, but with full compiler checking and IntelliSense support.
            //
            // LINQ can be used with:
            // - Arrays
            // - Lists
            // - XML
            // - Databases (via LINQ to SQL / Entity Framework)
            // -------------------------------------------------------------------------------------------------

            int[] numbers = { 1, 5, 8, 3, 10, 15, 2, 6, 7 };

            // Query syntax (similar to SQL)
            var querySyntax =
                from n in numbers
                where n > 5
                orderby n descending
                select n;

            // Method syntax (lambda expressions)
            var methodSyntax = numbers
                .Where(n => n > 5)
                .OrderByDescending(n => n);

            Console.WriteLine("-- Query Syntax Result --");
            foreach (var n in querySyntax)
                Console.WriteLine(n);

            Console.WriteLine("\n-- Method Syntax Result --");
            foreach (var n in methodSyntax)
                Console.WriteLine(n);

            // =================================================================================================
            // 2. FILTERING AND PROJECTION
            // -------------------------------------------------------------------------------------------------
            // Filtering -> where()
            // Projection -> select()
            // -------------------------------------------------------------------------------------------------

            List<string> names = new List<string> { "Luke", "Leia", "Han", "Lando", "Rey", "Finn" };

            var filtered = from name in names
                           where name.StartsWith("L")
                           select name.ToUpper();

            Console.WriteLine("\nNames starting with 'L' (uppercase):");
            foreach (var name in filtered)
                Console.WriteLine(name);

            // Equivalent method syntax
            var filteredLambda = names
                .Where(n => n.StartsWith("L"))
                .Select(n => n.ToUpper());

            // =================================================================================================
            // 3. ORDERING AND LIMITING RESULTS
            // -------------------------------------------------------------------------------------------------
            // LINQ supports ordering (OrderBy / OrderByDescending)
            // and limiting (Take / Skip)
            // -------------------------------------------------------------------------------------------------

            var ordered = names.OrderBy(n => n.Length).ThenBy(n => n);
            Console.WriteLine("\nOrdered by length then alphabetically:");
            foreach (var name in ordered)
                Console.WriteLine(name);

            Console.WriteLine("\nFirst 3 names:");
            foreach (var name in names.Take(3))
                Console.WriteLine(name);

            // =================================================================================================
            // 4. AGGREGATION AND STATISTICS
            // -------------------------------------------------------------------------------------------------
            // LINQ provides aggregation methods:
            // Sum(), Count(), Average(), Min(), Max(), Any(), All()
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- Aggregation Examples --");
            Console.WriteLine($"Count: {numbers.Count()}");
            Console.WriteLine($"Sum: {numbers.Sum()}");
            Console.WriteLine($"Average: {numbers.Average():F2}");
            Console.WriteLine($"Min: {numbers.Min()}");
            Console.WriteLine($"Max: {numbers.Max()}");
            Console.WriteLine($"Any > 10? {numbers.Any(n => n > 10)}");
            Console.WriteLine($"All > 0? {numbers.All(n => n > 0)}");

            // =================================================================================================
            // 5. GROUPING AND JOINING
            // -------------------------------------------------------------------------------------------------
            // LINQ can group elements based on a key - like SQL's GROUP BY.
            // -------------------------------------------------------------------------------------------------

            var grouped = from name in names
                          group name by name[0] into nameGroup
                          orderby nameGroup.Key
                          select nameGroup;

            Console.WriteLine("\n-- Grouped by first letter --");
            foreach (var group in grouped)
            {
                Console.WriteLine($"Names starting with '{group.Key}':");
                foreach (var n in group)
                    Console.WriteLine($"  {n}");
            }

            // Example join (students and grades)
            var students = new[]
            {
                new { ID = 1, Name = "Luke" },
                new { ID = 2, Name = "Leia" },
                new { ID = 3, Name = "Han" }
            };

            var grades = new[]
            {
                new { StudentID = 1, Grade = "A" },
                new { StudentID = 2, Grade = "B" },
                new { StudentID = 3, Grade = "A" }
            };

            var joined = from s in students
                         join g in grades on s.ID equals g.StudentID
                         select new { s.Name, g.Grade };

            Console.WriteLine("\n-- Student Grades (Join Example) --");
            foreach (var entry in joined)
                Console.WriteLine($"{entry.Name} -> {entry.Grade}");

            // =================================================================================================
            // 6. CUSTOM OBJECT QUERIES
            // -------------------------------------------------------------------------------------------------
            // LINQ works seamlessly with your own classes and types.
            // -------------------------------------------------------------------------------------------------

            List<Product> products = new List<Product>
            {
                new Product { Name = "Lightsaber", Price = 150.0 },
                new Product { Name = "Blaster", Price = 90.0 },
                new Product { Name = "Thermal Detonator", Price = 250.0 }
            };

            var expensive = products
                .Where(p => p.Price > 100)
                .OrderByDescending(p => p.Price)
                .Select(p => p.Name);

            Console.WriteLine("\nExpensive Products:");
            foreach (var p in expensive)
                Console.WriteLine(p);

            // =================================================================================================
            // SUMMARY
            // -------------------------------------------------------------------------------------------------
            // - LINQ provides query-like operations directly on objects
            // - Works with arrays, lists, dictionaries, and custom types
            // - Query syntax vs Method (Lambda) syntax
            // - Supports filtering, sorting, grouping, joining, and aggregation
            // -------------------------------------------------------------------------------------------------
            Console.WriteLine("\n=== END OF 12_LINQ MODULE ===");
        }
    }

    // Simple product class for demonstration
    public class Product
    {
        public string Name { get; set; } = "";
        public double Price { get; set; }
    }
}
