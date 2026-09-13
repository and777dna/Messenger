using Messenger.Entities;

namespace Messenger.Services.Interfaces;

public interface IUserService
{
    public Task AddUserAsync(User user);
}