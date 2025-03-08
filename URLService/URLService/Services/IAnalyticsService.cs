using URLService.Models;

namespace URLService.Services
{
    public interface IAnalyticsService
    {
        Task AccessAsync(int urlId);
        Task<List<URLAnalyticsViewModel>> GetURLAnalyticsAsync(int urlId);
    }
}
