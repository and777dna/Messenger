using Messenger.Entities.Operations.Interfaces;

namespace Messenger.Entities.Operations.Implementations;

public class AddMessageOperation : IOperation<Chat>
{
    public void Operation(Guid messageId)
    {
        
    }
}//TODO: this one shouldnt be inside Message.cs