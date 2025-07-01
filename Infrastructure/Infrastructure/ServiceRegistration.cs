using Application.Abstractions.Services;
using Application.Abstractions.Token;
using Application.Settings;
using Infrastructure.Services;
using Infrastructure.Services.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<ISmsService, TwilloSmsService>();
            services.AddTransient<IOtpService, OtpService>();
            services.AddOptions<TwilioSettings>().Bind(configuration.GetSection("Twilio"));
        }
    }
}
