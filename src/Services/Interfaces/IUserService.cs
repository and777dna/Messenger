using Messenger.DTOs.Requests;

namespace Messenger.Services.Interfaces;

public interface IUserService
{
    public Task AddUserAsync(RequestUserDto requestUserDto, CancellationToken ct = default);
}