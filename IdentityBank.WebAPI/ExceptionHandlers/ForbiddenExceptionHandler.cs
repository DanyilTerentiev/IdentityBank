using ExceptionHandler;
using IdentityBank.Domain.Exceptions;
using IdentityBank.Domain.Models;

namespace IdentityBankIdentityBank.API.ExceptionHandlers;

public class ForbiddenExceptionHandler : IExceptionHandler<ForbiddenException>
{
    public async Task ProceedAsync(HttpContext context, ForbiddenException exception)
    {
        context.Response.StatusCode = 403;
        await context.Response.WriteAsJsonAsync(AppResponse.CreateWithOneMessage(exception));
    }
}