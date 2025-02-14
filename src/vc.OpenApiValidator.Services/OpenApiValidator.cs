using Microsoft.OpenApi.Readers;

using vs.OpenApiValidator.Services.Contracts;

namespace vs.OpenApiValidator.Services;

public class OpenApiValidator(IConsole console) : IOpenApiValidator
{

    public void Validate(string filePath, bool showWarnings)
    {
    
        using var stream = File.OpenRead(filePath);
        var openApiDoc = new OpenApiStreamReader().Read(stream, out var diagnostic);

        if(openApiDoc == null || diagnostic.Errors.Count > 0)
        {

            console.WriteLine("OpenAPI document has errors:");
            foreach(var error in diagnostic.Errors)
            {
                console.WriteLine($"- {error.Message}");
            }
            throw new InvalidOperationException("OpenAPI spec is invalid.");

        }

        console.WriteLine("OpenAPI document is valid!");

        if(showWarnings && diagnostic.Warnings.Count > 0)
        {

            console.WriteLine("\nWarnings:");
            foreach(var warning in diagnostic.Warnings)
            {
                console.WriteLine($"- {warning}");
            }

        }

    }

}