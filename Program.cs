using CodingChallenges.Lib;
using System.Reflection;

var solvers = Assembly.GetExecutingAssembly().GetTypes()
    .Where(t => t.GetInterface(typeof(ISolver).Name) != null)
    .OrderBy(t => t.Namespace)
    .ToArray();

string GetUsage()
{
    return $"""
        Usage: dotnet run [command]

        Commands:
         exec [solver.name] Run particular solver
         list               List all available solvers
        """;
}

void Die(string errorMessage, int statusCode, bool printUsage = false)
{
    Console.Error.WriteLine($"Error: {errorMessage}");
    if (printUsage)
        Console.WriteLine(GetUsage());
    Environment.Exit(statusCode);
}

void ListSolvers()
{
    Console.WriteLine("Available solvers:");
    foreach (var solver in solvers)
    {
        Console.WriteLine($"- {solver.Namespace}");
    }
}

void ExecSolver(string[] args)
{
    if (args.Length != 2)
        Die("Solver not provided", statusCode: 1);
    var solverName = args[1];
    var solver = solvers.FirstOrDefault(s => s.Namespace == solverName);

    if (solver == null)
        Die("Invalid solver name provided", statusCode: 1);
    Console.WriteLine(solver);
}

if (args.Length == 0)
    Die("Command not provided", statusCode: 1, printUsage: true);

switch(args[0])
{
    case "list":
        ListSolvers();
        break;
    case "exec":
        ExecSolver(args);
        break;
    default:
        Die("Invalid command", statusCode: 1, printUsage: true);
        break;
}

// var workDir = Path.Combine("Y2023", "Day01", "data.in");
// var inputData = File.ReadAllText(workDir);
