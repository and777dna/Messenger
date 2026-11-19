using Messenger.Entities;

namespace Messenger.Repositories.Interfaces;

public interface IMessageRepository
{
    Task<Message?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Message>> GetLatestAsync(Guid chatId, int limit = 50, DateTimeOffset? before = null, CancellationToken ct = default);
    Task<bool> AddAsync(Message message, CancellationToken ct = default);
    void Remove(Message message, CancellationToken ct = default);
}