namespace Messenger.DTOs.Responses;

public record ChatResponse(Guid Id, string Type, string? Title, DateTime CreatedAt);
