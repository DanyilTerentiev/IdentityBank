using ExceptionHandler;

namespace Identity.Domain.Exceptions;
public class UnhandledException : Exception, IAppException
{
    public UnhandledException(IEnumerable<AppError> error)
    {
        Error = error;
    }

    public UnhandledException(string error)
    {
        Error = new[] { new AppError(null, error) };
    }
    
    public int StatusCode => 500;
    
    public IEnumerable<AppError>? Error { get; }
}