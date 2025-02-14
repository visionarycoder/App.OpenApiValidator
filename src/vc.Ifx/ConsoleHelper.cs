namespace vc.Ifx;

public static class ConsoleHelper
{

    public static RuntimeArguments TryParseArguments(string[] args)
    {

        var runtimeArguments = new RuntimeArguments();

        // If no arguments, or user wants help
        if (args.Length == 0)
        {
            runtimeArguments.PrintUsage = true;
            return runtimeArguments;
        }

        // We’ll loop through args and check for flags
        for (var i = 0; i < args.Length; i++)
        {
            var arg = args[i].Trim();
            // Check if it's a flag
            if (arg.Equals("-f", StringComparison.OrdinalIgnoreCase) || arg.Equals("--file", StringComparison.OrdinalIgnoreCase))
            {
                if (i + 1 >= args.Length)
                {
                    runtimeArguments.PrintUsage = true;
                    runtimeArguments.Errors.Add("Error: Missing file path after '-f'/'--file'.");
                    return runtimeArguments;
                }
                runtimeArguments.FilePath = args[++i];
            }
            else if (arg.Equals("-w", StringComparison.OrdinalIgnoreCase) || arg.Equals("--show-warnings", StringComparison.OrdinalIgnoreCase))
            {
                runtimeArguments.ShowWarnings = true;
            }
            else
            {
                // If it's not a recognized flag, assume it's the file path
                // (if user just typed: OpenApiValidator.exe myspec.yaml)
                if (string.IsNullOrEmpty(runtimeArguments.FilePath))
                {
                    runtimeArguments.FilePath = arg;
                }
                else
                {
                    // We already have a file path, so this is unexpected
                    runtimeArguments.Errors.Add($"Unrecognized argument: {arg}");
                }
            }
        }

        // At minimum, we need a spec file
        if (string.IsNullOrEmpty(runtimeArguments.FilePath))
        {
            runtimeArguments.Errors.Add("Error: No OpenAPI file path was provided.");
        }
        return runtimeArguments;

    }

    public static void PrintUsage()
    {
        Console.WriteLine("Usage:");
        Console.WriteLine("  OpenApiValidator.exe [options] <path-to-openapi-spec>");
        Console.WriteLine();
        Console.WriteLine("Options:");
        Console.WriteLine("  -f, --file            Path to OpenAPI spec (yaml/json).");
        Console.WriteLine("  -w, --show-warnings   Print warnings in addition to errors.");
        Console.WriteLine("  --help                Show this help message.");
        Console.WriteLine();
        Console.WriteLine("Examples:");
        Console.WriteLine("  OpenApiValidator.exe myApi.yaml");
        Console.WriteLine("  OpenApiValidator.exe -f myApi.json --show-warnings");
    }

}

public class RuntimeArguments
{
    public string FilePath { get; set; } = string.Empty;
    public bool ShowWarnings { get; set; }
    public bool PrintUsage { get; set; }
    public bool HasErrors => Errors.Count > 0;
    public ICollection<string> Errors { get; } = new List<string>();
}
