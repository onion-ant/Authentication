using Authentication.Application.Interfaces;
using Authentication.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Application.Extensions;
public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IHashService, HashService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        return services;
    }
}
