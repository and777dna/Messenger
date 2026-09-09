namespace Messenger.Messaging;

public interface IEventPublisher
{
    Task PublishAsync<T>(string queue, T @event);
}
