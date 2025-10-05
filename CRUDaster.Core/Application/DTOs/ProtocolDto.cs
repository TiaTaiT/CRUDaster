using System.ComponentModel.DataAnnotations;

namespace CRUDaster.Core.Application.DTOs
{
    public record ProtocolCreateDto(
        [Required][StringLength(255)] string Name,
        [Required] string Description,
        IEnumerable<int> ComponentIds);

    public record ProtocolUpdateDto(
        [Required] int Id,
        [StringLength(255)] string? Name,
        string? Description,
        IEnumerable<int>? ComponentIds);

    public record ProtocolDto(
        int Id,
        string Name,
        string Description,
        IEnumerable<ComponentSimpleDto> Components);

    public record ProtocolSimpleDto(
        int Id,
        string Name);
}
