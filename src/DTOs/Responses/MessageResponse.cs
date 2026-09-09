namespace Messenger.DTOs.Responses;

public record MessageResponse(Guid Id, Guid ChatId, Guid SenderId, string Content, DateTime SentAt, string Status);
