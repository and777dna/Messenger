using Messenger.DTOs.Responses;
using Messenger.Entities;
using Messenger.MapperDto.Interfaces;

namespace Messenger.MapperDto;

public class ResponseMessageMapper : IResponseMapper<Message, ResponseMessageDto>
{
    public ResponseMessageDto ToDto(Message entity) =>
        new(entity.Id, entity.ChatId, entity.SenderId, entity.Content, entity.SentAt, entity.IsRead);
}
