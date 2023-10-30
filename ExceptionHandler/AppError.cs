namespace ExceptionHandler;

public class AppError
{
    public AppError(string? property, string message)
    {
        Property = property;
        Message = message;
    }

    public string? Property { get; private set; }
    
    public string Message { get; set; }
}