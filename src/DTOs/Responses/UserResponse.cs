namespace Messenger.DTOs.Responses;

public record UserResponse(Guid Id, string Username, string DisplayName, string? AvatarUrl, string Status);
