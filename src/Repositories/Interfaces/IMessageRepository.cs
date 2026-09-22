using Messenger.Entities;

namespace Messenger.Repositories.Interfaces;

public interface IMessageRepository
{
    Task<Message?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Message>> GetLatestAsync(Guid chatId,int limit = 50, DateOnly? before = null, CancellationToken ct = default);
    Task AddAsync(Message message);
    void Remove(Message message);
}