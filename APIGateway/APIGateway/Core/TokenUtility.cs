using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APIGateway.Core
{
    public static class TokenUtility
    {
        public static string GenerateAccessToken(string userId, IApplicationConfiguration applicationConfiguration)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    applicationConfiguration.SecretMicroservice),
                    SecurityAlgorithms.HmacSha256
                    );

            var securityToken =
                new JwtSecurityToken(
                    applicationConfiguration.ValidIssuerMicroservice,
                    applicationConfiguration.ValidAudienceMicroservice,
                    claims,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(applicationConfiguration.TokenValidityInMinutesMicroservice),
                    signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
