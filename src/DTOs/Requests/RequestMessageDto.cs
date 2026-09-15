namespace Messenger.DTOs.Requests;

public record RequestMessageDto
{
    //public IOperation<>
    public Guid ChatId{ get; }
    public Guid SenderId { get; }
    public string? Content { get; }
};