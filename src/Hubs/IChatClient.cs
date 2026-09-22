namespace Messenger.Hubs;

public interface IChatClient<T>
{
    public Task SendMessage(T entity);
    public Task<T> ReceiveMessage(T entity);
}