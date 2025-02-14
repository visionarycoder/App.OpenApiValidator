using vc.Ifx;
using vs.OpenApiValidator.Services.Contracts;

namespace vs.OpenApiValidator.Services;

public class Client(IOpenApiValidator validator) : IClient
{

    public int Run(string[] args)
    {

        try
        {
            var (printUsage, filePath, showWarnings) = ConsoleHelper.TryParseArguments(args);

            if(printUsage)
            {
                PrintUsage();
                return 1;
            }

            var fi = new FileInfo(filePath);
            if(!fi.Exists)
            {
                Console.Error.WriteLine($"Error: File not found at '{specPath}'.");
                return 1;
            }

            validator.Validate(specPath, showWarnings);
            return 0;

        }
        catch(Exception ex)
        {

            Console.Error.WriteLine($"Validation failed: {ex.Message}");
            return 1;

        }

    }

   

    

}