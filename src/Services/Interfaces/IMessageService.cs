using Messenger.DTOs.Requests;
using Messenger.DTOs.Responses;

namespace Messenger.Services.Interfaces;

public interface IMessageService
{
    Task<MessageResponse> SendMessageAsync(Guid senderId, SendMessageRequest request);
    Task<IReadOnlyList<MessageResponse>> GetHistoryAsync(Guid chatId, DateTime? since = null);
}
