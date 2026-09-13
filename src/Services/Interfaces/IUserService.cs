using Messenger.DTOs.Requests;
using Messenger.Entities;

namespace Messenger.Services.Interfaces;

public interface IUserService
{
    public Task AddUserAsync(RequestUserDto requestUserDto, CancellationToken ct = default);
}