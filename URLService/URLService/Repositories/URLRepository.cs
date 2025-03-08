using Microsoft.EntityFrameworkCore;
using URLService.Contexts;
using URLService.Entities;

namespace URLService.Repositories
{
    public class URLRepository : IURLRepository
    {
        private readonly ApplicationDbContext _context;

        public URLRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<URL> AccessShortenURLAsync(string shortenURL)
        {
            return await _context.URLs.FirstOrDefaultAsync(s => s.ShortenPath == shortenURL);
        }

        public async Task<URL> CreateAsync(URL url)
        {
            await _context.URLs.AddAsync(url);
            await _context.SaveChangesAsync();
            return url;
        }

        public async Task<URL> GetByIdAsync(int id)
        {
            return await _context.URLs.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<URL>> GetByUserIdAsync(int userId)
        {
            return await _context.URLs.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task UpdateAsync(URL url)
        {
            _context.URLs.Update(url);
            await _context.SaveChangesAsync();
        }
    }
}
