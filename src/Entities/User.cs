using System.ComponentModel.DataAnnotations;

namespace Messenger.Entities;

public class User
{
    [Required]
    public Guid Id{get;} = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Email { get; set;}
    [Required]
    public DateOnly RegistrationDate { get; set; }
    [Required]
    public string? PasswordHash { get; set; }
    
    public ICollection<Chat> Chats { get; private set; } = new List<Chat>();
    public ICollection<Message> Messages { get; private set; } = new List<Message>();
}