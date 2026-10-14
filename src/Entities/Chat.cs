namespace Messenger.Entities;

public class Chat
{
    public Guid Id { get; private init; } = Guid.NewGuid();
    public bool IsPrivate { get; private set; }
    public Guid IdempotencyKey { get; set; }
    public ICollection<User> Members { get; private set; } = new List<User>();
    public ICollection<Message> Messages { get; private set; } = new List<Message>();

    public void AddMessage(Message message)
    {
        //Messages.Append(message);////TODO: to decide whether to implement this for DI
    }
}