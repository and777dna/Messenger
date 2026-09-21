using Messenger.DTOs.Requests;
using Messenger.Entities;
using Messenger.MapperDto.Interfaces;

namespace Messenger.MapperDto.Implementations;

public class MessageMapper : IMapper<Message, RequestMessageDto>
{
    public Message ToEntity(RequestMessageDto dto)
    {
        return new Message
        {
            ChatId = dto.ChatId,
            SenderId = dto.SenderId,
            Content = dto.Content
        };
    }

    public RequestMessageDto ToDto(Message entity)
    {
        throw new NotImplementedException();
    }
}