using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vc.Ifx;

public static class ConsoleHelper
{

    public static (bool printUsage, string filePath, bool showWarnings) TryParseArguments(string[] args)
    {

        var specPath = string.Empty;
        var showWarnings = false;
        var printUsage = false;

        // If no arguments, or user wants help
        if(args.Length == 0)
        {
            printUsage = true;
        }
        else
        {

            // We’ll loop through args and check for flags
            for(var i = 0; i < args.Length; i++)
            {
                var arg = args[i].Trim();
                // Check if it's a flag
                if(arg.Equals("-f", StringComparison.OrdinalIgnoreCase) || arg.Equals("--file", StringComparison.OrdinalIgnoreCase))
                {
                    if(i + 1 >= args.Length)
                    {
                        Console.Error.WriteLine("Error: Missing file path after '-f'/'--file'.");
                        break;
                    }
                    specPath = args[++i];
                }
                else if(arg.Equals("-w", StringComparison.OrdinalIgnoreCase) || arg.Equals("--show-warnings", StringComparison.OrdinalIgnoreCase))
                {
                    showWarnings = true;
                }
                else
                {
                    // If it's not a recognized flag, assume it's the file path
                    // (if user just typed: OpenApiValidator.exe myspec.yaml)
                    if(string.IsNullOrEmpty(specPath))
                    {
                        specPath = arg;
                    }
                    else
                    {
                        // We already have a file path, so this is unexpected
                        Console.Error.WriteLine($"Unrecognized argument: {arg}");
                    }
                }
            }

            // At minimum, we need a spec file
            if(string.IsNullOrEmpty(specPath))
            {
                Console.Error.WriteLine("Error: No OpenAPI file path was provided.");
            }
        }

        return (printUsage, specPath, showWarnings);

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

