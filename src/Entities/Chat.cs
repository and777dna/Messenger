using Messenger.Enums;

namespace Messenger.Entities;

public class Chat
{
    public Guid Id { get; set; }
    public ChatType Type { get; set; }
    public string? Title { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<ChatMember> Members { get; set; } = [];
    public List<Message> Messages { get; set; } = [];
}
