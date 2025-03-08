using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using URLService.Models;
using URLService.Services;

namespace URLService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet("{urlId}")]
        public async Task<ActionResult<List<URLAnalyticsViewModel>>> GetURLAnalyticsAsync(int urlId)
        {
            return Ok(await _analyticsService.GetURLAnalyticsAsync(urlId));
        }
    }
}
