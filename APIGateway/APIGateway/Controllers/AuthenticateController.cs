using APIGateway.Core;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace APIGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IApplicationConfiguration _applicationConfiguration;

        public AuthenticateController(HttpClient httpClient, IApplicationConfiguration applicationConfiguration)
        {
            _httpClient = httpClient;
            _applicationConfiguration = applicationConfiguration;
        }

        [HttpPost]
        public async Task<ActionResult> AuthenticateAsync([FromBody] object requestBody)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_applicationConfiguration.UserServiceAPI + "/Authenticate"),
                Content = new StringContent
                (
                    System.Text.Json.JsonSerializer.Serialize(requestBody),
                    Encoding.UTF8,
                    "application/json"
                )
            };

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return Content(content, "application/json");
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult> GenerateAccessTokenAsync([FromBody] object requestBody)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_applicationConfiguration.UserServiceAPI + "/Authenticate/RefreshToken"),
                Content = new StringContent
                (
                    System.Text.Json.JsonSerializer.Serialize(requestBody),
                    Encoding.UTF8,
                    "application/json"
                )
            };

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            return Content(content, "application/json");
        }
    }
}
