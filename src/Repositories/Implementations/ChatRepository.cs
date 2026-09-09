using Messenger.Data;
using Messenger.Entities;
using Messenger.Exceptions;
using Messenger.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Repositories.Implementations;

public class ChatRepository(MessengerDbContext dbContext) : IChatRepository
{
    public async Task<Chat> GetByIdAsync(Guid id) =>
        await dbContext.Chats
            .Include(c => c.Members)
            .FirstOrDefaultAsync(c => c.Id == id)
        ?? throw new ChatNotFoundException(id);

    public async Task<IReadOnlyList<Chat>> GetForUserAsync(Guid userId) =>
        await dbContext.Chats
            .Where(c => c.Members.Any(m => m.UserId == userId))
            .ToListAsync();

    public async Task AddAsync(Chat chat)
    {
        dbContext.Chats.Add(chat);
        await dbContext.SaveChangesAsync();
    }
}
