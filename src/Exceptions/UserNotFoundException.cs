namespace Messenger.Exceptions;

public class UserNotFoundException(Guid userId) : Exception($"User '{userId}' was not found.");
