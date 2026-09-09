using Messenger.DTOs.Requests;
using Messenger.DTOs.Responses;
using Messenger.Repositories.Interfaces;
using Messenger.Services.Interfaces;

namespace Messenger.Services.Implementations;

public class AuthService(IUserRepository userRepository) : IAuthService
{
    public Task<AuthResponse> RegisterAsync(RegisterRequest request) =>
        throw new NotImplementedException();

    public Task<AuthResponse> LoginAsync(LoginRequest request) =>
        throw new NotImplementedException();
}
