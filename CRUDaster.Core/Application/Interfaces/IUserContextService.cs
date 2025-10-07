namespace CRUDaster.Core.Application.Interfaces
{
    public interface IUserContextService
    {
        string? GetUserId();
        public string? GetUserName();
        public string? GetUserEmail();
        public bool IsInRole(string role);
        public IEnumerable<string> GetUserRoles();
    }
}
