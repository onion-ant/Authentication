using Authentication.Infrastructure.Interfaces;
using Authentication.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Infrastructure.Extensions;
public static class ServicesExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connString)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connString);
        });
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}
