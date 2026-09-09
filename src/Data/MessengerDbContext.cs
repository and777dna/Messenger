using Messenger.Entities;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Data;

public class MessengerDbContext(DbContextOptions<MessengerDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Chat> Chats => Set<Chat>();
    public DbSet<ChatMember> ChatMembers => Set<ChatMember>();
    public DbSet<Message> Messages => Set<Message>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChatMember>().HasKey(cm => new { cm.ChatId, cm.UserId });
    }
}
