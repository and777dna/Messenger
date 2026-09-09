using Messenger.Data;
using Messenger.Entities;
using Messenger.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Repositories.Implementations;

public class MessageRepository(MessengerDbContext dbContext) : IMessageRepository
{
    public async Task<IReadOnlyList<Message>> GetByChatIdAsync(Guid chatId, DateTime? since = null) =>
        await dbContext.Messages
            .Where(m => m.ChatId == chatId && (since == null || m.SentAt > since))
            .OrderBy(m => m.SentAt)
            .ToListAsync();

    public async Task AddAsync(Message message)
    {
        dbContext.Messages.Add(message);
        await dbContext.SaveChangesAsync();
    }
}
