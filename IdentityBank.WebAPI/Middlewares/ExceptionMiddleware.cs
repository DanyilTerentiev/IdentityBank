using ExceptionHandler;

namespace IdentityBankIdentityBank.API.Middlewares;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception e)
        {
            var handlerType = typeof(IExceptionHandler<>).MakeGenericType(e.GetType());
            var handler = context.RequestServices.GetService(handlerType) ?? context.RequestServices.GetService(typeof(IExceptionHandler<>).MakeGenericType(typeof(Exception)));
            
            if (handler != null)
            {
                var method = handler.GetType().GetMethod("ProceedAsync") ?? 
                             throw new InvalidOperationException($"Method ProceedAsync not found.");
                
                var task = (Task)method.Invoke(handler, new object[]{context, e})!;
                await task;
            }
        }
    }
}

public static class Exceptions
{
    public static IApplicationBuilder UseExceptions(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    } 
}
