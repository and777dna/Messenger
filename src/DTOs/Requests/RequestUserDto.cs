using System.ComponentModel.DataAnnotations;

namespace Messenger.DTOs.Requests;

public record RequestUserDto
{
    [Required] public string? Name { get; }
    [Required] public string? Email { get; }
    [Required] public DateOnly RegistrationDate { get; } = DateOnly.FromDateTime(DateTime.UtcNow);
    [Required]
    public string? PasswordHash { get; }
};