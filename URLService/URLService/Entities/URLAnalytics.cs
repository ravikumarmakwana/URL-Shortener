using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace URLService.Entities
{
    public class URLAnalytics
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(URL))]
        public int URLId { get; set; }
        public URL URL { get; set; }

        public DateTime AccessedAt { get; set; }
    }
}
