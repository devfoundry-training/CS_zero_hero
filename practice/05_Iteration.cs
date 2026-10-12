// =====================================================================================================
// TASK 05 - ITERATION PRACTICE
// -----------------------------------------------------------------------------------------------------
// GOAL:
// Practice repeating actions using C# loops (for, while, do-while, foreach).
//
// This task reinforces iteration concepts from 05_ITERATION.cs:
// - Using counting loops (for)
// - Using condition loops (while, do-while)
// - Iterating through collections (foreach)
// - Applying break, continue, and return
// -----------------------------------------------------------------------------------------------------
//
// INSTRUCTIONS:
//
// 1.  Create a new console project for this task:
//
//        dotnet new console -n IterationPlayground
//        cd IterationPlayground
//
// 2.  Replace the code in Program.cs with this starter file.
//     Fill in all the TODO sections below (marked with underscores or comments).
// -----------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

Console.WriteLine("=== ITERATION PRACTICE ===\n");

// =====================================================================================================
// 1. BASIC FOR LOOP
// -----------------------------------------------------------------------------------------------------
// The for loop repeats a known number of times.
// Complete the loop so it prints numbers 1 through 10.
// -----------------------------------------------------------------------------------------------------

Console.WriteLine("-- Counting with a For Loop --");

// TODO: Write a for loop that prints numbers 1 through 10.
for (int i = /* start */; i <= /* end */; i++)
{
    Console.WriteLine($"Number: { /* variable */ }");
}

// Challenge: Modify your loop to print only even numbers using an if-statement.

// -----------------------------------------------------------------------------------------------------


// =====================================================================================================
// 2. WHILE LOOP - CONDITION BASED
// -----------------------------------------------------------------------------------------------------
// Loops while a condition remains true.
// Create a countdown that starts from 5 and prints each number down to 1, then prints "Lift off!"
// -----------------------------------------------------------------------------------------------------

Console.WriteLine("\n-- While Loop Example --");

// TODO: Create a while loop that counts down from 5 to 1.
int counter = /* starting value */;

while (/* condition */)
{
    Console.WriteLine($"Countdown: { /* variable */ }");
    /* update variable */
}

Console.WriteLine("Lift off!");

// -----------------------------------------------------------------------------------------------------


// =====================================================================================================
// 3. DO-WHILE LOOP - RUNS AT LEAST ONCE
// -----------------------------------------------------------------------------------------------------
// The do-while loop always runs once before checking its condition.
// Ask the user to enter a number greater than 10. Keep asking until they do.
// -----------------------------------------------------------------------------------------------------

Console.WriteLine("\n-- Do-While Example --");

int number;

do
{
    Console.Write("Enter a number greater than 10: ");
    string? input = Console.ReadLine();
    int.TryParse(input, out number);
}
while (/* condition */);

Console.WriteLine($"You entered: { /* variable */ }");

// -----------------------------------------------------------------------------------------------------


// =====================================================================================================
// 4. FOREACH LOOP - COLLECTION ITERATION
// -----------------------------------------------------------------------------------------------------
// foreach is used to iterate through all items in a collection (array, list, etc.).
// Create an array of 5 names and print a greeting for each one.
// -----------------------------------------------------------------------------------------------------

Console.WriteLine("\n-- Foreach Loop Example --");

// TODO: Create an array and iterate through it.
string[] names = { /* fill in 5 names */ };

foreach (string name in /* collection */)
{
    Console.WriteLine($"Hello, { /* variable */ }!");
}

// Challenge: Count how many names start with 'L'.

int countL = 0;
foreach (/* type */ n in /* collection */)
{
    if (/* condition: starts with 'L' */)
        countL++;
}

Console.WriteLine($"Names starting with 'L': { /* variable */ }");

// -----------------------------------------------------------------------------------------------------


// =====================================================================================================
// 5. NESTED LOOPS - MULTIPLE LEVELS OF ITERATION
// -----------------------------------------------------------------------------------------------------
// A loop inside another loop is used for grids, coordinates, or 2D data.
// Print a simple 3x3 grid of coordinates (row, column).
// -----------------------------------------------------------------------------------------------------

Console.WriteLine("\n-- Nested Loops Example --");

// TODO: Print a 3x3 grid of coordinates.
for (int row = /* start */; row <= /* end */; row++)
{
    for (int col = /* start */; col <= /* end */; col++)
    {
        Console.Write($"(/* row */ , /* col */) ");
    }
    Console.WriteLine();
}

// Challenge: Use nested loops to print a 1-5 multiplication table.

Console.WriteLine("\nMultiplication Table (1-5):");

for (int i = /* start */; i <= /* end */; i++)
{
    for (int j = /* start */; j <= /* end */; j++)
    {
        Console.Write($"{ /* expression */ ,4}");
    }
    Console.WriteLine();
}

// -----------------------------------------------------------------------------------------------------


// =====================================================================================================
// 6. BREAK / CONTINUE / RETURN
// -----------------------------------------------------------------------------------------------------
// Control how loops end or skip iterations.
// Write a loop that counts from 1-10, skips 3 using 'continue', and stops completely at 8 using 'break'.
// -----------------------------------------------------------------------------------------------------

Console.WriteLine("\n-- Break & Continue Example --");

for (int i = /* start */; i <= /* end */; i++)
{
    if (/* condition to skip */)
    {
        Console.WriteLine("Skipping number 3...");
        continue;
    }

    if (/* condition to stop */)
    {
        Console.WriteLine("Stopping at 8...");
        break;
    }

    Console.WriteLine($"i = { /* variable */ }");
}

// -----------------------------------------------------------------------------------------------------


// =====================================================================================================
// 7. ADVANCED CHALLENGE - COMBINED EXAMPLE
// -----------------------------------------------------------------------------------------------------
// Combine loops and logic for a small working program.
// Ask the user how many numbers they want to enter.
// Use a for loop to collect them and calculate the total and average.
// -----------------------------------------------------------------------------------------------------

Console.WriteLine("\n-- Combined Example: Sum Calculator --");

Console.Write("How many numbers do you want to add? ");
int totalCount = int.Parse(Console.ReadLine() ?? "0");

int total = 0;

for (int i = /* start */; i <= /* end */; i++)
{
    Console.Write($"Enter number { /* variable */ }: ");
    int.TryParse(Console.ReadLine(), out int value);
    total += /* variable */;
}

Console.WriteLine($"Total sum: { /* expression */ }");
Console.WriteLine($"Average: { /* expression */ }");

// -----------------------------------------------------------------------------------------------------


// =====================================================================================================
// SUMMARY
// -----------------------------------------------------------------------------------------------------
// - for - fixed number of repetitions
// - while - condition-based repetition
// - do-while - runs at least once
// - foreach - iterate through arrays/lists
// - nested loops - useful for patterns/tables
// - break / continue - control loop execution
// -----------------------------------------------------------------------------------------------------

Console.WriteLine("\n=== END OF ITERATION PRACTICE ===");
