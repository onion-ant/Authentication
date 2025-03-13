using Authentication.Application.Interfaces;
using Authentication.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Application.Extensions;
public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IHashService, HashService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        var senderEmail = configuration["Email:SenderEmail"];
        var senderName = configuration["Email:SenderName"];
        var smtpHost = configuration["Email:Host"];
        var smtpPort = configuration["Email:Port"];
        var username = configuration["Email:Username"];
        var password = configuration["Email:Password"];
        services.AddFluentEmail(senderEmail, senderName)
            .AddSmtpSender(smtpHost, int.Parse(smtpPort), username, password);
        return services;
    }
}
