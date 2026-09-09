using Messenger.DTOs.Requests;
using Messenger.DTOs.Responses;

namespace Messenger.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
}
