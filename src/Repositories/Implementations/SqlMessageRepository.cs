using Messenger.Data;
using Messenger.Entities;
using Messenger.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Repositories.Implementations;

public class SqlMessageRepository(MessengerDbContext messengerDbContext) : IRepository<Message>
{
    public async Task<IEnumerable<Message>> GetAllAsync(CancellationToken ct = default)
    {
        return await messengerDbContext.Messages.ToListAsync();
    }

    public Task<Message?> GetByIdAsync(Guid id, int page, int pageSize, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(Message entity, CancellationToken ct = default)
    {
        await messengerDbContext.Messages.AddAsync(entity);
    }

    public Task UpdateAsync(Message entity, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Message entity, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}