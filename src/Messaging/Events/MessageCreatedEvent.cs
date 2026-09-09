namespace Messenger.Messaging.Events;

public record MessageCreatedEvent(
    Guid MessageId,
    Guid ChatId,
    Guid SenderId,
    Guid RecipientId,
    string Content,
    DateTime SentAt);
