using CRUDaster.Core.Application.Interfaces.Repositories;
using CRUDaster.Core.Domain.Entities;

namespace CRUDaster.Infrastructure.Data.Repositories
{
    public class DocumentRepository(ApplicationDbContext context) : Repository<Document>(context), IDocumentRepository
    {
    }
}
