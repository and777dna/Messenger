namespace Messenger.Entities;

public class Chat
{
    public Guid Id{ get; }
    public bool IsPrivate { get; }
    public IEnumerable<User> Members { get; }
    public IEnumerable<Message> Messages { get; }

    public void AddMessage(Message message)
    {
        Messages.Append(message);
    }
}