using CRUDaster.Core.Application.Interfaces;
using CRUDaster.Core.Domain.Entities;

namespace CRUDaster.Infrastructure.Data.Repositories
{
    public class Repository<T>(ApplicationDbContext context, IUserContextService userContext) : RepositoryBase<T>(context) where T : CreatorUpdaterBase
    {
        private readonly IUserContextService _userContext = userContext;

        public override async Task<T> AddAsync(T entity)
        {
            var userId = _userContext.GetUserId() ?? throw new Exception("User Id is unknow!");
            entity.CreatorId = userId;
            entity.CreatedAt = DateTime.UtcNow;
            var createdItem = await base.AddAsync(entity);
            await SaveChangesAsync();

            return createdItem;
        }

        public override async Task UpdateAsync(T entity)
        {
            var userId = _userContext.GetUserId() ?? throw new Exception("User Id is unknow!");
            entity.UpdaterId = userId;
            entity.UpdatedAt = DateTime.UtcNow;
            await base.UpdateAsync(entity);
            await SaveChangesAsync();

            return;
        }
    }
}