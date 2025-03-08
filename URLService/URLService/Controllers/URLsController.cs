using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using URLService.Core;
using URLService.Models;
using URLService.Services;

namespace URLService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class URLsController : ControllerBase
    {
        private readonly IURLService _urlService;

        public URLsController(IURLService urlService)
        {
            _urlService = urlService;
        }

        [HttpPost("Shorten")]
        public async Task<ActionResult<ShortenURL>> CreateShortenURLAsync(URLShortenRequest request)
        {
            ShortenURL shortenURL = await _urlService.CreateShortenURLAsync(request, HttpContext.GetUserClaims());
            return Created("Shorten", shortenURL);
        }

        [HttpGet]
        public async Task<ActionResult<List<ShortenURL>>> GetShortenURLsAsync()
        {
            return Ok(await _urlService.GetShortenURLsByUserIdAsync(HttpContext.GetUserClaims().UserId));
        }

        [AllowAnonymous]
        [HttpGet("Access")]
        public async Task<ActionResult<string>> AccessShortenURLAsync(string shorten)
        {
            return Ok(await _urlService.AccessShortenURLAsync(shorten));
        }
    }
}
