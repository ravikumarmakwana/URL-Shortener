namespace URLService.Core
{
    public interface IApplicationConfiguration
    {
        string ValidAudience { get; }
        string ValidIssuer { get; }
        byte[] Secret { get; }
    }
}
