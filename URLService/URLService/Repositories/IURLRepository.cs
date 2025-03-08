using URLService.Entities;

namespace URLService.Repositories
{
    public interface IURLRepository
    {
        Task<URL> GetByIdAsync(int id);
        Task<URL> CreateAsync(URL url);
        Task UpdateAsync(URL url);
        Task<List<URL>> GetByUserIdAsync(int userId);
        Task<URL> AccessShortenURLAsync(string shortenURL);
    }
}
