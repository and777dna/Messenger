using Messenger.Entities;

namespace Messenger.Repositories.Interfaces;

public interface IChatRepository
{
    Task<Chat> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Chat>> GetForUserAsync(Guid userId);
    Task AddAsync(Chat chat);
}
