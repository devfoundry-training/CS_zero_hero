// =====================================================================================================
// 11_OOP_BASICS.cs
// -----------------------------------------------------------------------------------------------------
// PURPOSE:
// Introduce the fundamental concepts of Object-Oriented Programming (OOP) in C#.
//
// OOP models software around real-world entities (objects) that contain both DATA and BEHAVIOUR.
// Instead of just writing sequential functions, OOP encourages structure, reusability,
// and clear relationships between code components.
//
// -----------------------------------------------------------------------------------------------------
//
// LESSON OBJECTIVES:
// - Understand the meaning and purpose of classes and objects
// - Explore the Four Pillars of OOP (Encapsulation, Inheritance, Polymorphism, Abstraction)
// - Learn how to define and instantiate simple classes in C#
// - Understand the difference between instance and static members
// -----------------------------------------------------------------------------------------------------

using System;

namespace CS_Zero_Hero.oop
{
    internal class OOP_Basics
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== OBJECT-ORIENTED PROGRAMMING (OOP) BASICS ===\n");

            // =================================================================================================
            // 1. WHAT IS OBJECT-ORIENTED PROGRAMMING?
            // -------------------------------------------------------------------------------------------------
            // OOP (Object-Oriented Programming) is a software design model based on *objects* -
            // reusable entities that combine DATA (fields/properties) and BEHAVIOUR (methods).
            //
            // Procedural programming executes a list of instructions.
            // OOP organizes those instructions into self-contained units (objects) that interact.
            //
            // Example:
            //   Object: Car
            //   Data (Fields): model, colour, speed
            //   Behaviour (Methods): Start(), Drive(), Stop()
            //
            // OOP helps us write modular, scalable, and maintainable programs.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("-- Creating a Car Object --");

            Car myCar = new Car("Tesla Model 3", "Red", 0);
            myCar.DisplayInfo();
            myCar.Accelerate(60);
            myCar.DisplayInfo();

            // =================================================================================================
            // 2. CLASSES VS OBJECTS
            // -------------------------------------------------------------------------------------------------
            // CLASS -> A *blueprint* defining structure (data + behaviour)
            // OBJECT -> A *real instance* created from that blueprint
            //
            // Think of a class as the design for a house, and an object as a particular house built from it.
            //
            //   Car myCar = new Car("Ford", "Blue", 0);
            //
            // Here:
            //   - Car = class
            //   - myCar = object (instance)
            // -------------------------------------------------------------------------------------------------

            Car car1 = new Car("Ford Mustang", "Blue", 20);
            Car car2 = new Car("Audi A3", "Black", 10);

            car1.Accelerate(30);
            car2.Brake(5);

            car1.DisplayInfo();
            car2.DisplayInfo();

            // =================================================================================================
            // 3. THE FOUR PILLARS OF OOP
            // -------------------------------------------------------------------------------------------------
            // The Four Pillars are the *core design principles* that make OOP powerful.
            // Each will have its own dedicated lesson later.
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n=== THE FOUR PILLARS OF OOP ===\n");

            // -------------------------------------------------------------------------------------------------
            // 1. ENCAPSULATION - "Protect and Package Data"
            // -------------------------------------------------------------------------------------------------
            // - Keeps data (fields) private and exposes controlled access via properties or methods.
            // - Prevents external code from putting the object into an invalid state.
            // - Helps isolate changes and enforce data integrity.
            //
            // Example:
            //   private int _speed;
            //   public int Speed
            //   {
            //       get { return _speed; }
            //       set { if (value >= 0) _speed = value; }
            //   }
            //
            // -> We'll explore full encapsulation patterns next in 12_ENCAPSULATION.cs
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("1. ENCAPSULATION - Hiding internal details and protecting state.");

            // -------------------------------------------------------------------------------------------------
            // 2. INHERITANCE - "Reuse and Extend Behaviour"
            // -------------------------------------------------------------------------------------------------
            // - Allows one class (child) to inherit members from another (parent/base class).
            // - Promotes reusability - common logic sits in the base, specialized logic in derived types.
            //
            // Example (concept):
            //   class Vehicle { public void Start() {...} }
            //   class Car : Vehicle { public void Honk() {...} }
            //
            // -> We'll cover base and derived classes in 13_INHERITANCE.cs
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("2. INHERITANCE - Sharing behaviour between related classes.");

            // -------------------------------------------------------------------------------------------------
            // 3. POLYMORPHISM - "One Interface, Many Forms"
            // -------------------------------------------------------------------------------------------------
            // - Allows different classes to respond differently to the same method call.
            // - Achieved via *virtual* methods and *overriding*.
            //
            // Example (concept):
            //   class Animal { public virtual void Speak() { Console.WriteLine(\"Generic sound\"); } }
            //   class Dog : Animal { public override void Speak() { Console.WriteLine(\"Woof\"); } }
            //
            // -> We'll cover overriding and runtime polymorphism in 14_POLYMORPHISM.cs
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("3. POLYMORPHISM - Redefining shared methods for unique behaviour.");

            // -------------------------------------------------------------------------------------------------
            // 4. ABSTRACTION - "Expose What Matters, Hide What Doesn't"
            // -------------------------------------------------------------------------------------------------
            // - Focus on *what* an object does, not *how* it does it.
            // - Often achieved through abstract classes and interfaces.
            //
            // Example (concept):
            //   abstract class Payment { public abstract void Process(); }
            //   class CardPayment : Payment { public override void Process() {...} }
            //
            // -> We'll explore abstract classes and interfaces in 15_ABSTRACTION.cs
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("4. ABSTRACTION - Simplifying complexity by exposing only essentials.");

            // =================================================================================================
            // 4. PROPERTIES AND ACCESS CONTROL
            // -------------------------------------------------------------------------------------------------
            // C# uses *properties* to safely access private data.
            // This blends Encapsulation and ease of use:
            //
            //   private string _name;
            //   public string Name { get; set; }
            //
            // Or with validation:
            //   public int Age
            //   {
            //       get => _age;
            //       set
            //       {
            //           if (value < 0) Console.WriteLine("Invalid age");
            //           else _age = value;
            //       }
            //   }
            //
            // C# 14 (.NET 10) can skip declaring _age: inside an accessor, `field` is the backing field
            // the compiler creates for you:
            //   public int Age
            //   {
            //       get;
            //       set
            //       {
            //           if (value < 0) Console.WriteLine("Invalid age");
            //           else field = value;
            //       }
            //   }
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n-- Property Demonstration --");

            Person luke = new Person("Luke Skywalker", 25);
            Console.WriteLine($"Name: {luke.Name}, Age: {luke.Age}");

            luke.Age = -5;  // Invalid, will be rejected
            luke.Age = 30;  // Valid
            Console.WriteLine($"Updated Age: {luke.Age}");

            // =================================================================================================
            // 5. STATIC VS INSTANCE MEMBERS
            // -------------------------------------------------------------------------------------------------
            // INSTANCE MEMBERS -> belong to individual objects
            // STATIC MEMBERS -> belong to the class itself (shared across all instances)
            //
            // Example:
            //   static int Car.TotalCars -> shared counter of all created cars
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine($"\nTotal Cars created: {Car.TotalCars}");

            // =================================================================================================
            // SUMMARY
            // -------------------------------------------------------------------------------------------------
            // - Classes define structure; objects are live instances
            // - OOP organizes data and behaviour together
            // - The Four Pillars of OOP:
            //      1. Encapsulation  -> protect data
            //      2. Inheritance    -> reuse structure
            //      3. Polymorphism   -> flexible behaviour
            //      4. Abstraction    -> simplify complexity
            // -------------------------------------------------------------------------------------------------

            Console.WriteLine("\n=== END OF OOP BASICS MODULE ===");
        }
    }

    // -------------------------------------------------------------------------------------------------
    // CLASS EXAMPLE: CAR
    // Demonstrates:
    // - Private fields
    // - Constructor
    // - Public methods
    // - Static member (shared count)
    // -------------------------------------------------------------------------------------------------
    class Car
    {
        private string model;
        private string color;
        private int speed;

        public static int TotalCars = 0; // static counter

        public Car(string model, string color, int speed)
        {
            this.model = model;
            this.color = color;
            this.speed = speed;
            TotalCars++;
        }

        public void Accelerate(int amount)
        {
            speed += amount;
            Console.WriteLine($"{model} accelerated by {amount} km/h.");
        }

        public void Brake(int amount)
        {
            speed -= amount;
            if (speed < 0) speed = 0;
            Console.WriteLine($"{model} slowed down by {amount} km/h.");
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Car: {model}, Colour: {color}, Speed: {speed} km/h");
        }
    }

    // -------------------------------------------------------------------------------------------------
    // CLASS EXAMPLE: PERSON
    // Demonstrates:
    // - Auto-properties
    // - Encapsulation via property validation
    // -------------------------------------------------------------------------------------------------
    class Person
    {
        public string Name { get; set; }
        private int age;

        public int Age
        {
            get => age;
            set
            {
                if (value < 0)
                    Console.WriteLine("Age cannot be negative.");
                else
                    age = value;
            }
        }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
