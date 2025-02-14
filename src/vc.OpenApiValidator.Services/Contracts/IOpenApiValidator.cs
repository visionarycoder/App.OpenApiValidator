namespace vc.OpenApiValidator.Contracts;

public interface IOpenApiValidator
{
    Task<ValidationResult> Validate(string filePath);
}