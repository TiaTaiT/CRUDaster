namespace CRUDaster.Core.Application.DTOs
{
    // This is obsolete class. It must be removed.
    public class CreatedUpdatedDtoBaseClass
    {
        public string CreatorId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? UpdaterId { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
