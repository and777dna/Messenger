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

    public async Task<User?> GetByIdAsync(Guid id, int page = 1, int pageSize = 50, CancellationToken ct = default)
    {
        var user = await messengerDbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        return user;   
    }

    public async Task AddAsync(User user, CancellationToken ct = default)
    {
        await messengerDbContext.Users.AddAsync(user);
    }

    public Task UpdateAsync(User user, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(User user, CancellationToken ct = default)
    {
        var findUser = await GetByIdAsync(user.Id);
        if(findUser != null)messengerDbContext.Users.Remove(findUser);
    }
}