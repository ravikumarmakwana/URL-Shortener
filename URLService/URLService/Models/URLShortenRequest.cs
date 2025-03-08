namespace URLService.Models
{
    public class URLShortenRequest
    {
        public int UserId { get; set; }
        public string LongURL { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime ExpiredOn { get; set; }
    }
}
