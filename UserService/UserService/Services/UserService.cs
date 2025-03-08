using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserService.Core;
using UserService.Core.Helper;
using UserService.Entities;
using UserService.Models;
using UserService.Repositories;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationConfiguration _applicationConfiguration;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        
        public UserService(IApplicationConfiguration applicationConfiguration, IUserRepository userRepository, 
            UserManager<User> userManager, IMapper mapper)
        {
            _applicationConfiguration = applicationConfiguration;
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest authenticationRequest)
        {
            var user = await _userManager.FindByNameAsync(authenticationRequest.UserName);
            await ValidateUserCredentials(authenticationRequest, user);

            var accessToken = await GenerateAccessTokenAsync(user);

            var authenticationResponse = _mapper.Map<AuthenticationResponse>(user);
            authenticationResponse.AccessToken = accessToken;

            return authenticationResponse;
        }

        public async Task<TokenResponse> GetAccessTokenAsync(string refreshToken)
        {
            var user = await _userRepository.GetByUserByRefreshToken(refreshToken);
            if (user == null)
            {
                throw new InvalidOperationException($"Refresh Token is Expired.");
            }

            var accessToken = await GenerateAccessTokenAsync(user);

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
            };
        }

        private async Task ValidateUserCredentials(AuthenticationRequest authenticationRequest, User user)
        {
            if (user == null)
            {
                throw new InvalidOperationException($"User not exists for given UserName: {authenticationRequest.UserName}");
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, authenticationRequest.Password);
            if (!isPasswordCorrect)
            {
                throw new InvalidOperationException("Invalid Password");
            }
        }

        private async Task<string> GenerateAccessTokenAsync(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var claims = TokenUtility.GetUserClaims(user, userRoles);
            var accessToken = TokenUtility.GenerateAccessToken(claims, _applicationConfiguration);
            await SaveRefreshTokenAsync(user);

            return accessToken;
        }

        private async Task SaveRefreshTokenAsync(User user)
        {
            user.RefreshToken = TokenUtility.GenerateRefreshToken(user);
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(_applicationConfiguration.RefreshTokenValidityInMinutes);
            await _userManager.UpdateAsync(user);
        }

        public async Task<UserRegistrationResponse> RegisterAsync(UserRegistrationRequest userRegistrationRequest)
        {
            User userExists = await _userManager.FindByNameAsync(userRegistrationRequest.UserName);
            if (userExists != null)
            {
                throw new InvalidOperationException("User already exists with give Email Address.");
            }

            User user = _mapper.Map<User>(userRegistrationRequest);
            var result = await _userManager.CreateAsync(user, userRegistrationRequest.Password);
            var response = _mapper.Map<UserRegistrationResponse>(user);

            response.Message =
                result.Succeeded ? "Registration Successful" : "Registration Failed";
            return response;
        }
    }
}
