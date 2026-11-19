using Messenger.Data;
using Messenger.Entities;
using Messenger.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Repositories;

public class ChatRepository(MessengerDbContext messengerDbContext): IRepository<Chat>
{
    public async Task<IEnumerable<Chat>> GetAllAsync(CancellationToken ct = default)
    {
        return await messengerDbContext.Chats.ToListAsync(ct);
    }

    public async Task<Chat?> GetByIdAsync(Guid id, int page, int pageSize, CancellationToken ct = default)
    {
        var chat = await messengerDbContext.Chats.Skip((page - 1)*pageSize).Take(pageSize).FirstOrDefaultAsync(chat => chat.Id == id, ct);
        return chat; 
    }

    public async Task AddAsync(Chat entity, CancellationToken ct = default)
    {
        await messengerDbContext.Chats.AddAsync(entity, ct);
    }

    public Task UpdateAsync(Chat entity, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Chat entity, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}