namespace vs.OpenApiValidator.Services.Contracts;

public interface IOpenApiValidator
{
    void Validate(string filePath, bool showWarnings);
}