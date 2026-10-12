using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_zero_hero
{
    public class StaticClassTutorial
    {
        // ---------------------------------------
        // 1. Const field: compile-time constant
        // ---------------------------------------
        public const int MaxCarAge = 30;

        // ---------------------------------------
        // 2. Readonly field: runtime constant
        // ---------------------------------------
        public readonly int ReadonlyField;

        // ---------------------------------------
        // 3. Property with private setter
        // ---------------------------------------
        public int ReadonlyProperty { get; private set; }

        // ---------------------------------------
        // Constructor
        // ---------------------------------------
        public StaticClassTutorial(int year)
        {
            ReadonlyField = year;        // [OK] Allowed
            ReadonlyProperty = year;     // [OK] Allowed

            // MaxCarAge = 40; // [X] Error: cannot assign to const
        }

        // ---------------------------------------
        // Attempt to modify readonly field
        // ---------------------------------------
        public void TryModifyReadonlyField()
        {
            // ReadonlyField = 2025; // [X] Error: cannot assign to readonly field
        }

        // ---------------------------------------
        // Modify property (legal inside class)
        // ---------------------------------------
        public void ModifyReadonlyProperty()
        {
            ReadonlyProperty++; // [OK] Allowed
        }

        // ---------------------------------------
        // Demonstrate const value
        // ---------------------------------------
        public static void ShowConst()
        {
            Console.WriteLine($"Const value (MaxCarAge): {MaxCarAge}");

            // MaxCarAge = 35; // [X] Error: cannot assign to const
        }

        // ---------------------------------------
        // Demonstrate readonly field
        // ---------------------------------------
        public static void ShowReadonlyField()
        {
            var car = new StaticClassTutorial(2022);
            Console.WriteLine($"Readonly field: {car.ReadonlyField}");

            // car.ReadonlyField = 2023; // [X] Error
        }

        // ---------------------------------------
        // Demonstrate readonly-style property
        // ---------------------------------------
        public static void ShowReadonlyProperty()
        {
            var car = new StaticClassTutorial(2020);
            car.ModifyReadonlyProperty(); // [OK] Allowed
            Console.WriteLine($"Readonly property: {car.ReadonlyProperty}");

            // car.ReadonlyProperty = 2021; // [X] Error
        }
    }
}
