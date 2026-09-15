using Messenger.Entities;
using Messenger.Entities.Operations.Interfaces;

namespace Messenger.DTOs.Requests;

public record RequestMessageDto
{
    //public IOperation<Chat> Operation { get; set; }//TODO: to decide whether to implement this for DI
    public Guid ChatId{ get; }
    public Guid SenderId { get; }
    public string? Content { get; }
};