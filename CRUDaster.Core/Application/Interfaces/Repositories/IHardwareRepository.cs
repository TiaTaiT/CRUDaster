using CRUDaster.Core.Domain.Entities.AppUserRights;

namespace CRUDaster.Core.Application.Interfaces.Repositories
{
    public interface IHardwareRepository : IRepository<Hardware>
    {
        /// <summary>
        /// Returns a single Hardware by serial number, including its Functionalities.
        /// </summary>
        Task<Hardware> GetBySerialAsync(string serial);
    }
}
