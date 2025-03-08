﻿namespace UserService.Core
{
    public interface IApplicationConfiguration
    {
        string ValidAudience { get; }
        string ValidIssuer { get; }
        byte[] Secret { get; }
        double TokenValidityInMinutes { get; }
        double RefreshTokenValidityInMinutes { get; }
    }
}
