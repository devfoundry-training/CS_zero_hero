// =====================================================================================================
// 04_CONTROL_FLOW.cs
// -----------------------------------------------------------------------------------------------------
// PURPOSE:
// Teach how to control the execution of a C# program using decision-making and loops.
//
// This module shows how conditions, comparisons, and repetition combine to shape program behavior.
// -----------------------------------------------------------------------------------------------------
//
// LESSON OBJECTIVES:
// - Understand conditional logic (if, else if, else)
// - Learn switch statements and pattern matching
// - Use loops (for, while, do-while, foreach)
// - Explore break, continue, and return
// - Combine logic and repetition for basic algorithms
// -----------------------------------------------------------------------------------------------------

using System;

namespace CS_Zero_Hero.control_flow
{
    internal class ControlFlow
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== CONTROL FLOW ===\n");

            // =================================================================================================
            // 1. CONDITIONALS - IF / ELSE
            // -------------------------------------------------------------------------------------------------
            // Control flow begins with decision-making.
            // C# evaluates a Boolean condition (true/false) to decide which block to execute.
            // -------------------------------------------------------------------------------------------------

            int score = 85;

            if (score >= 90)
            {
                Console.WriteLine("Grade: A");
            }
            else if (score >= 75)
            {
                Console.WriteLine("Grade: B");
            }
            else if (score >= 60)
            {
                Console.WriteLine("Grade: C");
            }
            else
            {
                Console.WriteLine("Grade: F");
            }

            // Combine conditions with logical operators (&&, ||)
            bool isMember = true;
            bool hasCoupon = false;

            if (isMember || hasCoupon)
            {
                Console.WriteLine("Discount applied!");
            }
            else
            {
                Console.WriteLine("No discount available.");
            }

            // Negation (!) inverts a Boolean value
            if (!(score < 50))
            {
                Console.WriteLine("You passed the exam!");
            }

            // =================================================================================================
            // 2. SWITCH STATEMENTS
            // -------------------------------------------------------------------------------------------------
            // A switch is an elegant alternative to many if/else statements.
            // -------------------------------------------------------------------------------------------------
            Console.WriteLine("\n-- SWITCH STATEMENT --");

            string day = "Saturday";

            switch (day)
            {
                case "Monday":
                    Console.WriteLine("Start of the work week!");
                    break;

                case "Friday":
                    Console.WriteLine("Almost the weekend!");
                    break;

                case "Saturday":
                case "Sunday":
                    Console.WriteLine("It's the weekend! Relax!");
                    break;

                default:
                    Console.WriteLine("Midweek grind.");
                    break;
            }

            // Modern switch expression (C# 8+)
            string mood = day switch
            {
                "Monday" => "Focused",
                "Friday" => "Excited",
                "Saturday" or "Sunday" => "Chill",
                _ => "Neutral"
            };
            Console.WriteLine($"Mood for {day}: {mood}");

            // =================================================================================================
            // 3. LOOPS - REPETITION
            // -------------------------------------------------------------------------------------------------
            // Loops allow code to repeat until a condition changes.
            // Common types: for, while, do-while, foreach
            // -------------------------------------------------------------------------------------------------
            Console.WriteLine("\n-- LOOPS --");

            // FOR LOOP - runs a set number of times
            Console.WriteLine("For loop: counting 0-4");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"i = {i}");
            }

            // WHILE LOOP - continues while condition is true
            Console.WriteLine("\nWhile loop: countdown");
            int countdown = 3;
            while (countdown > 0)
            {
                Console.WriteLine($"T-minus {countdown}");
                countdown--;
            }

            // DO-WHILE LOOP - runs at least once, even if condition is false
            Console.WriteLine("\nDo-While loop example:");
            int number;
            do
            {
                Console.Write("Enter a number greater than 10: ");
                string? input = Console.ReadLine();
                int.TryParse(input, out number);
            }
            while (number <= 10);
            Console.WriteLine($"You entered: {number}");

            // FOREACH LOOP - iterate through a collection (read-only)
            Console.WriteLine("\nForeach loop example:");
            string[] names = { "Luke", "Leia", "Han" };
            foreach (string name in names)
            {
                Console.WriteLine($"Hello, {name}!");
            }

            // Note: foreach creates a read-only copy of each element; you cannot modify the array directly.

            // =================================================================================================
            // 4. LOOP SCOPE AND VARIABLES
            // -------------------------------------------------------------------------------------------------
            // Variables declared inside a loop only exist within that loop's block.
            // The variable 'i' in the for-loop above is not accessible outside it.
            // -------------------------------------------------------------------------------------------------
            Console.WriteLine("\n-- LOOP SCOPE EXAMPLE --");
            for (int i = 0; i < 3; i++)
            {
                int inner = i * 10;
                Console.WriteLine($"Inner value: {inner}");
            }
            // Console.WriteLine(inner); // would cause an error - 'inner' is out of scope

            // =================================================================================================
            // 5. BREAK / CONTINUE / RETURN
            // -------------------------------------------------------------------------------------------------
            // break     -> exits the current loop immediately
            // continue  -> skips the rest of the loop iteration and moves to the next
            // return    -> exits the method entirely
            // -------------------------------------------------------------------------------------------------
            Console.WriteLine("\n-- BREAK & CONTINUE --");

            for (int i = 1; i <= 10; i++)
            {
                if (i == 3)
                {
                    Console.WriteLine("Skipping number 3...");
                    continue; // skip this iteration
                }

                if (i == 7)
                {
                    Console.WriteLine("Reached 7, stopping loop early.");
                    break; // exit loop completely
                }

                Console.WriteLine($"i = {i}");
            }

            // Example: using 'return' to end method early
            Console.WriteLine("\n-- RETURN Example --");
            CheckTemperature(15);
            CheckTemperature(35);

            // =================================================================================================
            // 6. COMBINED LOGIC - SIMPLE PROGRAM FLOW
            // -------------------------------------------------------------------------------------------------
            // Now combine everything: logic + loops + decisions
            // -------------------------------------------------------------------------------------------------
            Console.WriteLine("\n-- COMBINED EXAMPLE: Battery Drain Simulation --");

            int battery = 100;

            while (battery > 0)
            {
                Console.WriteLine($"Battery at {battery}%");

                if (battery > 75)
                    Console.WriteLine("Battery is healthy.");
                else if (battery > 30)
                    Console.WriteLine("Battery medium.");
                else
                    Console.WriteLine("Battery low! Please recharge soon.");

                battery -= 25;

                System.Threading.Thread.Sleep(250); // small delay for readability
            }

            Console.WriteLine("Battery empty. Shutting down...");

            // =================================================================================================
            // 7. TERNARY RECAP (Compact Conditional)
            // -------------------------------------------------------------------------------------------------
            // Quick review: A short way to assign a value based on a condition.
            // -------------------------------------------------------------------------------------------------
            int age = 17;
            string eligibility = (age >= 18) ? "Adult" : "Minor";
            Console.WriteLine($"\nTernary example - Age: {age} -> {eligibility}");

            // =================================================================================================
            // SUMMARY
            // -------------------------------------------------------------------------------------------------
            // - if / else -> branching logic
            // - switch -> cleaner multi-branch decisions
            // - loops -> repeat work until condition changes
            // - break / continue / return -> control flow inside loops
            // - ternary -> short form of if/else
            // -------------------------------------------------------------------------------------------------
            Console.WriteLine("\n=== END OF 04_CONTROL_FLOW MODULE ===");
        }

        // -------------------------------------------------------------------------------------------------
        // SUPPORTING METHOD: Demonstrates 'return' in action
        // -------------------------------------------------------------------------------------------------
        static void CheckTemperature(int temp)
        {
            if (temp < 0)
            {
                Console.WriteLine("It's freezing outside!");
                return; // stop executing this method here
            }

            if (temp > 30)
            {
                Console.WriteLine("It's a hot day!");
                return;
            }

            Console.WriteLine("Temperature is comfortable.");
        }
    }
}
