using CRUDaster.Core.Domain.Entities;

namespace CRUDaster.Core.Application.Interfaces.Repositories
{
    public interface IDraftRepository : IRepository<Draft>
    {
        //Task<IEnumerable<Draft>> GetDraftsByNameAsync(string name);
    }
}
