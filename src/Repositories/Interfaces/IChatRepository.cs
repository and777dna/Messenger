using Messenger.Entities;

namespace Messenger.Repositories.Interfaces;

public interface IChatRepository
{
    Task<Chat?> GetByIdAsync(Guid id, CancellationToken ct = default);
    //Task<IReadOnlyList<Chat>> GetByParticipantAsync(Guid userId, CancellationToken ct = default);//TODO:to decide if i need this at all
    void Add(Chat chat);
    void Remove(Chat chat);
}