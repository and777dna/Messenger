namespace Messenger.Integrations.PushNotifications;

public interface IPushNotificationService
{
    Task SendAsync(Guid userId, string title, string body);
}
