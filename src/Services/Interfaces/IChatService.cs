using Messenger.DTOs.Requests;
using Messenger.DTOs.Responses;

namespace Messenger.Services.Interfaces;

public interface IChatService
{
    public Task SendMessageAsync(RequestMessageDto requestMessageDto, CancellationToken ct = default);
    public Task<IReadOnlyList<ResponseMessageDto>> GetMessagesAsync(Guid chatId, int limit, CancellationToken ct = default);
}
