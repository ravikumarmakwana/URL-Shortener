using System.Text;

namespace APIGateway.Core
{
    public class ApplicationConfiguration : IApplicationConfiguration
    {
        private readonly IConfiguration _configuration;

        public ApplicationConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ValidAudience => _configuration.GetSection("JWT:ValidAudience").Value;

        public string ValidIssuer => _configuration.GetSection("JWT:ValidIssuer").Value;

        public byte[] Secret => Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Secret").Value);

        public double TokenValidityInMinutes
            => Convert.ToDouble(_configuration.GetSection("JWT:TokenValidityInMinutes").Value);

        public double RefreshTokenValidityInMinutes
            => Convert.ToDouble(_configuration.GetSection("JWT:RefreshTokenValidityInMinutes").Value);

        public string UserServiceAPI => _configuration.GetValue<string>("UserServiceAPI") ?? string.Empty;

        public string URLServiceAPI => _configuration.GetValue<string>("URLServiceAPI") ?? string.Empty;

        public string ValidAudienceMicroservice => _configuration.GetSection("URLServiceJWT:ValidAudience").Value;

        public string ValidIssuerMicroservice => _configuration.GetSection("URLServiceJWT:ValidIssuer").Value;

        public byte[] SecretMicroservice => Encoding.UTF8.GetBytes(_configuration.GetSection("URLServiceJWT:Secret").Value);

        public double TokenValidityInMinutesMicroservice
            => Convert.ToDouble(_configuration.GetSection("URLServiceJWT:TokenValidityInMinutes").Value);

        public double RefreshTokenValidityInMinutesMicroservice
            => Convert.ToDouble(_configuration.GetSection("URLServiceJWT:RefreshTokenValidityInMinutes").Value);
    }
}
