namespace URLService.Models
{
    public class ShortenURL
    {
        public int Id { get; set; }
        public string LongURL { get; set; }
        public string ShortenPath { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpiredOn { get; set; }
    }
}
