using Messenger.Data;
using Messenger.Entities;
using Messenger.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Repositories;

public class MessageRepository(MessengerDbContext messengerDbContext) : IMessageRepository
{
    public Task<Message?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        messengerDbContext.Messages.FirstOrDefaultAsync(m => m.Id == id, ct);

    public async Task<IReadOnlyList<Message>> GetLatestAsync(Guid chatId,int limit = 50, DateOnly? before = null, CancellationToken ct = default)
    {
        var query = messengerDbContext.Messages.Where(m => m.ChatId == chatId);
        if (before != null)
        {
            query = query.Where(m => m.SendDate < before);
        }

        return await query.OrderByDescending(m => m.SendDate).Take(50).ToListAsync(ct);
    }

    public async Task AddAsync(Message message, CancellationToken ct = default)
    {
        await messengerDbContext.Messages.AddAsync(message, ct);
    }

    public void Remove(Message message, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}