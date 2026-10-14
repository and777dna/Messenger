namespace Messenger.Entities;

public record IdempotencyRecord()
{
    public Guid Key { get; set; }
    public string RequestHash { get; set; } = null!;
};