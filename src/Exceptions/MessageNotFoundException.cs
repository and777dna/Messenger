namespace Messenger.Exceptions;

public class MessageNotFoundException(Guid messageId) : Exception($"Message '{messageId}' was not found.");
