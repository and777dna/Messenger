using Messenger.Data;
using Messenger.Entities;
using Messenger.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Repositories;

public class SqlMessageRepository(MessengerDbContext messengerDbContext) : IMessageRepository
{
    public Task<Message?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        messengerDbContext.Messages.FirstOrDefaultAsync(m => m.Id == id);

    public async Task<IReadOnlyList<Message>> GetLatestAsync(Guid chatId,int limit = 50, DateOnly? before = null, CancellationToken ct = default)
    {
        var query = messengerDbContext.Messages.Where(m => m.ChatId == chatId);
        if (before != null)
        {
            query = query.Where(m => m.SendDate < before);
        }

        return await query.OrderByDescending(m => m.SendDate).Take(50).ToListAsync();
    }

    public Task AddAsync(Message message)
    {
        throw new NotImplementedException();
    }

    public void Remove(Message message)
    {
        throw new NotImplementedException();
    }
}