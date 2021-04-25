using System.Threading.Tasks;
using AspNetCoreRateLimit;
using Convey;
using GrandParade.Registration.Interface;
using GrandParade.Registration.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GrandParade.Registration.Extensions
{
    public static class ServiceCollections
    {
        public static IConveyBuilder AddServices(this IConveyBuilder builder)
        {
            builder.Services.AddScoped<IRegistration, RegistrationService>();
            return builder;
        }

        public static IServiceCollection AddRateLimiter(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddOptions();
            services.AddMemoryCache();
            services.Configure<ClientRateLimitOptions>(Configuration.GetSection("ClientRateLimiting"));

            //load client rules from appsettings.json
            services.Configure<ClientRateLimitPolicies>(Configuration.GetSection("ClientRateLimitPolicies"));

            // inject counter and rules stores
            services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // configuration (resolvers, counter key builders)
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();


            
            return services;
        }

        public static async Task SeedClientRateLimit(this IHost webHost)
        {
            using var scope = webHost.Services.CreateScope();
            // get the ClientPolicyStore instance
            var clientPolicyStore = scope.ServiceProvider.GetRequiredService<IClientPolicyStore>();

            // seed Client data from appsettings
            await clientPolicyStore.SeedAsync();

        }
    }
}

