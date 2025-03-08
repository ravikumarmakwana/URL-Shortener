using System.Text;

namespace URLService.Core
{
    public class URLShortenerAlgorithm : IURLShortenerAlgorithm
    {
        private const int _counter = 100000;
        private const string _base62Char = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public string EncodeBase62(int num)
        {
            num += _counter;
            StringBuilder sb = new();
            while (num > 0)
            {
                sb.Append(_base62Char[num % 62]);
                num /= 62;
            }
            return sb.ToString();
        }
    }
}
