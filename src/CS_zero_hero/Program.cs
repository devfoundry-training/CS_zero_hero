// =====================================================================================================
// Program.cs
// -----------------------------------------------------------------------------------------------------
// PURPOSE:
// One entry point for the whole project. Every topic file has its own Main() method, so this launcher
// picks one by folder name and runs it:
//
//     dotnet run --project src/CS_zero_hero -- collections
//
// Run it with no name to list the topics.
// -----------------------------------------------------------------------------------------------------

using System.Reflection;

namespace CS_Zero_Hero
{
    class Program
    {
        static void Main(string[] args)
        {
            // Find every class in a topic namespace (CS_Zero_Hero.collections and so on) that has a
            // static Main method. Reflection lets us call it even though it is private.
            Dictionary<string, MethodInfo> topics = new Dictionary<string, MethodInfo>();
            foreach (Type type in typeof(Program).Assembly.GetTypes())
            {
                MethodInfo? main = type.GetMethod("Main", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
                if (type == typeof(Program) || main == null || type.Namespace == null || !type.Namespace.StartsWith("CS_Zero_Hero."))
                    continue;
                topics[type.Namespace.Substring("CS_Zero_Hero.".Length)] = main;
            }

            if (args.Length == 0 || !topics.ContainsKey(args[0]))
            {
                Console.WriteLine("Usage: dotnet run --project src/CS_zero_hero -- TOPIC");
                Console.WriteLine("Topics: " + string.Join(", ", topics.Keys.OrderBy(name => name, StringComparer.Ordinal)));
                return;
            }

            MethodInfo chosen = topics[args[0]];
            object?[] parameters = chosen.GetParameters().Length == 0 ? Array.Empty<object?>() : new object?[] { args.Skip(1).ToArray() };
            chosen.Invoke(null, parameters);
        }
    }
}
