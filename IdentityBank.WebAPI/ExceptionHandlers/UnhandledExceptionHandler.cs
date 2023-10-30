using ExceptionHandler;
using IdentityBank.Domain.Models;

namespace IdentityBankIdentityBank.API.ExceptionHandlers;

public class UnhandledExceptionHandler : IExceptionHandler<Exception>
{
    public async Task ProceedAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(new AppResponse(500, new[] {new AppError(null, exception.Message)}));
    }
}