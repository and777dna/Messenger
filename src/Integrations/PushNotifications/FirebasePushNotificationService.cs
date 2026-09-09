namespace Messenger.Integrations.PushNotifications;

public class FirebasePushNotificationService : IPushNotificationService
{
    public Task SendAsync(Guid userId, string title, string body) =>
        throw new NotImplementedException();
}
