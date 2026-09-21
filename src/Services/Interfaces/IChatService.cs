using Messenger.DTOs.Requests;
using Messenger.DTOs.Responses;

namespace Messenger.Services.Interfaces;

public interface IChatService
{
    public Task SendMessageAsync(RequestMessageDto requestMessageDto, CancellationToken ct = default);
    public Task<ResponseChatDto> GetMessagesAsync(Guid chatId, int page, int pageSize, CancellationToken ct = default);
}