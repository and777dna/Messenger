using Messenger.Entities;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Data;

public class MessengerDbContext : DbContext
{
    private static readonly Guid Chat1  = new("8f14e45f-ceea-467a-9a3e-7b3c1c2d0001");
    private static readonly Guid Chat2 = new("8f14e45f-ceea-467a-9a3e-7b3c1c2d0002");
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Chat>().HasData(
            new { Id = Chat1 },
            new { Id = Chat2 }
        );

        modelBuilder.Entity<Chat>().HasMany(u => u.Members).WithMany(chat => chat.Chats);
        modelBuilder.Entity<Chat>().HasMany(m => m.Messages).WithOne(chat => chat.Chat);
        
        modelBuilder.Entity<Message>().HasIndex(c => new{c.Id, c.SendDate});
    }
}