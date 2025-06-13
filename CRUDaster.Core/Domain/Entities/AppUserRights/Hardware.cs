namespace CRUDaster.Core.Domain.Entities.AppUserRights
{
    public class Hardware
    {
        public int Id { get; set; }
        public required string Serial { get; set; }
        public required string Description { get; set; }
        public ICollection<Functionality> Functionalities { get; set; } = [];
    }
}
