namespace CRUDaster.Core.Domain.Entities
{
    public class Brand : CreatorUpdaterBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
