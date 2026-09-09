using Messenger.DTOs.Requests;
using Messenger.DTOs.Responses;
using Messenger.Repositories.Interfaces;
using Messenger.Services.Interfaces;

namespace Messenger.Services.Implementations;

public class ChatService(IChatRepository chatRepository) : IChatService
{
    public Task<ChatResponse> CreateDirectChatAsync(Guid currentUserId, CreateChatRequest request) =>
        throw new NotImplementedException();

    public Task<IReadOnlyList<ChatResponse>> GetChatsForUserAsync(Guid userId) =>
        throw new NotImplementedException();
}
