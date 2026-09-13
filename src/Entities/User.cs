namespace Messenger.Entities;

public class User
{//регистрация пользователя
    public Guid Id{get; private set;}
    public string? Name { get; private set; }
    public string? Email { get; private set; }
    public DateOnly RegistrationDate { get; private set; }
    public string? PasswordHash { get; private set; }
}