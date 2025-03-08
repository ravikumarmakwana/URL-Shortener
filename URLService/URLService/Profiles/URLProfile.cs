using AutoMapper;
using URLService.Entities;
using URLService.Models;

namespace URLService.Profiles
{
    public class URLProfile : Profile
    {
        public URLProfile()
        {
            CreateMap<URL, ShortenURL>();
            CreateMap<URLShortenRequest, URL>();
            CreateMap<URLAnalytics, URLAnalyticsViewModel>()
                .ForMember(dst => dst.LongURL, opt => opt.MapFrom(src => src.URL.LongURL))
                .ForMember(dst => dst.ShortenURL, opt => opt.MapFrom(src => src.URL.ShortenPath));
        }
    }
}
