using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Services;

namespace UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticateController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest authenticationRequest)
        {
            try
            {
                return Ok(await _userService.AuthenticateAsync(authenticationRequest));
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("RefreshToken")]
        public async Task<ActionResult<TokenResponse>> GenerateAccessTokenAsync(TokenRequest tokenRequest)
        {
            return Ok(await _userService.GetAccessTokenAsync(tokenRequest.RefreshToken));
        }
    }
}
