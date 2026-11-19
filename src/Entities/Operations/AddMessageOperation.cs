using Messenger.Entities.Operations.Interfaces;

namespace Messenger.Entities.Operations;

public class AddMessageOperation(Message message) : IOperation<Chat>
{
    public void Apply(Chat chat)
    {
        chat.AddMessage(message);
    }
}