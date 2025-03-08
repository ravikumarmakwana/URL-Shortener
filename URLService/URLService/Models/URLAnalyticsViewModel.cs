using System.ComponentModel.DataAnnotations.Schema;
using URLService.Entities;

namespace URLService.Models
{
    public class URLAnalyticsViewModel
    {
        public int Id { get; set; }
        public int URLId { get; set; }
        public string LongURL { get; set; }
        public string ShortenURL { get; set; }
        public DateTime AccessedAt { get; set; }
    }
}
