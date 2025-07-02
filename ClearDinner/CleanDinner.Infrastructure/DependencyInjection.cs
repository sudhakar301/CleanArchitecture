

using CleanDinner.Application.Common.Authentication;
using CleanDinner.Infrastructure.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanDinner.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraStructure(this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }
    }
}
