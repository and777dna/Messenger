using Messenger.DTOs.Requests;
using Messenger.Entities;
using Messenger.MapperDto.Interfaces;

namespace Messenger.MapperDto.Implementations;

public class UserMapper : IMapper<User, RequestUserDto> 
{
    public User ToEntity(RequestUserDto dto)
    {
        return new User
        {
            Name = dto.Name,
            Email = dto.Email,
            RegistrationDate = dto.RegistrationDate,
            PasswordHash = dto.PasswordHash
        };
    }

    public RequestUserDto ToDto(User entity)
    {
        throw new NotImplementedException();
    }
}