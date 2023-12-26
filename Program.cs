using System.Reflection;
using Katas.Lib;

var solvers = Assembly.GetExecutingAssembly().GetTypes()
    .Where(t => t.GetInterface(nameof(ISolver)) != null)
    .OrderBy(t => t.Namespace)
    .ToArray();

if (args.Length == 0)
{
    WriteError("Command not provided", printUsage: true);
    return 1;
}

switch(args[0])
{
    case "list":
        ListSolvers();
        break;
    case "exec":
        return ExecSolver(args);
    default:
        WriteError("Invalid command", printUsage: true);
        return 1;
}

return 0;

string GetUsage()
{
    return $"""
            Usage: dotnet run [command]

            Commands:
             exec [solver.name] Run particular solver
             list               List all available solvers
            """;
}

void WriteError(string errorMessage, bool printUsage = false)
{
    Console.Error.WriteLine($"Error: {errorMessage}");
    if (printUsage)
        Console.WriteLine(GetUsage());
}

int ExecSolver(IReadOnlyList<string> args)
{
    if (args.Count != 2)
    {
        WriteError("Solver not provided");
        return 1;
    }
        
    var solverName = args[1];
    var solverType = solvers.FirstOrDefault(s => s.Namespace == $"Katas.{solverName}");

    if (solverType == null)
    {
        WriteError("Invalid solver name provided");
        return 1;
    }

    var inputFilepath = Path.Combine(
        solverName.Replace(".", "/"),
        "data.in");
    var input = File.ReadAllText(inputFilepath);
    
    var solver = (ISolver)Activator.CreateInstance(solverType)!;
    Console.WriteLine($"Running {solverName} - {solver.Name}");
    
    return solver.Solve(input);
}

void ListSolvers()
{
    Console.WriteLine("Available solvers:");
    foreach (var solver in solvers)
    {
        var name = solver.Namespace!.Substring(
            solver.Namespace!.IndexOf('.') + 1);
        Console.WriteLine($"- {name}");
    }
}
