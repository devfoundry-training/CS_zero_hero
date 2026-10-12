// =====================================================================================================
// 02_DATATYPES.cs
// -----------------------------------------------------------------------------------------------------
// PURPOSE:
// Introduce C#'s type system - the foundation of all variables, logic, and objects.
//
// This module explains how types are represented in memory, what "value" vs "reference"
// means, how the compiler enforces type safety, and how common built-in types work.
// -----------------------------------------------------------------------------------------------------

using System;

namespace CS_Zero_Hero.datatypes
{
    internal class DataTypes
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== C# DATA TYPES ===\n");

            // =================================================================================================
            // VALUE TYPES - STORED DIRECTLY ON THE STACK
            // -------------------------------------------------------------------------------------------------
            // Value types contain their data *directly*. When you copy them, the data is duplicated.
            // Examples: int, double, bool, char, struct, enum.
            // -------------------------------------------------------------------------------------------------
            Console.WriteLine("- VALUE TYPES (stored on the stack)\n");

            int intExample = 42;         // 32-bit integer
            double doubleExample = 3.14159; // 64-bit floating point
            bool boolExample = true;     // Boolean value (true or false)
            char charExample = 'A';      // Single character (Unicode)

            // Display example values
            Console.WriteLine($"intExample = {intExample}");
            Console.WriteLine($"doubleExample = {doubleExample}");
            Console.WriteLine($"boolExample = {boolExample}");
            Console.WriteLine($"charExample = {charExample}");

            // -------------------------------------------------------------------------------------------------
            // Display numeric range boundaries for visualization.
            // Each primitive type has static MinValue and MaxValue properties.
            // -------------------------------------------------------------------------------------------------
            Console.WriteLine("\n-- Numeric Ranges --");
            Console.WriteLine($"byte:   {byte.MinValue} to {byte.MaxValue}");
            Console.WriteLine($"short:  {short.MinValue} to {short.MaxValue}");
            Console.WriteLine($"int:    {int.MinValue} to {int.MaxValue}");
            Console.WriteLine($"long:   {long.MinValue} to {long.MaxValue}");
            Console.WriteLine($"float:  {float.MinValue} to {float.MaxValue}");
            Console.WriteLine($"double: {double.MinValue} to {double.MaxValue}");
            Console.WriteLine($"decimal:{decimal.MinValue} to {decimal.MaxValue}");

            // -------------------------------------------------------------------------------------------------
            // Demonstrate copy behavior.
            // Value types are *copied by value*, meaning each variable holds its own copy of the data.
            // -------------------------------------------------------------------------------------------------
            int a = 10;
            int b = a; // makes a full copy of the value (a and b are independent)
            b = 20;    // changing b does NOT affect a
            Console.WriteLine($"\nCopy behavior: a = {a}, b = {b} (value types are independent)");

            // =================================================================================================
            // REFERENCE TYPES - STORED ON THE HEAP
            // -------------------------------------------------------------------------------------------------
            // Reference types store *references* (pointers) to data on the heap.
            // Copying a reference variable copies the pointer, not the data.
            // Examples: string, class, array, interface, delegate.
            // -------------------------------------------------------------------------------------------------
            Console.WriteLine("\n- REFERENCE TYPES (stored on the heap)\n");

            string name = "Alice";   // string is a reference type, but immutable
            string copy = name;      // both reference the same string in memory

            Console.WriteLine($"Before change: name = {name}, copy = {copy}");
            copy = "Bob";            // new string created (immutability!)
            Console.WriteLine($"After change:  name = {name}, copy = {copy}");

            // Example using a CLASS (custom reference type)
            Person p1 = new Person("Luke");
            Person p2 = p1; // p2 now references the same object as p1

            p2.Name = "Leia"; // modifies the shared heap object
            Console.WriteLine($"\nPerson p1.Name = {p1.Name}"); // "Leia"
            Console.WriteLine($"Person p2.Name = {p2.Name}");

            // =================================================================================================
            // TYPE INFERENCE - VAR
            // -------------------------------------------------------------------------------------------------
            // The 'var' keyword lets the compiler infer the type from the value.
            // The resulting variable is still *statically typed* (not dynamic).
            // -------------------------------------------------------------------------------------------------
            Console.WriteLine("\n- TYPE INFERENCE (var)\n");

            var count = 10;              // inferred as int
            var message = "Hello World"; // inferred as string
            var pi = 3.14;               // inferred as double

            Console.WriteLine($"count (type: {count.GetType().Name}) = {count}");
            Console.WriteLine($"message (type: {message.GetType().Name}) = {message}");
            Console.WriteLine($"pi (type: {pi.GetType().Name}) = {pi}");

            // =================================================================================================
            // BOXING AND UNBOXING
            // -------------------------------------------------------------------------------------------------
            // Value types can be "boxed" - wrapped inside an object - when assigned to a reference variable.
            // "Unboxing" extracts the value back out. There's a small performance cost.
            // -------------------------------------------------------------------------------------------------
            Console.WriteLine("\n- BOXING AND UNBOXING\n");

            int value = 100;           // value type
            object boxed = value;      // boxing: int -> object
            int unboxed = (int)boxed;  // unboxing: object -> int

            Console.WriteLine($"Original int: {value}");
            Console.WriteLine($"Boxed as object: {boxed}");
            Console.WriteLine($"Unboxed back to int: {unboxed}");

            // =================================================================================================
            // NULLABLE TYPES
            // -------------------------------------------------------------------------------------------------
            // Value types (like int, bool, double) normally cannot be null.
            // A nullable type (T?) allows you to represent "no value".
            // -------------------------------------------------------------------------------------------------
            Console.WriteLine("\n- NULLABLE TYPES (int?)\n");

            int? optionalValue = null; // nullable int
            Console.WriteLine(optionalValue.HasValue
                ? $"Has value: {optionalValue.Value}"
                : "No value assigned (null)");

            // Assign a value and check again
            optionalValue = 25;
            Console.WriteLine(optionalValue.HasValue
                ? $"Now has value: {optionalValue.Value}"
                : "Still null");

            // =================================================================================================
            // ENUMS AND CONSTANTS
            // -------------------------------------------------------------------------------------------------
            // Enums define a set of named numeric constants - ideal for representing categories or states.
            // Constants define fixed compile-time values that cannot change.
            // -------------------------------------------------------------------------------------------------
            Console.WriteLine("\n- ENUMS AND CONSTANTS\n");

            Direction dir = Direction.East;
            Console.WriteLine($"Current direction: {dir}");
            Console.WriteLine($"Underlying numeric value: {(int)dir}");

            const double Pi = 3.14159; // constant (compile-time fixed value)
            Console.WriteLine($"Constant PI = {Pi}");

            // =================================================================================================
            // STRINGS AND IMMUTABILITY
            // -------------------------------------------------------------------------------------------------
            // Strings are reference types but act like value types because they are *immutable*.
            // Changing a string actually creates a new object in memory.
            // -------------------------------------------------------------------------------------------------
            Console.WriteLine("\n- STRINGS AND IMMUTABILITY\n");

            string original = "Hello";
            string modified = original.Replace("H", "Y"); // creates a NEW string object

            Console.WriteLine($"original = {original}"); // "Hello" remains unchanged
            Console.WriteLine($"modified = {modified}"); // "Yello" is a new string

            // =================================================================================================
            // SUMMARY OF BEHAVIOR
            // -------------------------------------------------------------------------------------------------
            // - Value types live on the stack - copying creates *independent* values.
            // - Reference types live on the heap - copying copies the *reference*.
            // - Strings are reference types but immutable.
            // - 'var' infers type but remains strongly typed.
            // - Nullable types allow "no value" for value types.
            // - Enums and constants create readable, safe identifiers for fixed values.
            // -------------------------------------------------------------------------------------------------
            Console.WriteLine("\n=== END OF 02_DATATYPES MODULE ===");
        }
    }

    // -----------------------------------------------------------------------------------------------------
    // SUPPORTING TYPES FOR DEMONSTRATION
    // -----------------------------------------------------------------------------------------------------

    // Simple Person class for reference type demonstration.
    // Objects of this class are stored on the heap.
    internal class Person
    {
        public string Name { get; set; }
        public Person(string name) => Name = name;
    }

    // Enum representing basic directions.
    // Under the hood, each name corresponds to an integer (starting at 0).
    internal enum Direction
    {
        North, // 0
        East,  // 1
        South, // 2
        West   // 3
    }
}
