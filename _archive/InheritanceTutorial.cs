using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_zero_hero
{
    // File: InheritanceDemo.cs
    // ===========================================
    // Focus: C# inheritance with Vehicles & Cars
    // - virtual, override, sealed
    // - class-level access modifiers (public, internal, sealed)
    // - member access modifiers (private, protected, internal, public)

    using System;

    namespace VehicleInheritanceDemo
    {
        // -------------------------------------------
        // Class-level access modifiers:
        // -------------------------------------------
        // public   : visible to any code that references this assembly.
        // internal : visible only within this assembly.
        // sealed   : prevents further inheritance.
        //
        // (Note: sealed classes still obey public/internal visibility.)
        // -------------------------------------------

        /// <summary>
        /// 1) Base class: Vehicle
        ///    - Declared public so it can be used anywhere.
        ///    - Defines common data & behavior for all vehicles.
        /// </summary>
        public class Vehicle
        {
            // ---------------------------------------
            // Member access modifiers:
            // ---------------------------------------
            // private   : this class only.
            // protected : this class & its derived classes.
            // internal  : this assembly only.
            // public    : everyone.
            // ---------------------------------------

            // private field: not visible to derived classes or other code.
            private readonly string _engineSerial = "ENG-" + Guid.NewGuid();

            // protected property: like a BankAccount's protected PIN,
            // visible to derived classes (e.g. Car) but hidden from outside.
            protected string Vin { get; }

            // internal property: visible only within this assembly.
            internal string Manufacturer { get; set; }

            // public property: visible everywhere.
            public string Model { get; set; }

            /// <summary>
            /// virtual method: provides a default implementation,
            /// but allows derived classes to override.
            /// </summary>
            public virtual void StartEngine()
            {
                Console.WriteLine("Vehicle engine starting (base).");
            }

            /// <summary>
            /// virtual method that derived classes can override.
            /// </summary>
            public virtual void StopEngine()
            {
                Console.WriteLine("Vehicle engine stopping (base).");
            }

            // Constructor sets read-only Vin and initial properties
            public Vehicle(string vin, string model, string manufacturer)
            {
                Vin = vin;
                Model = model;
                Manufacturer = manufacturer;
            }

            /// <summary>
            /// Shows the private engine serial -- allowed here,
            /// but _not_ accessible from Car or outside.
            /// </summary>
            public void ShowEngineSerial()
            {
                Console.WriteLine($"Engine Serial: {_engineSerial}");
            }
        }

        /// <summary>
        /// 2) Derived class: Car
        ///    - Inherits Vehicle
        ///    - Overrides virtual methods
        ///    - Seals one override to prevent further overrides
        /// </summary>
        public class Car : Vehicle
        {
            // public property specific to Car
            public int NumberOfDoors { get; set; }

            /// <summary>
            /// override keyword: replaces the base StartEngine().
            /// Now Car has its own implementation.
            /// </summary>
            public override void StartEngine()
            {
                Console.WriteLine($"Car {Model} engine roaring! (doors: {NumberOfDoors})");
                // Accessing protected Vin from base class:
                Console.WriteLine($"VIN: {Vin}");
            }

            /// <summary>
            /// sealed override: stops inheritance chain here.
            /// No subclass can further override StopEngine().
            /// </summary>
            public sealed override void StopEngine()
            {
                Console.WriteLine($"Car {Model} engine shutting down.");
            }

            public Car(string vin, string model, string manufacturer, int doors)
                : base(vin, model, manufacturer)
            {
                NumberOfDoors = doors;
            }
        }

        /// <summary>
        /// 3) Sealed class: SportsCar
        ///    - sealed at the class level: no other class can inherit from SportsCar.
        ///    - Still able to override non-sealed virtual members.
        /// </summary>
        public sealed class SportsCar : Car
        {
            public double TopSpeedMph { get; set; }

            // Allowed: StartEngine was not sealed in Car.
            public override void StartEngine()
            {
                Console.WriteLine($"SportsCar {Model} blasts off to {TopSpeedMph} mph!");
            }

            // Cannot override StopEngine() here because Car sealed it.

            public SportsCar(string vin, string model, string manufacturer, int doors, double topSpeed)
                : base(vin, model, manufacturer, doors)
            {
                TopSpeedMph = topSpeed;
            }
        }

        /// <summary>
        /// 4) internal class: Motorcycle
        ///    - internal at class level: only visible within this assembly.
        ///    - Demonstrates that not every derived type needs to be public.
        /// </summary>
        internal class Motorcycle : Vehicle
        {
            // Inherits StartEngine()/StopEngine() from Vehicle (no overrides here).
            public Motorcycle(string vin, string model, string manufacturer)
                : base(vin, model, manufacturer) { }
        }

        /// <summary>
        /// Demo runner: shows polymorphic behavior and access rules.
        /// </summary>
        class Program
        {
            static void methodToHold()
            {
                // Base class instance
                Vehicle v = new Vehicle("VIN0001", "Generic", "Acme Motors");
                v.StartEngine();  // Vehicle.StartEngine
                v.StopEngine();   // Vehicle.StopEngine

                Console.WriteLine();

                // Derived Car
                Car c = new Car("VIN0002", "Sedan", "Acme Motors", 4);
                c.StartEngine();  // Car.StartEngine override
                c.StopEngine();   // Car.StopEngine (sealed override)
                c.ShowEngineSerial(); // can call base's public method

                Console.WriteLine();

                // Derived SportsCar
                SportsCar sc = new SportsCar("VIN0003", "Roadster", "SpeedCo", 2, 180);
                sc.StartEngine(); // SportsCar.StartEngine override
                sc.StopEngine();  // Car.StopEngine (no further override)

                Console.WriteLine();

                // internal Motorcycle (only works here, same assembly)
                Motorcycle m = new Motorcycle("VIN0004", "Ninja", "MotoCorp");
                m.StartEngine();
                m.StopEngine();
            }
        }
    }
}