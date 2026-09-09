using Messenger.Entities;

namespace Messenger.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);
    Task<User?> FindByEmailAsync(string email);
    Task AddAsync(User user);
}
