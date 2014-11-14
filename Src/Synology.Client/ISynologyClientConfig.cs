namespace SynologyClient
{
    public interface ISynologyClientConfig
    {
        string ApiBaseAddressAndPathNoTrailingSlash { get; }

        string User { get; }

        string Password { get; }
    }
}