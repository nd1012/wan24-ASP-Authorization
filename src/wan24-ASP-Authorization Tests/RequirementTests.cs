using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using wan24_ASP_Authorization_Integration_Tests;

namespace wan24_ASP_Authorization_Tests
{
    public class RequirementTests : IClassFixture<WebApplicationFactory<Program>>, IDisposable
    {
        private readonly HttpClient Client;

        public RequirementTests(WebApplicationFactory<Program> factory)
            => Client = factory.CreateClient(new WebApplicationFactoryClientOptions()
            {
                AllowAutoRedirect = false
            });

        [Fact]
        public async Task AuthorizedAsync()
        {
            using HttpResponseMessage response = await Client.GetAsync($"/Test/authorized");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task UnauthorizedAsync()
        {
            using HttpResponseMessage response = await Client.GetAsync($"/Test/unauthorized");
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        ~RequirementTests() => Client.Dispose();

        void IDisposable.Dispose()
        {
            Client.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
