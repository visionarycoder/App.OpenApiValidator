namespace vc.OpenApiValidator.Contracts;

public interface IClient
{
    Task Run(string[] args);
}