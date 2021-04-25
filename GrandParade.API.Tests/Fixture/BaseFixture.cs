using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace GrandParade.API.Tests.Fixture
{
    public abstract class BaseClassFixture : IClassFixture<TestServerStartUp>
    {
        private readonly HttpClient _client;

        protected BaseClassFixture(TestServerStartUp factory)
        {

            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, conf) =>
                {
                    conf.AddJsonFile("appsettings.json");
                });
            }).CreateClient(options: new WebApplicationFactoryClientOptions
            {
                BaseAddress = new System.Uri("https://localhost:44304")
            });
        }

        protected HttpClient GetClient()
        {
            return _client;
        }

    }
}   
