namespace CRUDaster.Core.Application.Interfaces
{
    public interface IUserContextService
    {
        Task<string> GetUserIdAsync();
    }
}
