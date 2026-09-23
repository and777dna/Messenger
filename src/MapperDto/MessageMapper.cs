using Messenger.DTOs.Requests;
using Messenger.Entities;
using Messenger.MapperDto.Interfaces;

namespace Messenger.MapperDto;

public class MessageMapper : IMapper<Message, RequestMessageDto>
{
    public Message ToEntity(RequestMessageDto dto)
    {
        var chatId = dto.ChatId;
        if (chatId == Guid.Empty) throw new ArgumentException("cannot be Guid.Empty", nameof(dto.ChatId));
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