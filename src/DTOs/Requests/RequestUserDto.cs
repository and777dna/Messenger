using System.ComponentModel.DataAnnotations;

namespace Messenger.DTOs.Requests;

public record RequestUserDto
{
    public required string Name { get; init; }
    public string? Email { get; init; }
    [Required] public DateOnly RegistrationDate { get; } = DateOnly.FromDateTime(DateTime.UtcNow);
    public required string PasswordHash { get; init; }
}