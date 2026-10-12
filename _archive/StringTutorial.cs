using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_zero_hero
{
    using System;
    using System.Text; // Required for StringBuilder

    namespace CS_zero_hero
    {
        public class StringExamples
        {

            //In C#, strings are immutable, which means that once a string is created, its content cannot be changed.
            //Any operation that appears to modify a string - such as ToUpper(), Replace(), or concatenation
            //(+) - actually creates a new string object in memory. The original string remains unchanged.
            //This design improves security and stability, especially in multi-threaded environments,
            //but it can lead to performance issues when repeatedly modifying strings (e.g., inside loops).
            //In such cases, use StringBuilder, which is designed for efficient, mutable string manipulation.

            //In C#, string interning is a mechanism where identical string literals are stored only once in memory -
            //this is managed by the CLR (Common Language Runtime). So, when you declare two identical string literals:





            // ----------------------------------------
            // 1. string vs String
            // ----------------------------------------
            public static void ExplainStringVsString()
            {
                // 'string' is a C# keyword and is preferred for variable declarations.
                string keywordExample = "This uses the C# keyword";

                // 'String' refers to System.String, the .NET class that defines string behavior.
                String classNameExample = "This uses the System.String class";

                // Both are exactly the same type under the hood.
                Console.WriteLine(keywordExample);
                Console.WriteLine(classNameExample);

                // Confirm they are the same type
                bool areEqual = keywordExample.GetType() == classNameExample.GetType();
                Console.WriteLine($"Same type? {areEqual}"); // Output: True
            }

            // ----------------------------------------
            // 2. Basic string creation and interpolation
            // ----------------------------------------
            public static void ShowStringBasics()
            {
                string name = "Alice";
                string greeting = $"Hello, {name}!"; // Interpolation using $
                Console.WriteLine(greeting);
            }

            // ----------------------------------------
            // 3. Concatenation (joining strings)
            // ----------------------------------------
            public static void ShowConcatenation()
            {
                string first = "C#";
                string second = " is fun!";
                string result = first + second; // Using + to join strings
                Console.WriteLine(result);
            }

            // ----------------------------------------
            // 4. Useful string methods
            // ----------------------------------------
            public static void ShowStringMethods()
            {
                string sample = "Hello World";

                // Common string operations
                Console.WriteLine($"Original: {sample}");
                Console.WriteLine($"ToUpper: {sample.ToUpper()}");          // Convert to uppercase
                Console.WriteLine($"Contains 'World': {sample.Contains("World")}"); // Check substring
                Console.WriteLine($"StartsWith 'He': {sample.StartsWith("He")}");   // Check prefix
                Console.WriteLine($"Substring (6): {sample.Substring(6)}");         // Slice string
                Console.WriteLine($"Length: {sample.Length}");             // Character count
            }

            // ----------------------------------------
            // 5. String immutability
            // ----------------------------------------
            public static void ShowImmutability()
            {
                string original = "hello";
                string modified = original.ToUpper(); // Creates a new string

                Console.WriteLine($"Original: {original}");  // Remains 'hello'
                Console.WriteLine($"Modified: {modified}");  // 'HELLO'

                // Explanation:
                // Strings in C# are immutable, meaning any modification
                // creates a new string in memory.
            }

            // ----------------------------------------
            // 6. Escape sequences and verbatim strings
            // ----------------------------------------
            public static void ShowEscapeAndVerbatim()
            {
                string escapedPath = "C:\\Users\\Alice\\Documents"; // Double backslashes
                string verbatimPath = @"C:\Users\Alice\Documents";  // Verbatim with @

                Console.WriteLine($"Escaped path: {escapedPath}");
                Console.WriteLine($"Verbatim path: {verbatimPath}");
            }

            // ----------------------------------------
            // 7. StringBuilder basics
            // ----------------------------------------
            public static void ShowStringBuilder()
            {
                // StringBuilder is mutable and more efficient for repeated modifications.
                StringBuilder sb = new StringBuilder();

                // Appending multiple pieces of text
                sb.Append("Hello");
                sb.Append(" ");
                sb.Append("World");
                sb.Append("!");

                // Output the built string
                Console.WriteLine($"Built string: {sb.ToString()}");

                // Modify the string in place
                sb.Replace("World", "C# Learner");  // Replace part of the text
                Console.WriteLine($"After Replace: {sb}");

                sb.Insert(0, ">> ");                // Insert at beginning
                Console.WriteLine($"After Insert: {sb}");

                sb.Remove(0, 3);                    // Remove first 3 characters
                Console.WriteLine($"After Remove: {sb}");
            }

            // ----------------------------------------
            // 8. Why use StringBuilder?
            // ----------------------------------------
            public static void ShowStringBuilderAdvantage()
            {
                // Less efficient way - creates many temporary strings
                string result = "";
                for (int i = 0; i < 5; i++)
                {
                    result += i + " "; // New string created each time
                }
                Console.WriteLine($"Normal string loop: {result}");

                // More efficient - modifies one object in memory
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 5; i++)
                {
                    sb.Append(i).Append(" ");
                }
                Console.WriteLine($"StringBuilder loop: {sb.ToString()}");

                // Summary:
                // Use StringBuilder for performance when concatenating
                // many strings (especially in loops or large data handling).
            }

            // ----------------------------------------
            // 9. String interning (string pool concept)
            // ----------------------------------------
            public static void ExplainStringInterning()
            {
                // String literals are automatically interned - they share memory
                string literal1 = "hello";
                string literal2 = "hello";
                Console.WriteLine($"Literal1 == Literal2: {object.ReferenceEquals(literal1, literal2)}"); // True

                // Create a new string at runtime - not interned by default
                string runtimeString = new string("hello".ToCharArray());
                Console.WriteLine($"Literal1 == RuntimeString: {object.ReferenceEquals(literal1, runtimeString)}"); // False

                // Force interning of the runtime string
                string interned = string.Intern(runtimeString);
                Console.WriteLine($"Literal1 == Interned: {object.ReferenceEquals(literal1, interned)}"); // True

                // Explanation:
                Console.WriteLine("\nExplanation:");
                Console.WriteLine("- String literals like \"hello\" are stored once in the intern pool.");
                Console.WriteLine("- Strings created at runtime (like using new string(...)) are not interned.");
                Console.WriteLine("- Using string.Intern(), you can manually add a string to the intern pool.");
            }

        }
    }
}