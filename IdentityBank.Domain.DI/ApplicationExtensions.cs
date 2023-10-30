using ExceptionHandler;
using IdentityBank.Application.Interfaces;
using IdentityBank.Application.Services;
using IdentityBank.Domain.Entities;
using IdentityBank.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityBank.Domain.DI;

public static class ApplicationExtensions
{
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<AppUser>()
            .AddRoles<IdentityRole<Guid>>()
            .AddUserManager<UserManager<AppUser>>()
            .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();

        return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddException(AppDomain.CurrentDomain.GetAssemblies());
        
        return services;
    }
}