using vc.Ifx;
using vc.OpenApiValidator.Contracts;

namespace vc.OpenApiValidator;

public class Client(IOpenApiValidator validator) : IClient
{

    public async Task Run(string[] args)
    {

        try
        {

            var runtimeArguments = ConsoleHelper.TryParseArguments(args);
            if(runtimeArguments.PrintUsage)
            {
                ConsoleHelper.PrintUsage();
                return;
            }

            var fi = new FileInfo(runtimeArguments.FilePath);
            if(!fi.Exists)
            {
                await Console.Error.WriteLineAsync($"Error: File not found at '{runtimeArguments.FilePath}'.");
                return;
            }

            var validationResults = await validator.Validate(runtimeArguments.FilePath);
            if (validationResults.HasErrors)
            {
                foreach (var error in validationResults.ErrorMessages)
                {
                    await Console.Error.WriteLineAsync($"Error: {error}");
                }
            }

        }
        catch(Exception ex)
        {
            await Console.Error.WriteLineAsync($"Validation failed: {ex.Message}");
        }

    }

}