namespace Messenger.DTOs.Responses;

public record ResponseMessageDto(
    Guid Id,
    Guid ChatId,
    Guid SenderId,
    string? Content,
    DateTimeOffset SentAt,
    bool IsRead);
