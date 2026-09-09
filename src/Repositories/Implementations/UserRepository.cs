using Messenger.Data;
using Messenger.Entities;
using Messenger.Exceptions;
using Messenger.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Repositories.Implementations;

public class UserRepository(MessengerDbContext dbContext) : IUserRepository
{
    public async Task<User> GetByIdAsync(Guid id) =>
        await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id)
        ?? throw new UserNotFoundException(id);

    public Task<User?> FindByEmailAsync(string email) =>
        dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task AddAsync(User user)
    {
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
    }
}
