using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreRateLimit;
using GrandParade.Registration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace GrandParade.API.Tests.Fixture
{
    public class TestServerStartUp : WebApplicationFactory<Program>
    {
        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            var server = base.CreateServer(builder);

            using (var scope = server.Host.Services.CreateScope())
            {
                // get the ClientPolicyStore instance
                var clientPolicyStore = scope.ServiceProvider.GetRequiredService<IClientPolicyStore>();

                // seed Client data from appsettings
                clientPolicyStore.SeedAsync().Wait();

                // get the IpPolicyStore instance
                var ipPolicyStore = scope.ServiceProvider.GetRequiredService<IIpPolicyStore>();

                // seed IP data from appsettings
                ipPolicyStore.SeedAsync().Wait();
            }

            return server;
        }
    }
}
