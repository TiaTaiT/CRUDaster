namespace CRUDaster.Core.Domain.Entities.AppUserRights
{
    public class Functionality
    {
        public int Id { get; set; }
        public required string Name { get; set; } = "";
        public required string Description { get; set; } = "";
        public ICollection<Hardware> Hardwares { get; set; } = [];
    }
}
