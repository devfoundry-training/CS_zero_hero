// =====================================================================================================
// TASK 04 - CONTROL FLOW: DECISIONS & LOOPS
// -----------------------------------------------------------------------------------------------------
// GOAL:
// Practice controlling the flow of a C# program using conditionals, loops, and branching statements.
//
// This task reinforces concepts from 04_CONTROL_FLOW.cs:
// - Conditional statements (if / else / switch)
// - Loops (for, while, do-while, foreach)
// - Flow modifiers (break, continue, return)
// -----------------------------------------------------------------------------------------------------
//
// INSTRUCTIONS:
//
// 1. Create a new console project inside your solution:
//
//        dotnet new console -n ControlFlowPractice
//        cd ControlFlowPractice
//
// 2. Replace the contents of Program.cs with the code below.
//
// 3. Read the comments carefully - fill in the missing logic where indicated.
//
// -----------------------------------------------------------------------------------------------------
// STARTER CODE
// -----------------------------------------------------------------------------------------------------

using System;

Console.WriteLine("=== CONTROL FLOW PRACTICE ===\n");

// =====================================================================================================
// 1. CONDITIONALS - IF / ELSE
// -----------------------------------------------------------------------------------------------------
// Ask the user for their score and print a grade.
// Include input validation to handle non-numeric values.
// -----------------------------------------------------------------------------------------------------

Console.Write("Enter your score (0-100): ");
string? input = Console.ReadLine();

// TODO: Safely convert the input to an integer named 'score'.
// Hint: use int.TryParse()

// TODO: Write an if/else if/else chain to print:
//       - "Grade: A" for 90 and above
//       - "Grade: B" for 75-89
//       - "Grade: C" for 60-74
//       - "Grade: F" otherwise


// =====================================================================================================
// 2. SWITCH STATEMENT - DAY MESSAGE
// -----------------------------------------------------------------------------------------------------
// Ask the user for a day of the week, then print an activity suggestion.
// -----------------------------------------------------------------------------------------------------

Console.Write("\nEnter a day of the week: ");
string? day = Console.ReadLine();

// TODO: Use a switch statement to print one of the following messages:
// Monday  -> "Back to work!"
// Friday  -> "Almost the weekend!"
// Saturday/Sunday -> "Time to relax!"
// Default -> "Midweek routine."


// =====================================================================================================
// 3. FOR LOOP - SUMMATION
// -----------------------------------------------------------------------------------------------------
// Write a for loop that adds numbers 1-10 together.
// Then print the total.
// -----------------------------------------------------------------------------------------------------

int sum = 0;

// TODO: Loop from 1 to 10 (inclusive) and add each number to 'sum'.



Console.WriteLine($"\nThe total sum from 1-10 is: {sum}");


// =====================================================================================================
// 4. WHILE LOOP - COUNTDOWN
// -----------------------------------------------------------------------------------------------------
// Create a countdown starting from a number entered by the user.
// Use a while loop that counts down to 0 and prints each step.
// -----------------------------------------------------------------------------------------------------

Console.Write("\nEnter a countdown start number: ");
string? countInput = Console.ReadLine();
int.TryParse(countInput, out int countdown);

// TODO: Write a while loop that decrements 'countdown' until it reaches 0.
// Print "Blastoff!" when finished.


// =====================================================================================================
// 5. DO-WHILE LOOP - USER PROMPT
// -----------------------------------------------------------------------------------------------------
// Ask the user to type "yes" to continue the program. Keep looping until they do.
// -----------------------------------------------------------------------------------------------------

string? response;

// TODO: Write a do-while loop that keeps asking the user until they type "yes".


Console.WriteLine("Continuing...\n");


// =====================================================================================================
// 6. FOREACH - ITERATING COLLECTIONS
// -----------------------------------------------------------------------------------------------------
// Create an array of names and greet each one using foreach.
// -----------------------------------------------------------------------------------------------------

string[] names = { "Luke", "Leia", "Han", "Rey" };

// TODO: Use a foreach loop to print a greeting for each name.


// =====================================================================================================
// 7. BREAK / CONTINUE / RETURN - ADVANCED FLOW
// -----------------------------------------------------------------------------------------------------
// Use a for loop from 0-15.
// Skip printing number 3 (continue)
// Stop completely at 10 (break)
// When the loop reaches 5, call a helper method that demonstrates break vs return in nested loops.
// -----------------------------------------------------------------------------------------------------

for (int i = 0; i <= 15; i++)
{
    // TODO: Skip 3, break at 10, and call NestedLoopExample() at 5.

}

Console.WriteLine("Main loop complete.");


// -----------------------------------------------------------------------------------------------------
// SUPPORTING METHOD: Demonstrates 'break' vs 'return' inside nested loops
// -----------------------------------------------------------------------------------------------------
static void NestedLoopExample()
{
    Console.WriteLine("\nEntering NestedLoopExample...\n");

    for (int outer = 1; outer <= 3; outer++)
    {
        for (int inner = 1; inner <= 3; inner++)
        {
            if (inner == 2 && outer == 2)
            {
                Console.WriteLine("Using 'break' - exits only the INNER loop.");
                break; // exits only the inner loop
            }

            if (inner == 3 && outer == 3)
            {
                Console.WriteLine("Using 'return' - exits the ENTIRE METHOD immediately.");
                return; // leaves the entire method
            }

            Console.WriteLine($"Outer: {outer}, Inner: {inner}");
        }

        Console.WriteLine($"End of outer loop iteration {outer}\n");
    }

    Console.WriteLine("End of NestedLoopExample (if return wasn't triggered).");
}

// =====================================================================================================
// DISCUSSION POINTS (for debrief)
// -----------------------------------------------------------------------------------------------------
// - What's the difference between break, continue, and return?  
// - What happens when you put one loop inside another?  
// - What happens if the user enters text instead of a number?  
// - How do switch expressions improve readability?  
// -----------------------------------------------------------------------------------------------------
//
// CHALLENGE (Stretch Goal):
// - Write a simple "menu" that repeats until the user selects Exit.
//   Example options: [1] Greet, [2] Show Date, [3] Exit
// - Use a do-while loop and switch statement to implement it.
// -----------------------------------------------------------------------------------------------------
//
// END OF TASK 04
// =====================================================================================================
