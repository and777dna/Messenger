using Messenger.DTOs.Requests;
using Messenger.DTOs.Responses;
using Messenger.Entities;

namespace Messenger.Services.Interfaces;

public interface IChatService
{
    public Task SendMessageAsync(RequestMessageDto requestMessageDto, CancellationToken ct = default);
    public Task<IReadOnlyList<Message>> GetMessagesAsync(Guid chatId, int limit, CancellationToken ct = default);
}