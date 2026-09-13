using System.ComponentModel.DataAnnotations;

namespace Messenger.Entities;

public class User
{//регистрация пользователя
    [Required]
    public Guid Id{get; private set;}
    public string? Name { get; private set; }
    public string? Email { get; private set; }
    [Required]
    public DateOnly RegistrationDate { get; private set; }
    [Required]
    public string? PasswordHash { get; private set; }
}