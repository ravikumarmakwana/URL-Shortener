namespace URLService.Core
{
    public interface IURLShortenerAlgorithm
    {
        string EncodeBase62(int num);
    }
}
