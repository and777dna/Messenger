using Messenger.Entities;

namespace Messenger.Repositories.Interfaces;

public interface IMessageRepository
{
    Task<IReadOnlyList<Message>> GetByChatIdAsync(Guid chatId, DateTime? since = null);
    Task AddAsync(Message message);
}
