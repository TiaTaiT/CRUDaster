namespace CRUDaster.Core.Domain.Entities
{
    public abstract class CreatorUpdaterBase
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatorId { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; }
        public string UpdaterId { get; set; } = string.Empty;
    }
}
