namespace Messenger.Entities;

public class Users
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public DateOnly RegistrationDate { get; private set; }
    public string? PasswordHash { get; private set; }
}