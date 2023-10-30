using System.Reflection;
using IdentityBank.Application.Services;
using IdentityBank.Application.Services.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityBank.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        return services.AddAutoMapper(typeof(AuthService));
    }

    public static IServiceCollection AddMediatr(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(SignInRequestHandler))!));
    }
}