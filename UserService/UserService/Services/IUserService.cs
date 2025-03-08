using UserService.Models;

namespace UserService.Services
{
    public interface IUserService
    {
        Task<UserRegistrationResponse> RegisterAsync(UserRegistrationRequest userRegistrationRequest);
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest authenticationRequest);
        Task<TokenResponse> GetAccessTokenAsync(string refreshToken);
    }
}
