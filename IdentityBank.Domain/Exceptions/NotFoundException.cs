using ExceptionHandler;

namespace Identity.Domain.Exceptions;

public class NotFoundException : Exception, IAppException
{
    public NotFoundException(IEnumerable<AppError> error)
    {
        Error = error;
    }

    public NotFoundException(string error)
    {
        Error = new[] { new AppError(null, error) };
    }
    
    public int StatusCode => 404;
    
    public IEnumerable<AppError>? Error { get; }
}