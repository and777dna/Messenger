using Messenger.DTOs.Requests;
using Messenger.Entities;
using Messenger.Repositories.Interfaces;
using Messenger.Services.Interfaces;
using Messenger.Services.MapperDto.Interfaces;

namespace Messenger.Services.Implementations;

public class ChatService(IMapper<Message, RequestMessageDto> mapper, IRepository<Message> messageRepository) : IChatService
{
    public async Task SendMessageAsync(RequestMessageDto requestMessageDto, CancellationToken ct = default)
    {
        var message = mapper.ToEntity(requestMessageDto);
        await messageRepository.AddAsync(message);
    }
}