using IdentityBank.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityBank.Domain.DI;

public static class DbContextExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(builder =>
        {
            var connectionString = configuration.GetConnectionString("SqlServer");
            builder.UseSqlServer(connectionString);
        });

        return services;
    }
    
}