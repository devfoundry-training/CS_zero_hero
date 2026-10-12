
// =====================================================================================================
// TASK 09 - FILE I/O PRACTICE
// -----------------------------------------------------------------------------------------------------
// GOAL:
// Practice reading, writing, and managing text files and directories in C#.
//
// This task reinforces concepts from 09_IO.cs:
// - Writing and reading text files
// - Using StreamReader and StreamWriter
// - Managing directories and file paths
// - Checking for missing files safely
// -----------------------------------------------------------------------------------------------------
//
// INSTRUCTIONS:
//
// 1. Create a new console project:
//
//        dotnet new console -n IOPlayground
//        cd IOPlayground
//
// 2. Replace the code in Program.cs with this file.
//    Fill in the blanks or TODO sections as instructed.
//
// -----------------------------------------------------------------------------------------------------
using System;
using System.IO;
Console.WriteLine("=== FILE I/O PRACTICE ===\n");
// =====================================================================================================
// 1. WRITE A FILE
// -----------------------------------------------------------------------------------------------------
// Create an array of names and write it to a new file using File.WriteAllLines().
// Then append one extra name using File.AppendAllText().
// -----------------------------------------------------------------------------------------------------
string path = "characters.txt";
string[] characters = { "Luke Skywalker", "Leia Organa", "Han Solo" };
// TODO: Write the array 'characters' to the file 'path'.
// File.__________________________;
// TODO: Append a new line with "Chewbacca".
// File.__________________________;
// Print confirmation
Console.WriteLine("Characters written to file.\n");
// =====================================================================================================
// 2. READ A FILE
// -----------------------------------------------------------------------------------------------------
// Read all lines back using File.ReadAllLines() and display them.
// -----------------------------------------------------------------------------------------------------
// TODO: Read all lines from the file and store them in an array.
string[] lines = __________________________;
// Loop through each line and print
foreach (string line in lines)
{
    Console.WriteLine($"Character: {line}");
}
// Challenge: Use File.ReadAllText() to print the entire file as one string.
string allText = __________________________;
Console.WriteLine("\nFile contents as one string:");
Console.WriteLine(allText);
// =====================================================================================================
// 3. STREAMWRITER / STREAMREADER
// -----------------------------------------------------------------------------------------------------
// Use StreamWriter to create a new file "planets.txt" and write three planet names.
// Then read and print them with StreamReader.
// -----------------------------------------------------------------------------------------------------
string planetPath = "planets.txt";
// TODO: Use StreamWriter inside a 'using' block to write planet names.
using (StreamWriter writer = new StreamWriter(planetPath))
{
    // writer.WriteLine("Mercury");
    // writer.WriteLine("Venus");
    // writer.WriteLine("Earth");
}
// TODO: Use StreamReader to read each line and print it.
using (StreamReader reader = new StreamReader(planetPath))
{
    string? line;
    while (/* condition: while line is not null */)
    {
        // Read line and print
        __________________________;
    }
}
// =====================================================================================================
// 4. DIRECTORY AND PATH OPERATIONS
// -----------------------------------------------------------------------------------------------------
// Create a folder, write a file inside it, and list all files within.
// -----------------------------------------------------------------------------------------------------
string dirPath = "ExampleDir";
// TODO: Check if directory exists - if not, create it.
if (/* condition */)
{
    Directory.__________________________;
    Console.WriteLine($"Created directory: {dirPath}");
}
// TODO: Combine path and file name using Path.Combine().
string filePath = __________________________;
// Write a short message to the new file
File.WriteAllText(filePath, "This file lives inside ExampleDir.");
// TODO: Display the full path using Path.GetFullPath()
Console.WriteLine($"File created at: __________________________");
// TODO: Get all files in the directory and print their names
string[] files = __________________________;
foreach (string f in files)
{
    Console.WriteLine($"Found file: {Path.GetFileName(f)}");
}
// =====================================================================================================
// 5. FILE SAFETY CHECK
// -----------------------------------------------------------------------------------------------------
// Always check for missing files before reading them.
// -----------------------------------------------------------------------------------------------------
string missing = "missing.txt";
// TODO: Check if the file exists before reading it.
if (/* condition */)
{
    Console.WriteLine(File.ReadAllText(missing));
}
else
{
    Console.WriteLine("File not found. Skipping safely.");
}
// =====================================================================================================
// SUMMARY
// -----------------------------------------------------------------------------------------------------
//  File.WriteAllLines / ReadAllLines - text file operations
//  StreamWriter / StreamReader - controlled file access
//  Directory and Path classes - manage folders and paths
//  Always check File.Exists() before reading
// -----------------------------------------------------------------------------------------------------
Console.WriteLine("\n=== END OF I/O PRACTICE ===");

