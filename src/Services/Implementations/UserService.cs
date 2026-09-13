using Messenger.Entities;
using Messenger.Repositories.Interfaces;
using Messenger.Services.Interfaces;

namespace Messenger.Services.Implementations;

public class UserService(IRepository<User> userRepository) : IUserService
{
    public async Task AddUserAsync(User user, CancellationToken ct)
    { 
        if (user == null) throw new ArgumentNullException(nameof(user), "User is required");
       await userRepository.AddAsync(user, ct);
    }
}