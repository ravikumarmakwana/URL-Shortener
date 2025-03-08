using APIGateway.Core;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace APIGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IApplicationConfiguration _applicationConfiguration;

        public UsersController(HttpClient httpClient, IApplicationConfiguration applicationConfiguration)
        {
            _httpClient = httpClient;
            _applicationConfiguration = applicationConfiguration;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> RegisterAsync([FromBody] object requestBody)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_applicationConfiguration.UserServiceAPI + "/Users/Register"),
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
