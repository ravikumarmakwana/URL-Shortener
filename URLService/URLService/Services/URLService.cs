using AutoMapper;
using URLService.Core;
using URLService.Models;
using URLService.Entities;
using URLService.Repositories;

namespace URLService.Services
{
    public class URLService : IURLService
    {
        private readonly IURLRepository _urlRepository;
        private readonly IAnalyticsService _analyticsService;
        private readonly IURLShortenerAlgorithm _shortenerAlgorithm;
        private readonly IMapper _mapper;

        public URLService(IURLRepository urlRepository, IAnalyticsService analyticsService, IURLShortenerAlgorithm urlShortenerAlgorithm, IMapper mapper)
        {
            _urlRepository = urlRepository;
            _analyticsService = analyticsService;
            _shortenerAlgorithm = urlShortenerAlgorithm;
            _mapper = mapper;
        }

        public async Task<string> AccessShortenURLAsync(string shortenURL)
        {
            URL url = await _urlRepository.AccessShortenURLAsync(shortenURL);
            await _analyticsService.AccessAsync(url.Id);
            return url.LongURL;
        }

        public async Task<ShortenURL> CreateShortenURLAsync(URLShortenRequest uRLShortenRequest, UserClaims userClaims)
        {
            uRLShortenRequest.CreatedOn = DateTime.Now;
            uRLShortenRequest.ExpiredOn = DateTime.Now.AddMonths(6);
            uRLShortenRequest.UserId = userClaims.UserId;

            URL url = await _urlRepository.CreateAsync(_mapper.Map<URL>(uRLShortenRequest));
            url.ShortenPath = _shortenerAlgorithm.EncodeBase62(url.Id);
            await _urlRepository.UpdateAsync(url);

            return _mapper.Map<ShortenURL>(url);
        }

        public async Task<List<ShortenURL>> GetShortenURLsByUserIdAsync(int userId)
        {
            return _mapper.Map<List<ShortenURL>>(await _urlRepository.GetByUserIdAsync(userId));
        }

        public async Task UpdateURLExpireTimeAsync(int urlId, DateTime expireTime)
        {
            URL url = await _urlRepository.GetByIdAsync(urlId);
            url.ExpiredOn = expireTime;
            await _urlRepository.UpdateAsync(url);
        }
    }
}
