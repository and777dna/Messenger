namespace Messenger.DTOs.Requests;

public record SendMessageRequest(Guid ChatId, string Content);
