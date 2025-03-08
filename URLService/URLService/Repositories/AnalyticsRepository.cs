using Microsoft.EntityFrameworkCore;
using URLService.Contexts;
using URLService.Entities;

namespace URLService.Repositories
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly ApplicationDbContext _context;

        public AnalyticsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(URLAnalytics urlAnalytics)
        {
            await _context.URLAnalytics.AddAsync(urlAnalytics);
            await _context.SaveChangesAsync();
        }

        public async Task<List<URLAnalytics>> GetURLAnalyticsAsync(int urlId)
        {
            return await _context.URLAnalytics
                .Where(s => s.URLId == urlId)
                .Include(s => s.URL)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
