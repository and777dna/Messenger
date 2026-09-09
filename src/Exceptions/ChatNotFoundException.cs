namespace Messenger.Exceptions;

public class ChatNotFoundException(Guid chatId) : Exception($"Chat '{chatId}' was not found.");
