using Messenger.Caching;
using Messenger.DTOs.Requests;
using Messenger.DTOs.Responses;
using Messenger.Messaging;
using Messenger.Repositories.Interfaces;
using Messenger.Services.Interfaces;

namespace Messenger.Services.Implementations;

public class MessageService(
    IMessageRepository messageRepository,
    ICacheService cacheService,
    IEventPublisher eventPublisher) : IMessageService
{
    public Task<MessageResponse> SendMessageAsync(Guid senderId, SendMessageRequest request) =>
        throw new NotImplementedException();

    public Task<IReadOnlyList<MessageResponse>> GetHistoryAsync(Guid chatId, DateTime? since = null) =>
        throw new NotImplementedException();
}
