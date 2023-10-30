using ExceptionHandler;

namespace Identity.Domain.Exceptions;

public class ValidationException : Exception, IAppException
{
    public ValidationException(IEnumerable<AppError> error)
    {
        Error = error;
    }

    public ValidationException(string error)
    {
        Error = new[] { new AppError(null, error) };
    }
    
    public int StatusCode => 400;
    
    public IEnumerable<AppError>? Error { get; }
}