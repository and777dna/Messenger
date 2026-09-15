using Messenger.DTOs.Requests;
using Messenger.Entities;
using Messenger.Services.MapperDto.Interfaces;

namespace Messenger.Services.MapperDto.Implementations;

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
}