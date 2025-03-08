namespace APIGateway.Core
{
    public interface IApplicationConfiguration
    {
        string ValidAudience { get; }
        string ValidIssuer { get; }
        byte[] Secret { get; }
        double TokenValidityInMinutes { get; }
        double RefreshTokenValidityInMinutes { get; }
        string UserServiceAPI { get; }
        string URLServiceAPI { get; }

        string ValidAudienceMicroservice { get; }
        string ValidIssuerMicroservice { get; }
        byte[] SecretMicroservice { get; }
        double TokenValidityInMinutesMicroservice { get; }
        double RefreshTokenValidityInMinutesMicroservice { get; }
    }
}
