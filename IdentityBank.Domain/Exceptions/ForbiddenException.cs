using ExceptionHandler;

namespace IdentityBank.Domain.Exceptions;

public class ForbiddenException : Exception, IAppException
{
    public ForbiddenException(IEnumerable<AppError>? error)
    {
        Error = error;
    }

    public int StatusCode => 403;
    
    public IEnumerable<AppError>? Error { get; }
}