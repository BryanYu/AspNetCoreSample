using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class HttpClientController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public HttpClientController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [Route("")]
        public async Task<string> Get()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://api.github.com/repos/aspnet/AspNetCore.Docs/branches");
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return string.Empty;
        }
    }
}