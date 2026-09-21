namespace Messenger.MapperDto.Interfaces;

public interface IMapper<TEntity, TDto>
{
    public TEntity ToEntity(TDto dto);
    public TDto ToDto(TEntity entity);
}