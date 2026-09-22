using Messenger.DTOs.Responses;
using Messenger.Entities;
using Messenger.MapperDto.Interfaces;

namespace Messenger.MapperDto.Implementations;

public class ChatMapper : IMapper<Chat, ResponseChatDto>
{
    public Chat ToEntity(ResponseChatDto dto)
    {
        throw new NotImplementedException();
    }

    public ResponseChatDto ToDto(Chat chat)
    {
        throw new NotImplementedException();
    }
}