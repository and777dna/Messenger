using Messenger.DTOs.Requests;
using Messenger.DTOs.Responses;

namespace Messenger.Services.Interfaces;

public interface IChatService
{
    Task<ChatResponse> CreateDirectChatAsync(Guid currentUserId, CreateChatRequest request);
    Task<IReadOnlyList<ChatResponse>> GetChatsForUserAsync(Guid userId);
}
