using CRUDaster.Core.Domain.Entities;

namespace CRUDaster.Core.Application.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
    }
}
