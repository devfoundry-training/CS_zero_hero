// =====================================================================================================
// TASK 01 - FUNDAMENTALS: PROJECT SETUP & RUNTIME BASICS
// -----------------------------------------------------------------------------------------------------
// GOAL:
// Create your first C# project using the .NET CLI and explore how it runs.
//
// This task reinforces the concepts from 01_FUNDAMENTALS.cs:
// - What .NET is
// - How projects are built
// - Where Main() lives
// - How to use the CLI and inspect a .csproj file
// -----------------------------------------------------------------------------------------------------
//
// INSTRUCTIONS:
//
// 1.  Create a new folder for your exercises (e.g., "CSharpCourse").
//
// 2.  Open a terminal in that folder and create a new console project:
//
//         dotnet new console -n HelloDotNet
//
//     This will create a new folder and generate:
//       - Program.cs
//       - HelloDotNet.csproj
//
// 3.  Open Program.cs and read the code.
//
//     - Identify where execution starts (the Main method).
//     - Add one extra Console.WriteLine() to prove you can edit and run it.
//
// 4.  Run the project:
//
//         cd HelloDotNet
//         dotnet run
//
//     Observe how .NET builds, restores packages, and executes.
//
// 5.  Edit HelloDotNet.csproj and inspect its structure.
//
//     - Locate the <PropertyGroup> and <ItemGroup> sections.
//     - Change <OutputType> to "Exe" (if not already).
//     - Add a comment inside the file explaining what the TargetFramework means.
//
// 6.  Build manually:
//
//         dotnet build
//
//     Then look inside:
//         bin/Debug/net10.0/
//     You should see a DLL (IL assembly).
//
// 7.  OPTIONAL: Try the modern "top-level statements" form.
//
//     - Delete the Program class and Main method.
//     - Write this directly in Program.cs:
//
//         Console.WriteLine("Hello from top-level statements!");
//
//     - Run again: `dotnet run`
//
// -----------------------------------------------------------------------------------------------------
//
// DISCUSSION POINTS (for debrief):
// - What happens when you build?  
// - Where is IL stored?  
// - What does the .csproj control?  
// - What does "managed code" mean?  
// -----------------------------------------------------------------------------------------------------
//
// OUTPUT EXAMPLE:
//
//   Hello from Main!
//   Hello from top-level statements!
// -----------------------------------------------------------------------------------------------------
//
// CHALLENGE (stretch goal):
// - Create another project named "AboutDotNet" that prints the following:
//   1. What .NET version you're running on (hint: Environment.Version)
//   2. Whether the current OS is Windows, Linux, or macOS (Environment.OSVersion)
// -----------------------------------------------------------------------------------------------------
//
// END OF TASK 01
// =====================================================================================================
