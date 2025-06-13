using System.ComponentModel.DataAnnotations;

namespace CRUDaster.Core.Application.DTOs
{
    public record FunctionalityCreateDto(
    [Required][StringLength(255)] string Name,
    [Required] string Description,
    IEnumerable<int> HardwareIds);

    public record FunctionalityUpdateDto(
        [Required] int Id,
        [StringLength(255)] string? Name,
        string? Description,
        IEnumerable<int>? HardwareIds);

    public record FunctionalityDto(
        int Id,
        string Name,
        string Description,
        IEnumerable<HardwareSimpleDto> Hardwares);

    public record FunctionalitySimpleDto(
    int Id,
    string Name);
}
