namespace CRUDaster.Core.Domain.Entities
{
    public class Pim : CreatorUpdaterBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
