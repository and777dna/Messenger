namespace Messenger.Integrations.Gif;

public interface IGifSearchService
{
    Task<IReadOnlyList<string>> SearchAsync(string query);
}
