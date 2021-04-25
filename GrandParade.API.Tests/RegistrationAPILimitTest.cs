using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GrandParade.API.Tests.Fixture;
using Xunit;

namespace GrandParade.API.Tests
{
    public class RegistrationAPILimitTest :BaseClassFixture
    {
        private const string apiPath = "/api/registration";

        public RegistrationAPILimitTest(TestServerStartUp factory) : base(factory)
        {
        }

        [Fact]
        public async Task If_User_Makes_More_request_then_throttle_For_X_Seconds()
        {
            // Arrange
            var clientId = "testClient";

            int responseStatusCode = 0;

            // Act    
            for (int i = 0; i < 4; i++)
            {
                var request = new HttpRequestMessage(new HttpMethod("GET"), apiPath);
                request.Headers.Add("X-ClientId", clientId);

                var response = await GetClient().SendAsync(request);
                responseStatusCode = (int)response.StatusCode;
            }

            // Assert
            Assert.Equal(429, responseStatusCode);
        }

    }
}
