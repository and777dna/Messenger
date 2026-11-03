using Messenger.Entities;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Data;

public class MessengerDbContext(DbContextOptions<MessengerDbContext> options) : DbContext(options)
{
    private static readonly Guid Chat1  = new("8f14e45f-ceea-467a-9a3e-7b3c1c2d0001");
    private static readonly Guid Chat2 = new("8f14e45f-ceea-467a-9a3e-7b3c1c2d0002");
    private static readonly Guid User1 = new("8f14e45f-ceea-467a-9a3e-7b3c1c2d1001");
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Chat>().HasData(
            new { Id = Chat1, IsPrivate = false },
            new { Id = Chat2, IsPrivate = false }
        );
        modelBuilder.Entity<User>().HasData(
            new
            {
                Id = User1,
                Name = "seed-user",
                Email = "seed-user@example.com",
                RegistrationDate = new DateOnly(2026, 1, 1),
                PasswordHash = "seed-password-hash"
            }
        );
        modelBuilder.Entity<Message>().HasData(
            new
            {
                Id = new Guid("8f14e45f-ceea-467a-9a3e-7b3c1c2d2001"),
                ChatId = Chat1,
                SenderId = User1,
                Content = "Hello!",
                SendDate = new DateOnly(2026, 1, 1),
                IsRead = true
            },
            new
            {
                Id = new Guid("8f14e45f-ceea-467a-9a3e-7b3c1c2d2002"),
                ChatId = Chat1,
                SenderId = User1,
                Content = "How are you?",
                SendDate = new DateOnly(2026, 1, 2),
                IsRead = true
            },
            new
            {
                Id = new Guid("8f14e45f-ceea-467a-9a3e-7b3c1c2d2003"),
                ChatId = Chat1,
                SenderId = User1,
                Content = "This is the latest seeded message.",
                SendDate = new DateOnly(2026, 1, 3),
                IsRead = false
            }
        );
        
        modelBuilder.Entity<Message>().HasIndex(o => o.IdempotencyKey).IsUnique();
        
        modelBuilder.Entity<Chat>().HasMany(u => u.Members).WithMany(chat => chat.Chats);
        modelBuilder.Entity<Chat>().HasMany(m => m.Messages).WithOne(chat => chat.Chat);
        
        modelBuilder.Entity<Message>().HasIndex(c => new{c.Id, c.SendDate});
    }
}