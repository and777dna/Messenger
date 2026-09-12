using Messenger.Data;
using Messenger.Entities;
using Messenger.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Repositories.Implementations;

public class SqlUserRepository(MessengerDbContext messengerDbContext) : IRepository<User>
{
    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken ct = default)
    {
        var users = await messengerDbContext.Users.ToListAsync();
        return users;    
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var user = await messengerDbContext.Users.FirstAsync(user => user.Id == id);
        return user;   
    }

    public Task AddAsync(User entity, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User entity, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(User entity, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}