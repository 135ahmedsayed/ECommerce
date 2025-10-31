namespace Ecommerce.ServiceAbstraction.Common;

public class Error
{
    public string Code { get;}
    public string Description { get;}
    public ErrorType Type { get; }
    private Error(string code, string description, ErrorType type)
    {
        Code = code;
        Description = description;
        Type = type;
    }
    public static Error Failure(string code = "General.Failure", string description = "A Failure has occurred.")
        => new Error(code, description, ErrorType.failure);
    public static Error Validation(string code = "General.Validation", string description = "A Validation error has occurred.")
        => new Error(code, description, ErrorType.validation);
    public static Error NotFound(string code = "General.NotFound", string description = "The requested resource was not found.")
        => new Error(code, description, ErrorType.notFound);
    public static Error Conflict(string code = "General.Conflict", string description = "A conflict error has occurred.")
        => new Error(code, description, ErrorType.conflict);
    public static Error Unauthorized(string code = "General.Unauthorized", string description = "An unauthorized error has occurred.")
        => new Error(code, description, ErrorType.unauthorized);
}
