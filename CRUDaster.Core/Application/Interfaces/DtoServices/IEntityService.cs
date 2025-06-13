namespace CRUDaster.Core.Application.Interfaces.DtoServices
{
    public interface IEntityService<TDto>
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task DeleteAsync(int id);
    }
}
