namespace CRUDaster.Core.Application.DTOs
{
    public abstract record UpdatedDtoBase
    {
        public string? UpdaterId { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
