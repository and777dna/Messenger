namespace Messenger.Integrations.Gif;

public class GiphyClient(HttpClient httpClient) : IGifSearchService
{
    public Task<IReadOnlyList<string>> SearchAsync(string query) =>
        throw new NotImplementedException();
}
