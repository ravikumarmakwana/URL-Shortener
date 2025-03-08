using AutoMapper;
using URLService.Entities;
using URLService.Models;
using URLService.Repositories;

namespace URLService.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IAnalyticsRepository _analyticsRepository;
        private readonly IMapper _mapper;

        public AnalyticsService(IAnalyticsRepository analyticsRepository, IMapper mapper)
        {
            _analyticsRepository = analyticsRepository;
            _mapper = mapper;
        }

        public async Task AccessAsync(int urlId)
        {
            await _analyticsRepository.AddAsync(new URLAnalytics
            {
                URLId = urlId,
                AccessedAt = DateTime.UtcNow
            });
        }

        public async Task<List<URLAnalyticsViewModel>> GetURLAnalyticsAsync(int urlId)
        {
            return _mapper.Map<List<URLAnalyticsViewModel>>(
                await _analyticsRepository.GetURLAnalyticsAsync(urlId)
                );
        }
    }
}
