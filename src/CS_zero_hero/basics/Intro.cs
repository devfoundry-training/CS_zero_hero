// =====================================================================================================
// 01_FUNDAMENTALS.cs
// -----------------------------------------------------------------------------------------------------
// PURPOSE:
// Introduce how C# works - from compilation to runtime - and how a typical .NET project is structured.
//
// This is a conceptual teaching file: read and discuss it before writing code.
// -----------------------------------------------------------------------------------------------------
//
// LESSON OBJECTIVES:
// - Understand what C# is (language type, runtime, memory management)
// - Learn how .NET builds and runs a project
// - Understand what lives inside a .csproj file
// - Know where code execution begins (Main)
// - Understand the .NET CLI and NuGet
// - See what folders and files make up a real-world project
// - Learn what 'using', 'namespace', and 'internal' mean in a C# file
// -----------------------------------------------------------------------------------------------------
//
// WHAT IS C#
//
// - C# (pronounced "C-Sharp") is a modern, statically-typed, object-oriented language.
// - It runs on the .NET platform - a managed runtime environment.
// - C# source files compile into IL (Intermediate Language) inside a .dll or .exe.
// - The Common Language Runtime (CLR) executes IL, using the JIT compiler to turn IL into native code.
// - The CLR also manages memory automatically using the Garbage Collector (GC).
// -----------------------------------------------------------------------------------------------------
//
// THE .NET PLATFORM
//
// ".NET" is the platform that runs C# applications. It includes:
//
// 1. **.NET Runtime (CLR)**  
//     - Executes compiled code (IL)  
//     - Manages memory, handles exceptions, provides type safety
//
// 2. **Base Class Library (BCL)**  
//     - Core functionality: collections, IO, LINQ, threading, async/await
//
// 3. **.NET SDK (Software Development Kit)**  
//     - Compilers, templates, and build tools used to create and run projects
//
// 4. **.NET CLI (Command Line Interface)**  
//     - The `dotnet` tool: used to build, run, test, and publish apps
//
// 5. **Ecosystem & Workloads**  
//     - Supports multiple app types:  
//         - Console apps  
//         - Web APIs (ASP.NET Core)  
//         - Desktop (WPF / WinForms)  
//         - Mobile (MAUI)  
//         - Cloud and microservices
// -----------------------------------------------------------------------------------------------------
//
// .NET EVOLUTION
//
// - .NET Framework (Windows-only, 2002-2019)
// - .NET Core (cross-platform rewrite, 2016-2020)
// - .NET 5+ (modern, unified platform; now simply called ".NET")
// -----------------------------------------------------------------------------------------------------
//
// MANAGED VS UNMANAGED CODE
//
// - Managed code runs under the CLR - memory, type safety, and exceptions are handled automatically.
// - Unmanaged code (like C/C++) runs directly on the OS - you manage memory manually.
// - C# can call unmanaged code when needed (via P/Invoke or COM Interop).
// -----------------------------------------------------------------------------------------------------
//
// USING DIRECTIVES
//
// - The `using` keyword imports **namespaces** (collections of related classes) so you can use them
//   without writing the full name.
//
// Example:
//      using System.ComponentModel.DataAnnotations;
//      [Required] public string Name { get; set; }
//
// Without `using`, you'd write the full path:
//      [System.ComponentModel.DataAnnotations.Required]
//
// - Think of it like an "import" statement in other languages.
// -----------------------------------------------------------------------------------------------------
//
// NAMESPACES
//
// - A **namespace** groups related classes logically and avoids naming conflicts.
//
// Example:
//      namespace CS_Tutorial
//      {
//          public class Program { }
//      }
//
// - The fully qualified name of Program is `CS_Tutorial.Program`.
// - Namespaces often mirror your folder structure (e.g., Models, Services, Utils).
// -----------------------------------------------------------------------------------------------------
//
// ACCESS MODIFIERS - INTERNAL VS PUBLIC
//
// - `internal` (default) -> visible **only within the same assembly** (your current project).
// - `public` -> visible to **all projects** that reference this one.
// - `private` -> visible **only inside its class**.
// - `protected` -> visible to derived (child) classes.
//
// Example:
//      internal class Program { }  // used inside the same project
//      public class Program { }    // visible from other assemblies
//
// Internal is ideal for teaching/demo projects where you don't need external access.
// -----------------------------------------------------------------------------------------------------
//
// EXECUTION FLOW (Visual Summary)
//
//   Source Code (.cs)
//       v
//   Roslyn Compiler -> IL (Intermediate Language)
//       v
//   .NET Runtime (CLR)
//       v
//   JIT Compilation -> Native Machine Code
//       v
//   Execution (on CPU)
// -----------------------------------------------------------------------------------------------------
//
// HOW C# EXECUTES
//
// 1.  You write .cs source files.
// 2.  The compiler (Roslyn) converts them into IL.
// 3.  The IL code is packaged into an assembly (.dll or .exe).
// 4.  The CLR loads the assembly and JIT-compiles IL -> native code.
// 5.  Execution begins at the program entry point (Main).
// -----------------------------------------------------------------------------------------------------
//
// ENTRY POINT
//
// - Every C# console app has a static entry point: `static void Main(string[] args)`.
// - Execution begins there and ends when Main returns.
// - Since C# 9, you can omit Main and use *top-level statements* (simpler syntax).
// -----------------------------------------------------------------------------------------------------
//
// ENTRY POINT EXAMPLES
//
// Classic (pre C# 9):
//
//      using System;
//
//      namespace DemoApp
//      {
//          internal class Program
//          {
//              static void Main(string[] args)
//              {
//                  Console.WriteLine("Hello from Main!");
//              }
//          }
//      }
//
// Modern (C# 9+ top-level statements):
//
//      using System;
//
//      Console.WriteLine("Hello, world!");
//
// The compiler automatically generates a hidden Program class and Main() method behind the scenes.
// -----------------------------------------------------------------------------------------------------
//
// THE .NET CLI - YOUR TOOLCHAIN
//
// The .NET CLI (`dotnet`) is used for almost everything:
//
//      dotnet new console -n MyApp        -> Create a new project
//      dotnet build                       -> Compile source to IL
//      dotnet run                         -> Build and execute
//      dotnet add package <name>          -> Add a NuGet dependency
//      dotnet restore                     -> Restore packages
//      dotnet test                        -> Run tests
//      dotnet publish                     -> Package for deployment
//
// The CLI uses MSBuild (the same build engine Visual Studio uses) behind the scenes.
// -----------------------------------------------------------------------------------------------------
//
// NUGET - .NET'S PACKAGE MANAGER
//
// - NuGet is how .NET handles libraries and dependencies - like NPM (JavaScript) or Maven (Java).
// - Packages are downloaded from feeds (default: https://www.nuget.org).
// - Example commands:
//
//      dotnet add package Newtonsoft.Json
//      dotnet add package Serilog
//
// - Packages are declared inside the .csproj:
//
//      <ItemGroup>
//         <PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
//      </ItemGroup>
//
// - When you build, the CLI automatically restores any missing packages.
// -----------------------------------------------------------------------------------------------------
//
// PROJECT STRUCTURE & ARCHITECTURE
//
// - Projects contain source files and a .csproj descriptor.
// - Solutions (.sln) group related projects (e.g., App + Tests + Library).
//
// Typical structure:
//
//   MyApp/
//   +-- Program.cs                 <- Entry point (Main method)
//   +-- Models/                    <- Data structures
//   +-- Services/                  <- Core logic
//   +-- Utils/                     <- Helpers
//   +-- Tasks/                     <- Practice files
//   +-- MyApp.csproj               <- Project descriptor
//
// This structure keeps code modular, readable, and maintainable.
// -----------------------------------------------------------------------------------------------------
//
// WHAT THE LANGUAGE CONTAINS
//
// - Namespaces and assemblies  -> organize code
// - Classes, structs, records  -> define data and behavior
// - Methods, properties, fields -> hold logic and state
// - Enums, interfaces           -> define categories and contracts
// - Generics                   -> type-safe, reusable logic
// - Exceptions                 -> safe error handling
// - Async/await                -> concurrency model
// - LINQ                       -> querying data
// -----------------------------------------------------------------------------------------------------
// THE .CSPROJ FILE - PROJECT BLUEPRINT
//
// - The `.csproj` (C# Project File) is an XML-based descriptor that defines *how your app is built*.
//
// - It stores all the metadata the .NET SDK needs:  
//     - Which framework to target  
//     - What type of app it is (Console, Library, Web, etc.)  
//     - Which dependencies or packages to include  
//     - Optional build or compiler settings
//
// Example of a simple .csproj:
//
//     <Project Sdk="Microsoft.NET.Sdk">
//       <PropertyGroup>
//         <OutputType>Exe</OutputType>               <!-- Exe = Console app -->
//         <TargetFramework>net10.0</TargetFramework>  <!-- .NET 10 runtime -->
//         <ImplicitUsings>enable</ImplicitUsings>     <!-- Auto-import common namespaces -->
//         <Nullable>enable</Nullable>                 <!-- Enable null-safety checks -->
//       </PropertyGroup>
//
//       <ItemGroup>
//         <PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
//       </ItemGroup>
//     </Project>
//
// ----------------------------------------------------------------
// Key Elements:
//
// - <Project> -> Root node. The SDK defines which build tools to use (Microsoft.NET.Sdk).  
// - <PropertyGroup> -> General settings (framework, build type, options).  
// - <ItemGroup> -> Lists dependencies, project links, or additional resources.  
// - <PackageReference> -> Adds external NuGet libraries.  
// - <ProjectReference> -> Links to another .csproj (multi-project solutions).
//
// ----------------------------------------------------------------
// Additional Notes:
//
// - You can have multiple <PropertyGroup> blocks for Debug/Release settings.  
// - The .csproj is automatically edited when you run CLI commands like:
//       dotnet add package <name>
//       dotnet add reference <path-to-other-project>
// - .NET auto-includes all .cs files by default (you rarely need to list them manually).
// - The output of the build (a DLL or EXE) depends on <OutputType>.
//

using System;

namespace CS_Zero_Hero.basics
{
    internal class Fundamentals
    {
        static void Main(string[] args)
        {
            // This Main() exists only so the project compiles and runs.
            // There's no executable logic yet - this module is conceptual.
            Console.WriteLine("Fundamentals module loaded.");
        }
    }
}
