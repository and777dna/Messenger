namespace Messenger.MapperDto.Interfaces;

public interface IResponseMapper<TEntity, TDto>
{
    TDto ToDto(TEntity entity);
}
