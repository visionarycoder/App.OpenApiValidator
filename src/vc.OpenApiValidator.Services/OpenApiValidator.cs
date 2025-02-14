using Microsoft.OpenApi.Readers;
using vc.OpenApiValidator.Contracts;

namespace vc.OpenApiValidator;

public class OpenApiValidator : IOpenApiValidator
{

    public async Task<ValidationResult> Validate(string filePath)
    {
    
        var result = new ValidationResult();
        var fi = new FileInfo(filePath);
        if (!fi.Exists)
        {
            result.ErrorMessages.Add($"OpenAPI spec file not found: {filePath}");
            return result;
        }

        await using var stream = fi.OpenRead();
        var openApiDoc = new OpenApiStreamReader().Read(stream, out var diagnostic);

        if(openApiDoc == null || diagnostic.Errors.Count > 0)
        {
            foreach(var error in diagnostic.Errors)
            {
                result.ErrorMessages.Add(error.Message);
            }
            return result;
        }

        result.Messages.Add("OpenAPI document is valid!");
        foreach(var warning in diagnostic.Warnings.Where(i => i != null))
        {
            result.WarningMessages.Add($"{warning}");
        }
        return result;

    }

}