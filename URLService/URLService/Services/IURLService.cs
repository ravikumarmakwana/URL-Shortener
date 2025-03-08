using URLService.Models;

namespace URLService.Services
{
    public interface IURLService
    {
        Task<ShortenURL> CreateShortenURLAsync(URLShortenRequest uRLShortenRequest, UserClaims userClaims);
        Task<List<ShortenURL>> GetShortenURLsByUserIdAsync(int userId);
        Task UpdateURLExpireTimeAsync(int urlId, DateTime expireTime);
        Task<string> AccessShortenURLAsync(string shortenURL);
    }
}
