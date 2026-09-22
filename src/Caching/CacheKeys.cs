namespace Messenger.Caching;

public static class CacheKeys
{
    public static string ChatMessages(Guid chatId) => $"chat:{chatId}:messages";
}