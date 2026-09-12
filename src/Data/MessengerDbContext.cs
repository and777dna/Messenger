using Messenger.Entities;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Data;

public class MessengerDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
}