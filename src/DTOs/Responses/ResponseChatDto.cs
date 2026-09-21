using Messenger.Entities;

namespace Messenger.DTOs.Responses;

public record ResponseChatDto
{
    public bool IsPrivate{ get;  set; }
    public IEnumerable<User> Members { get;  set; }
    public IEnumerable<Message> Messages { get;  set; }
};