namespace CRUDaster.Core.Domain.Entities
{
    public class Document : CreatorUpdaterBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
