using URLService.Entities;

namespace URLService.Repositories
{
    public interface IAnalyticsRepository
    {
        Task AddAsync(URLAnalytics urlAnalytics);
        Task<List<URLAnalytics>> GetURLAnalyticsAsync(int urlId);
    }
}
