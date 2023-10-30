using ExceptionHandler;
using Identity.Domain.Exceptions;
using IdentityBank.Domain.Models;

namespace IdentityBankIdentityBank.API.ExceptionHandlers;

public class NotFoundExceptionHandler : IExceptionHandler<NotFoundException>
{
    public async Task ProceedAsync(HttpContext context, NotFoundException exception)
    {
        context.Response.StatusCode = exception.StatusCode;
        await context.Response.WriteAsJsonAsync(AppResponse.CreateWithOneMessage(exception));
    }
}