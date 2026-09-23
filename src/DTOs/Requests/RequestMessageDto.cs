using Messenger.Entities;
using Messenger.Entities.Operations.Interfaces;

namespace Messenger.DTOs.Requests;

public record RequestMessageDto
{
    //public IOperation<Chat> Operation { get; set; }//TODO: to decide whether to implement this for DI
    public Guid ChatId{ get; init; }
    public Guid SenderId { get; init; }
    public required string Content { get; init; }
};