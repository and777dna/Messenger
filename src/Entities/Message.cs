namespace Messenger.Entities;

public class Message
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateOnly SendDate { get; private set; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public bool IsRead { get; private set; }
    public string? Content { get; set; }
    
    public Guid ChatId{ get; set; }
    public Chat Chat { get; }
    public Guid SenderId{ get; set; }
    public User Sender { get; }
    public void MakeRead()
    {
        this.IsRead = true;
    }
}