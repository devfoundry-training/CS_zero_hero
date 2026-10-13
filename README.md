# C# Zero to Hero

[![Tasks](https://github.com/devfoundry-training/CS_zero_hero/actions/workflows/tasks.yml/badge.svg)](https://github.com/devfoundry-training/CS_zero_hero/actions/workflows/tasks.yml)

A structured teaching repository covering C# 14 and .NET 10 (LTS) from first principles through to OOP, LINQ, and WPF. Each module is a self-contained file with runnable examples and in-code commentary. Part of the Zero to Hero course family.

---

## Who Is This For?

- Developers learning C# for the first time or transitioning from another language
- Delegates on instructor-led C# fundamentals courses
- Self-learners who want a clear, progressive path through the language and platform

No prior C# experience is assumed. Basic programming awareness is helpful.

---

## Prerequisites

| Tool       | Minimum Version |
|------------|----------------|
| .NET SDK   | 10.0           |
| Python     | 3.10 (only for the task checker) |

```bash
dotnet --version
```

---

## Getting Started

```bash
# Clone the repository
git clone git@github.com:Edrzapi/CS_zero_hero.git
cd CS_zero_hero

# Build the topic project
dotnet build src/CS_zero_hero

# List the topics, then run one
dotnet run --project src/CS_zero_hero
dotnet run --project src/CS_zero_hero -- collections

# Build everything (topics, tests and every task) and run every test
dotnet build
dotnet test
```

Each topic file has its own `Main()` method. `Program.cs` is the single entry point: give it the topic's folder name and it runs that topic.

The WPF demo is a separate project because WPF only exists on Windows. It is not part of `CS_zero_hero.sln`; on Windows, build and run it with:

```bash
dotnet run --project src/CS_zero_hero_wpf
```

---

## Repository Structure

```
CS_zero_hero.sln                 # Everything except the WPF demo: dotnet build / dotnet test
src/
  CS_zero_hero/
    basics/                      # .NET platform, compilation, project structure
    datatypes/                   # Value types, reference types, nullable, boxing
    operators/                   # Arithmetic, logical, comparison, ternary
    control_flow/                # if/else, switch, break/continue
    iteration/                   # for, while, foreach, IEnumerator, yield
    collections/                 # List, Dictionary, Queue, Stack, HashSet
    string_manipulation/         # Substring, split, join, replace, parsing
    io/                          # File read/write, StreamReader/Writer, paths
    exceptions/                  # try/catch/finally, custom exceptions, EAFP/LBYL
    oop/                         # Classes, properties, static members, four pillars
    linq/                        # Query syntax, method syntax, filtering, grouping
    Program.cs                   # Entry point: runs a topic by name
    CS_zero_hero.csproj          # Project configuration (.NET 10)
  CS_zero_hero_wpf/              # WPF demo: window controls, events, layout (Windows only)
  CS_zero_heroTests/
    CS_zero_heroTests.csproj     # MSTest project
    LessonTests.cs               # First tests, for the datatypes and oop lessons
tasks/                           # The Library Manager: 12 end-of-session tasks and a bonus
practice/                        # Fill-in-the-blank exercises, one per topic (not compiled; see practice/README.md)
_archive/                        # Legacy draft files (kept for reference)
```

---

## Topics Covered

### Platform and Language Fundamentals

- **Fundamentals:** What C# is, .NET runtime (CLR, JIT), compilation pipeline, project structure, .csproj, namespaces, access modifiers, .NET CLI.
- **Data Types:** Value types (int, double, bool, char, struct, enum), reference types (string, class, array), type inference with `var`, boxing/unboxing, nullable types.
- **Operators:** Arithmetic, assignment, comparison, logical, increment/decrement, operator precedence, ternary expressions.

### Control Flow and Iteration

- **Control Flow:** `if`/`else if`/`else`, traditional switch and switch expressions (C# 8+), loops (`for`, `while`, `do-while`, `foreach`), `break`/`continue`/`return`.
- **Iteration:** Loop patterns, `IEnumerator`/`IEnumerable` interfaces, manual iteration, `yield return` for lazy evaluation.

### Collections and Data

- **Collections:** `List<T>`, `Dictionary<TKey, TValue>`, `Queue<T>`, `Stack<T>`, `HashSet<T>`, set operations (union, intersection, difference), collection interfaces.
- **String Manipulation:** Substring, split/join, replace, searching, trimming, character analysis, `StringBuilder`, method chaining, CSV parsing.
- **File I/O:** `File.ReadAllText`/`WriteAllText`, `StreamReader`/`StreamWriter`, directory operations, `Path` utilities.

### Error Handling

- **Exceptions:** `try`/`catch`/`finally`, multiple catch blocks, custom exceptions, inner exceptions, LBYL vs EAFP patterns.

### Object-Oriented Programming

- **OOP Basics:** Classes, objects, constructors, properties with validation, static vs instance members, the four pillars (encapsulation, inheritance, polymorphism, abstraction).

### Advanced Topics

- **LINQ:** Query syntax and method syntax, `Where`, `Select`, `OrderBy`, `GroupBy`, `Join`, aggregation (`Sum`, `Count`, `Average`, `Min`, `Max`, `Any`, `All`).
- **WPF Basics:** Window creation, UI controls (TextBox, Button, TextBlock), event handling, StackPanel layout, basic styling.

---

## Tasks

The [`tasks/`](tasks/README.md) folder holds the **Library Manager**, one practical project shared by every Zero to
Hero repo: 12 tasks and a bonus in Python, Java, C#, C++ and JavaScript, and eight tasks (S1 to S8) in SQL's own
order on the same database. Every repo uses the same briefs, the same sample data and the same expected output; only
the code differs.

- **One task per session.** Each task is the end-of-session check and builds on the one before, so by the end the
  learner has one working program.
- **Three levels:** Core (everyone), Stretch (for those who finish early) and Challenge (the strongest learners, or
  homework).
- **Every task folder** has the brief (`README.md`), the exact expected output (`expected.txt`), a `starter/` that
  runs as it is (unfinished parts say "not implemented") and a `solution/` with Core and Stretch.
- **One checker:** `tasks/check/check.py` (Python 3.10 or later, standard library only) runs a program and prints
  `PASS`, or `FAIL` with the first line that differs from `expected.txt`. On Windows, `py` works in place of
  `python`.

[tasks/README.md](tasks/README.md) has the session to task map, the 3-day and 5-day selections, and the run and
check command for every task. To check the first task's solution, from the repo root:

```bash
python tasks/check/check.py 01 -- dotnet run --project tasks/01_bookshelf/solution
```

---

## Teaching Approach

- Each module is a standalone, runnable C# file
- Concepts are explained in comments alongside working code
- Topics progress from simple to complex across the numbered modules
- Tasks give delegates structured exercises to apply what they've learned
- No frameworks beyond the .NET SDK: the focus is on the language and platform

---

## Tech Stack

| Technology | Purpose                              |
|------------|--------------------------------------|
| C# 14      | Language                             |
| .NET 10.0  | Runtime and SDK (LTS, supported to November 2028) |
| MSTest 4   | Testing framework                    |

No Entity Framework and no third-party packages in the core modules. Two tasks use parts of .NET that are not in the core modules: task 10 uses Microsoft's `Microsoft.Data.Sqlite` package from NuGet, and task 12 uses ASP.NET Core, which ships with the .NET SDK.

---

## What's Next?

This course covers C# fundamentals, OOP, LINQ, and WPF basics. For language fundamentals in other languages, see the companion repositories:

- [Python Zero to Hero](https://github.com/Edrzapi/Python_zero_hero)
- [Java Zero to Hero](https://github.com/Edrzapi/Java_zero_hero)
