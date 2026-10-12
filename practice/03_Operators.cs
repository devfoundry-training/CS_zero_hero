// =====================================================================================================
// TASK 03 - OPERATORS: EXPRESSIONS & EVALUATION
// -----------------------------------------------------------------------------------------------------
// GOAL:
// Practice using arithmetic, assignment, comparison, logical, and ternary operators.
//
// This task reinforces the concepts from 03_OPERATORS.cs:
// - Arithmetic (+, -, *, /, %)
// - Assignment (+=, -=, etc.)
// - Comparison (==, !=, >, <, >=, <=)
// - Logical (&&, ||, !)
// - Increment / decrement (++ and --)
// - Operator precedence and parentheses
// - The ternary operator (?:)
// -----------------------------------------------------------------------------------------------------
//
// INSTRUCTIONS:
//
// 1. Inside your solution folder, create a new console project:
//
//        dotnet new console -n OperatorPlayground
//        cd OperatorPlayground
//
// 2. Replace the code in Program.cs with the template below.
//    Fill in each missing line where you see comments such as "// your code here".
// -----------------------------------------------------------------------------------------------------

using System;

Console.WriteLine("=== Operator Playground ===\n");

// -----------------------------------------------------------------------------------------------------
// 1. ARITHMETIC OPERATORS
// -----------------------------------------------------------------------------------------------------
// Perform calculations using +, -, *, /, and %.
// The goal: understand integer vs floating-point division.

int a = 10;
int b = 3;

Console.WriteLine("-- Arithmetic Operators --");
Console.WriteLine($"{a} + {b} = { /* your code here */ }");
Console.WriteLine($"{a} - {b} = { /* your code here */ }");
Console.WriteLine($"{a} * {b} = { /* your code here */ }");
Console.WriteLine($"{a} / {b} = { /* your code here */ }");  // integer division (fraction removed)
Console.WriteLine($"{a} % {b} = { /* your code here */ }");  // remainder (modulus)

// Convert to double for precise division:
double accurate = /* cast a or b to double and divide */;
Console.WriteLine($"Accurate division: {accurate}\n");

// -----------------------------------------------------------------------------------------------------
// 2. ASSIGNMENT OPERATORS
// -----------------------------------------------------------------------------------------------------
// Use shorthand operators to modify the same variable.

int x = 5;
Console.WriteLine("-- Assignment Operators --");
x += /* your code here */;
x -= /* your code here */;
x *= /* your code here */;
x /= /* your code here */;
x %= /* your code here */;
Console.WriteLine($"Final value of x: {x}\n");

// -----------------------------------------------------------------------------------------------------
// 3. COMPARISON OPERATORS
// -----------------------------------------------------------------------------------------------------
// Compare values and print whether conditions are true or false.

int num1 = 10;
int num2 = 20;

Console.WriteLine("-- Comparison Operators --");
Console.WriteLine($"{num1} == {num2} -> { /* your code here */ }");
Console.WriteLine($"{num1} != {num2} -> { /* your code here */ }");
Console.WriteLine($"{num1} > {num2}  -> { /* your code here */ }");
Console.WriteLine($"{num1} < {num2}  -> { /* your code here */ }");
Console.WriteLine($"{num1} >= {num2} -> { /* your code here */ }");
Console.WriteLine($"{num1} <= {num2} -> { /* your code here */ }\n");

// -----------------------------------------------------------------------------------------------------
// 4. LOGICAL OPERATORS
// -----------------------------------------------------------------------------------------------------
// Combine multiple Boolean values using logical AND (&&), OR (||), and NOT (!).

bool isSunny = true;
bool isWarm = false;

Console.WriteLine("-- Logical Operators --");
Console.WriteLine($"isSunny && isWarm -> { /* your code here */ }");  // both must be true
Console.WriteLine($"isSunny || isWarm -> { /* your code here */ }");  // at least one must be true
Console.WriteLine($"!isSunny -> { /* your code here */ }");            // negation
Console.WriteLine($"!(isSunny && isWarm) -> { /* your code here */ }\n");

// -----------------------------------------------------------------------------------------------------
// 5. INCREMENT / DECREMENT
// -----------------------------------------------------------------------------------------------------
// Test prefix and postfix behavior.

int counter = 5;
Console.WriteLine("-- Increment/Decrement --");
Console.WriteLine($"Start: {counter}");
Console.WriteLine($"Postfix increment (counter++) = { /* your code here */ }");
Console.WriteLine($"After postfix: {counter}");
Console.WriteLine($"Prefix increment (++counter) = { /* your code here */ }");
Console.WriteLine($"After prefix: {counter}\n");

// -----------------------------------------------------------------------------------------------------
// 6. OPERATOR PRECEDENCE
// -----------------------------------------------------------------------------------------------------
// Predict then check the results. Use parentheses to control order.

int result1 = 5 + 2 * 3;      // multiplication first
int result2 = (5 + 2) * 3;    // parentheses change the order
Console.WriteLine("-- Operator Precedence --");
Console.WriteLine($"5 + 2 * 3 = {result1}");
Console.WriteLine($"(5 + 2) * 3 = {result2}\n");

// -----------------------------------------------------------------------------------------------------
// 7. TERNARY OPERATOR
// -----------------------------------------------------------------------------------------------------
// Assign one of two values depending on a condition.
// Syntax: condition ? value_if_true : value_if_false

int age = /* your code here */;
string status = /* your code here */; // example: (age >= 18) ? "Adult" : "Minor"
Console.WriteLine($"Age: {age}, Status: {status}\n");

// -----------------------------------------------------------------------------------------------------
// 8. COMBINED EXERCISE
// -----------------------------------------------------------------------------------------------------
// Combine logical and comparison operators to decide what message to print.

int temperature = /* your code here */;
bool sunny = /* your code here */;

if ( /* your combined condition */ )
{
    Console.WriteLine("It's a great day!");
}
else
{
    Console.WriteLine("Maybe stay indoors.");
}

// -----------------------------------------------------------------------------------------------------
// DISCUSSION POINTS (for review):
// - What happens if you remove parentheses in a long expression?  
// - When would you use ++x vs x++?  
// - When is a ternary operator useful?  
// -----------------------------------------------------------------------------------------------------
//
// OUTPUT EXAMPLE:
//
//   === Operator Playground ===
//   10 + 3 = 13
//   10 - 3 = 7
//   10 * 3 = 30
//   10 / 3 = 3
//   10 % 3 = 1
//   Accurate division: 3.3333333333333335
//   Final value of x: ...
//   Age: 21, Status: Adult
//
// -----------------------------------------------------------------------------------------------------
// CHALLENGE (Stretch Goal):
// -----------------------------------------------------------------------------------------------------
// - Ask the user to enter two numbers using Console.ReadLine().
// - Parse them to integers, then display results for +, -, *, /, and %.
// - Use string interpolation to print results neatly.
//
// Example Output:
// Enter first number: 12
// Enter second number: 5
// 12 + 5 = 17
// 12 - 5 = 7
// 12 * 5 = 60
// 12 / 5 = 2
// 12 % 5 = 2
// -----------------------------------------------------------------------------------------------------
//
// END OF TASK 03
// =====================================================================================================
