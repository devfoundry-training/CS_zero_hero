// =====================================================================================================
// TASK 02 - DATA TYPES: VALUE, REFERENCE & NULLABLES
// -----------------------------------------------------------------------------------------------------
// GOAL:
// Practice defining, using, and inspecting C# data types.
//
// This task reinforces concepts from 02_DATATYPES.cs:
// - Value vs reference types
// - Type inference (var)
// - Nullable types
// - Boxing and unboxing
// -----------------------------------------------------------------------------------------------------
//
// INSTRUCTIONS:
//
// 1. Inside your solution, create a new console project:
//
//        dotnet new console -n DataTypePlayground
//        cd DataTypePlayground
//
// 2. Replace the code in Program.cs with the starter below.
//    Fill in the blanks or modify as you go.
//
// -----------------------------------------------------------------------------------------------------
// STARTER CODE
// -----------------------------------------------------------------------------------------------------

using System;

Console.WriteLine("=== Data Type Playground ===");

// 1. VALUE TYPES
// -----------------------------------------------------------------------------------------------------
// Declare a few basic value types below.
// Example: int age = 25;

int age = /* your code */;
double price = /* your code */;
bool isStudent = /* your code */;

// 2. REFERENCE TYPES
// -----------------------------------------------------------------------------------------------------
// Reference types store their data on the heap and are accessed by reference.

string name = /* your code */;
int[] numbers = /* your code */;

// 3. NULLABLE TYPES
// -----------------------------------------------------------------------------------------------------
// Value types cannot normally be null. Use '?' to allow a "no value" state.

int? optionalValue = /* your code */;

// 4. TYPE INFERENCE (var)
// -----------------------------------------------------------------------------------------------------
// The compiler infers the type automatically at compile time.

var city = /* your code */;  // What type does the compiler infer here?

// 5. BOXING / UNBOXING
// -----------------------------------------------------------------------------------------------------
// Boxing wraps a value type into an object. Unboxing extracts it back.

int score = /* your code */;
object boxed = /* your code */;      // box the value
int unboxed = /* your code */;       // unbox it

// 6. PRINT YOUR VARIABLES
// -----------------------------------------------------------------------------------------------------
// Use Console.WriteLine to print your variables with string interpolation.
// Example: Console.WriteLine($"Name: {name}, Age: {age}");

/* your code here */

// -----------------------------------------------------------------------------------------------------
//
// 3. Run the program:
//
//        dotnet run
//
// Observe each line of output.
// Try assigning null to a non-nullable variable (it should fail to compile).
// Add one example of your own struct or enum below.
//
// Example:
// enum Grade { A, B, C, D, F }
// Grade myGrade = Grade.B;
//
// -----------------------------------------------------------------------------------------------------
//
// 4. Demonstrate reference vs value behavior:
// -----------------------------------------------------------------------------------------------------
//
// int x = 5;
// int y = x;
// y = 10;       // Does x change?
//
// string s1 = "Hello";
// string s2 = s1;
// s2 += " World"; // Does s1 change?
//
// -----------------------------------------------------------------------------------------------------
//
// 5. OPTIONAL:
// -----------------------------------------------------------------------------------------------------
// Use Environment.Version again to display your runtime version.
//
// Example:
// Console.WriteLine($"Running on .NET Version: {Environment.Version}");
//
// -----------------------------------------------------------------------------------------------------
//
// DISCUSSION POINTS:
// - What happens when you assign one variable to another?  
// - How does null behave on value vs reference types?  
// - What's the difference between var and dynamic?  
// -----------------------------------------------------------------------------------------------------
//
// OUTPUT EXAMPLE:
//
//   === Data Type Playground ===
//   Name: Alice, Age: 25, Student: True
//   Numbers: 1, 2, 3
//   Nullable value:
//   Unboxed score: 100
//
// -----------------------------------------------------------------------------------------------------
//
// CHALLENGE (Stretch Goal):
// - Create a struct named "Book" with Title, Author, and Pages.
// - Instantiate it and print its information using string interpolation.
//
// Example (for reference only):
// struct Book { public string Title; public string Author; public int Pages; }
// Book myBook = new Book { Title = "1984", Author = "George Orwell", Pages = 328 };
// Console.WriteLine($"Book: {myBook.Title} by {myBook.Author}, {myBook.Pages} pages");
//
// -----------------------------------------------------------------------------------------------------
//
// END OF TASK 02
// =====================================================================================================
