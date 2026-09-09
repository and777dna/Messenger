namespace Messenger.DTOs.Requests;

public record RegisterRequest(string Username, string Email, string Password, string DisplayName);
