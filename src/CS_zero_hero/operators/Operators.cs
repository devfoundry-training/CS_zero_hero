// =====================================================================================================
// 03_OPERATORS.cs
// -----------------------------------------------------------------------------------------------------
// PURPOSE:
// Introduce the operators that make C# expressions work.
//
// Operators are symbols that perform actions on variables and values.
// They are the "verbs" of your code - things that combine, compare, and modify data.
//
// -----------------------------------------------------------------------------------------------------
//
// LESSON OBJECTIVES:
// - Understand arithmetic operators (+, -, *, /, %)
// - Learn assignment and compound operators (=, +=, -=, etc.)
// - Understand comparison operators (==, !=, >, <, >=, <=)
// - Learn logical operators (&&, ||, !)
// - Explore increment/decrement (++ and --)
// - Learn operator precedence and parentheses
// - Preview the ternary operator (?:)
// -----------------------------------------------------------------------------------------------------

using System;

namespace CS_Zero_Hero.operators
{
    internal class Operators
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== OPERATORS IN C# ===\n");

            // =================================================================================================
            // 1. ARITHMETIC OPERATORS
            // -------------------------------------------------------------------------------------------------
            // These perform mathematical calculations on numeric types (int, double, float, decimal).
            // -------------------------------------------------------------------------------------------------

            int a = 10;
            int b = 3;

            Console.WriteLine("-- Arithmetic Operators --");
            Console.WriteLine($"{a} + {b} = {a + b}");  // addition
            Console.WriteLine($"{a} - {b} = {a - b}");  // subtraction
            Console.WriteLine($"{a} * {b} = {a * b}");  // multiplication
            Console.WriteLine($"{a} / {b} = {a / b}");  // integer division (fraction discarded)
            Console.WriteLine($"{a} % {b} = {a % b}");  // modulus (remainder)

            // When dividing integers, you lose decimals. To get accurate division, use double:
            double result = (double)a / b;
            Console.WriteLine($"Accurate division (double): {result}\n");

            // =================================================================================================
            // 2. ASSIGNMENT OPERATORS
            // -------------------------------------------------------------------------------------------------
            // Used to assign or update values. Often combined with arithmetic for shorthand updates.
            // -------------------------------------------------------------------------------------------------

            int x = 5;
            Console.WriteLine("-- Assignment Operators --");

            x += 2; // same as x = x + 2
            Console.WriteLine($"x += 2 -> {x}");

            x -= 3; // same as x = x - 3
            Console.WriteLine($"x -= 3 -> {x}");

            x *= 4; // same as x = x * 4
            Console.WriteLine($"x *= 4 -> {x}");

            x /= 2; // same as x = x / 2
            Console.WriteLine($"x /= 2 -> {x}");

            x %= 3; // same as x = x % 3
            Console.WriteLine($"x %= 3 -> {x}\n");

            // =================================================================================================
            // 3. COMPARISON OPERATORS
            // -------------------------------------------------------------------------------------------------
            // Return a Boolean (true/false). Used in conditions (if, loops, etc.).
            // -------------------------------------------------------------------------------------------------

            int num1 = 10;
            int num2 = 20;

            Console.WriteLine("-- Comparison Operators --");
            Console.WriteLine($"{num1} == {num2} -> {num1 == num2}"); // equal
            Console.WriteLine($"{num1} != {num2} -> {num1 != num2}"); // not equal
            Console.WriteLine($"{num1} > {num2}  -> {num1 > num2}");  // greater than
            Console.WriteLine($"{num1} < {num2}  -> {num1 < num2}");  // less than
            Console.WriteLine($"{num1} >= {num2} -> {num1 >= num2}"); // greater or equal
            Console.WriteLine($"{num1} <= {num2} -> {num1 <= num2}\n"); // less or equal

            // These are most often used inside if-statements, loops, and switch expressions.

            // =================================================================================================
            // 4. LOGICAL OPERATORS
            // -------------------------------------------------------------------------------------------------
            // Combine multiple Boolean expressions.
            // -------------------------------------------------------------------------------------------------

            bool isSunny = true;
            bool isWarm = false;

            Console.WriteLine("-- Logical Operators --");
            Console.WriteLine($"isSunny && isWarm -> {isSunny && isWarm}"); // AND: both must be true
            Console.WriteLine($"isSunny || isWarm -> {isSunny || isWarm}"); // OR: at least one true
            Console.WriteLine($"!isSunny -> {!isSunny}");                   // NOT: reverses the value
            Console.WriteLine($"!(isSunny && isWarm) -> {!(isSunny && isWarm)}\n");

            // Logical operators are crucial for complex conditions like:
            // if (age >= 18 && hasTicket) { ... }

            // =================================================================================================
            // 5. INCREMENT / DECREMENT
            // -------------------------------------------------------------------------------------------------
            // ++ increases by 1, -- decreases by 1.
            // There are two forms: prefix (++x) and postfix (x++).
            // -------------------------------------------------------------------------------------------------

            int counter = 5;

            Console.WriteLine("-- Increment/Decrement --");
            Console.WriteLine($"counter = {counter}");

            Console.WriteLine($"Postfix increment (counter++) = {counter++}"); // prints 5, then increments
            Console.WriteLine($"After postfix: {counter}");

            Console.WriteLine($"Prefix increment (++counter) = {++counter}");  // increments first, prints 7

            Console.WriteLine($"Postfix decrement (counter--) = {counter--}");
            Console.WriteLine($"After decrement: {counter}\n");

            // =================================================================================================
            // 6. OPERATOR PRECEDENCE
            // -------------------------------------------------------------------------------------------------
            // The order in which operators are evaluated.
            // Multiplication and division happen before addition and subtraction (like math).
            // Use parentheses to clarify order.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("-- Operator Precedence --");
            int result1 = 5 + 2 * 3;       // 2*3 runs first -> 5 + 6 = 11
            int result2 = (5 + 2) * 3;     // parentheses change order -> 7*3 = 21
            Console.WriteLine($"5 + 2 * 3 = {result1}");
            Console.WriteLine($"(5 + 2) * 3 = {result2}\n");

            // In real code, ALWAYS use parentheses for readability - it prevents subtle bugs.

            // =================================================================================================
            // 7. THE TERNARY OPERATOR (?:)
            // -------------------------------------------------------------------------------------------------
            // A compact form of if/else for expressions.
            // Syntax: condition ? value_if_true : value_if_false
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("-- Ternary Operator --");

            int age = 20;
            string eligibility = (age >= 18) ? "Adult" : "Minor";
            Console.WriteLine($"Age: {age}, Status: {eligibility}\n");

            // Nested ternaries are possible but can hurt readability. Use sparingly.

            // =================================================================================================
            // 8. COMBINING OPERATORS - MINI EXAMPLE
            // -------------------------------------------------------------------------------------------------
            // Let's combine a few operator types to simulate a simple logical check.
            // -------------------------------------------------------------------------------------------------

            int temperature = 25;
            bool sunny = true;

            // Complex Boolean condition combining comparison + logical operators
            if (temperature > 20 && sunny)
            {
                Console.WriteLine("It's a nice day for a walk!");
            }
            else
            {
                Console.WriteLine("Maybe stay indoors today.");
            }

            // =================================================================================================
            // SUMMARY
            // -------------------------------------------------------------------------------------------------
            // - Arithmetic -> + - * / %
            // - Assignment -> = += -= *= /= %=
            // - Comparison -> == != > < >= <=
            // - Logical -> && || !
            // - Increment/Decrement -> ++ --
            // - Precedence -> use parentheses to control order
            // - Ternary -> short conditional expressions
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n=== END OF 03_OPERATORS MODULE ===");
        }
    }
}
