namespace Messenger.Entities;

public class Message
{
    public Guid Id{get; private set;}
    public DateOnly RegistrationDate { get; private set; }
    public bool IsRead { get; private set; }
    public string? Content { get; private set; }
}