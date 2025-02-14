namespace vc.OpenApiValidator.Contracts;

public class ValidationResult
{

    public ICollection<string> Messages { get; } = new List<string>();

    public bool HasErrors => ErrorMessages.Count == 0;
    public ICollection<string> ErrorMessages { get; } = new List<string>();

    public bool HasWarnings => WarningMessages.Count == 0;
    public ICollection<string> WarningMessages { get; } = new List<string>();

}