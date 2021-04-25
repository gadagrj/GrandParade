using AspNetCoreRateLimit;
using Convey;
using Convey.Docs.Swagger;
using Convey.Persistence.MongoDB;
using Convey.WebApi;
using GrandParade.Registration.DTO;
using GrandParade.Registration.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace GrandParade.Registration
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            await host.SeedClientRateLimit();
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices((webHostBuilderContext, services) =>
                    {
                        IConfiguration configuration = webHostBuilderContext.Configuration;

                        services
                            .AddRateLimiter(configuration)
                            .AddConvey()
                            .AddSwaggerDocs()
                            .AddServices()
                            .AddMongo()
                            .AddMongoRepository<RegistrationBaseDTO, Guid>("Registrations")
                            .AddWebApi()
                            .Build();
                    })
                    .Configure(app => app
                        .UseRouting()
                        .UseClientRateLimiting()
                        .UseEndpoints(r => r.MapControllers())
                        .UseSwaggerDocs()
                        .UseClientRateLimiting()
                    );
            });
        }
    }
}
