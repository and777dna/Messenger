namespace Messenger.Hubs;

public interface IChatClient<T>
{
    public Task ReceiveMessage(T entity);
}