using Messenger.DTOs.Requests;
using Messenger.Entities;
using Messenger.MapperDto.Interfaces;
using Messenger.Repositories.Interfaces;
using Messenger.Services.Interfaces;

namespace Messenger.Services;

public class UserService(IRepository<User> userRepository, IMapper<User, RequestUserDto> mapper) : IUserService
{
    public async Task AddUserAsync(RequestUserDto requestUserDto, CancellationToken ct)
    {
        if (requestUserDto == null) throw new ArgumentNullException(nameof(requestUserDto), "requestUserDto is required");
        var user = mapper.ToEntity(requestUserDto);
       await userRepository.AddAsync(user, ct);
    }
}