namespace ExceptionHandler;

public interface IAppException
{
    int StatusCode { get; }
    
    IEnumerable<AppError>? Error { get; }
}