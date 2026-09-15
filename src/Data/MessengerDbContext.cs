using Messenger.Entities;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Data;

public class MessengerDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Message>().HasIndex(c => new{c.Id, c.SendDate});
    }
}