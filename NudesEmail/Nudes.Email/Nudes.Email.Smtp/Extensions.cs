using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Nudes.Email.Smtp
{
    public static class Extensions
    {
        // TODO: Add smtpEmailConfiguration is null
        public static IServiceCollection AddSmtpEmailConfiguration(this IServiceCollection services, Func<IServiceProvider, SmtpEmailServiceConfiguration> config) => services
            .AddTransient<IEmailService, SmtpEmailService>()
            .AddTransient(config);
    }
}
