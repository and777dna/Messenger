namespace Messenger.Entities.Operations.Interfaces;

public interface IOperation<T>
{
    public void Operation(Guid id);
}