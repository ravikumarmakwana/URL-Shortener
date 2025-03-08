using APIGateway.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace APIGateway.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class URLsController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IApplicationConfiguration _applicationConfiguration;

        public URLsController(HttpClient httpClient, IApplicationConfiguration applicationConfiguration)
        {
            _httpClient = httpClient;
            _applicationConfiguration = applicationConfiguration;
        }

        [HttpPost("Shorten")]
        public async Task<ActionResult> CreateShortenURLAsync([FromBody] object requestBody)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_applicationConfiguration.URLServiceAPI + "/URLs/Shorten"),
                Content = new StringContent
                (
                    System.Text.Json.JsonSerializer.Serialize(requestBody),
                    Encoding.UTF8,
                    "application/json"
                )
            };

            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                TokenUtility.GenerateAccessToken(
                    HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty,
                    _applicationConfiguration)
                );

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        [HttpGet]
        public async Task<ActionResult> GetShortenURLsAsync()
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_applicationConfiguration.URLServiceAPI + "/URLs")
            };

            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                TokenUtility.GenerateAccessToken(
                    HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty,
                    _applicationConfiguration)
                );

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }

        [AllowAnonymous]
        [HttpGet("Access")]
        public async Task<ActionResult<string>> AccessShortenURLAsync(string shorten)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_applicationConfiguration.URLServiceAPI + $"/URLs/Access?shorten={shorten}")
            };
            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }
    }
}
