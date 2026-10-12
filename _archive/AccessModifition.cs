// AccessModifiers.cs

/*
    ==============================
    Access Modifiers in C#
    ==============================

    Access modifiers control the visibility (or accessibility) of classes,
    methods, and other members in your C# program.

    +------------------------------+----------------------------------------------+
    | Modifier                     | Accessible From                              |
    +------------------------------+----------------------------------------------+
    | public                       | Anywhere (inside or outside the project)     |
    | private                      | Only within the same class                   |
    | protected                    | Same class AND derived (child) classes       |
    | internal                     | Only within the same project (assembly)      |
    | protected internal           | Derived classes OR same project              |
    | private protected            | Derived classes AND same project             |
    +------------------------------+----------------------------------------------+

    ------------------------------------------------------------------------------
    internal (Keyword)
    ------------------------------------------------------------------------------
    - Default for top-level classes (if no modifier is specified).
    - Restricts usage to code within the same project.
    - Not accessible from another project in the same solution unless you change
      it to public.

    Example:
*/

using System;

internal class InternalHelper
{
    public void PrintHelp()
    {
        Console.WriteLine("This class is internal. Accessible only in this project.");
    }
}

// This class is public, so it could be accessed from other projects (if referenced).
public class PublicHelper
{
    public void PrintHelp()
    {
        Console.WriteLine("This class is public. Accessible from anywhere.");
    }
}

// Main method to demonstrate usage
//class AccessDemo
//    static void Main()
//    {
//        InternalHelper ih = new InternalHelper();
//        ih.PrintHelp();  // Works fine in the same project

//        PublicHelper ph = new PublicHelper();
//        ph.PrintHelp();  // Also works

//        Console.WriteLine("Access modifiers demonstration complete.");
//    }
//}

/*
    Additional Notes:
    ------------------
    - A project in Visual Studio usually builds into an "assembly" (.dll or .exe).
    - internal means: "only accessible within this assembly."
    - This is useful for hiding implementation details from external code.

    Good Practices:
    - Use internal for classes you don't want other projects to depend on.
    - Use public only for your intended API or reusable library interfaces.

    Want to test this? Try creating another project in the same solution,
    reference this one, and attempt to use InternalHelper - it won't work unless
    it's public!
*/
