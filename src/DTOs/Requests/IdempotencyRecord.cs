namespace Messenger.DTOs.Requests;

public record IdempotencyRecord
{
    public Guid Key { get; set; }
}