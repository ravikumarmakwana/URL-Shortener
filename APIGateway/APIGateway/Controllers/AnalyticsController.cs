using APIGateway.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace APIGateway.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly IApplicationConfiguration _applicationConfiguration;

        public AnalyticsController(HttpClient httpClient, IApplicationConfiguration applicationConfiguration)
        {
            _httpClient = httpClient;
            _applicationConfiguration = applicationConfiguration;
        }

        [HttpGet("{urlId}")]
        public async Task<ActionResult> GetURLAnalyticsAsync(int urlId)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_applicationConfiguration.URLServiceAPI + $"/Analytics/{urlId}")
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
    }
}
