// ============================================
// File: InheritanceLab_StepsWithSolution.cs
// ============================================
// Lab: Inheritance with Shapes
// Steps annotated with solution code beneath each.

// Step 1: Open the Labs Console project.
//  - Make it the startup project in your solution.

// Step 2: Add System.Windows.Forms (COM) reference.
//  a) Right-click Dependencies -> Add Project Reference...  
//  b) Choose COM on the left.  
//  c) Search "Windows" on the right.  
//  d) Check "System_Windows_Forms" and click OK.  
//  (Provides System.Drawing.Point and Color.)

using System;
using System.Collections.Generic;
using System.Drawing;   // For Point & Color

namespace CS_Zero_Hero.tasks.inheritance_lab
{
    // ==================================================
    // Step 3: Define the base class Shape
    //  a) public class Shape
    //  b) protected properties: Color Colour; Point Position
    //  c) constructor(Color, Point)
    //  d) virtual string GetDetails()
    // ==================================================
    public class Shape
    {
        protected Color Colour { get; }
        protected Point Position { get; }

        public Shape(Color colour, Point position)
        {
            Colour = colour;
            Position = position;
        }

        public virtual string GetDetails()
        {
            return $"Shape @ ({Position.X},{Position.Y}), Colour = {Colour.Name}";
        }
    }

    // ==================================================
    // Step 4: Define Rectangle : Shape
    //  a) public class Rectangle : Shape
    //  b) public int Width, Height
    //  c) constructor(Color, Point, int, int)
    //  d) override GetDetails()
    // ==================================================
    public class Rectangle : Shape
    {
        public int Width { get; }
        public int Height { get; }

        public Rectangle(Color colour, Point position, int width, int height)
            : base(colour, position)
        {
            Width = width;
            Height = height;
        }

        public override string GetDetails()
        {
            return base.GetDetails()
                 + $", Rectangle WxH = {Width}x{Height}";
        }
    }

    // ==================================================
    // Step 5: Define Circle : Shape
    //  a) public class Circle : Shape
    //  b) public double Radius
    //  c) constructor(Color, Point, double)
    //  d) virtual double Area, Circumference
    //  e) override GetDetails()
    // ==================================================
    public class Circle : Shape
    {
        public double Radius { get; }

        public Circle(Color colour, Point position, double radius)
            : base(colour, position)
        {
            Radius = radius;
        }

        public virtual double Area
            => Math.PI * Radius * Radius;

        public virtual double Circumference
            => 2 * Math.PI * Radius;

        public override string GetDetails()
        {
            return base.GetDetails()
                 + $", Circle R={Radius:F1}, A={Area:F2}, C={Circumference:F2}";
        }
    }

    // ==================================================
    // Step 6: Define Sphere : Circle
    //  a) public class Sphere : Circle
    //  b) public double Volume => 4/3 * pi * R^3
    //  c) constructor(Color, Point, double)
    //  d) override GetDetails()
    // ==================================================
    public class Sphere : Circle
    {
        public double Volume
            => (4.0 / 3.0) * Math.PI * Math.Pow(Radius, 3);

        public Sphere(Color colour, Point position, double radius)
            : base(colour, position, radius)
        {
        }

        public override string GetDetails()
        {
            return base.GetDetails()
                 + $", Sphere Volume={Volume:F2}";
        }
    }

    class Program
    {
        // ==================================================
        // Step 7: Add static void Lab7()
        // ==================================================
        static void Lab7()
        {
            // 7.1) Create List<Shape>
            var shapes = new List<Shape>();

            // Step 8: Instantiate shapes and add to list
            shapes.Add(new Rectangle(
                colour: Color.Red,
                position: new Point(10, 20),
                width: 30,
                height: 40));
            shapes.Add(new Circle(
                colour: Color.Blue,
                position: new Point(50, 60),
                radius: 15.5));
            shapes.Add(new Sphere(
                colour: Color.Green,
                position: new Point(5, 5),
                radius: 7.2));

            // Step 9: foreach to print each.GetDetails()
            Console.WriteLine("=== Shape Details ===");
            foreach (var shape in shapes)
            {
                Console.WriteLine(shape.GetDetails());
            }
        }

        static void Main(string[] args)
        {
            // Step 10: Call Lab7()
            Lab7();

            // Step 11: Build & run -> verify output
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
