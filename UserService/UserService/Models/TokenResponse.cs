﻿namespace UserService.Models
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
