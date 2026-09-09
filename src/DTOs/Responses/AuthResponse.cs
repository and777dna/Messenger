namespace Messenger.DTOs.Responses;

public record AuthResponse(string Token, UserResponse User);
