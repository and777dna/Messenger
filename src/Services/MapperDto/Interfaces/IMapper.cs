namespace Messenger.Services.MapperDto.Interfaces;

public interface IMapper<TEntity, TDto>
{
    public TEntity ToEntity(TDto dto);
}