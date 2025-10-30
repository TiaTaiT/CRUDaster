using System.ComponentModel.DataAnnotations;

namespace CRUDaster.Core.Application.DTOs
{
    public record HardwareCreateDto(
        [Required][StringLength(255)] string Serial,
        [Required] string Description,
        IEnumerable<int> FunctionalityIds);

    public record HardwareUpdateDto(
        [Required] int Id,
        [StringLength(255)] string? Serial,
        string? Description,
        IEnumerable<int>? FunctionalityIds);

    public record HardwareDto(
        int Id,
        string Serial,
        string Description,
        IEnumerable<FunctionalitySimpleDto> Functionalities);

    public record HardwareCapsuleDto(
        int Id,
        string Serial,
        string Description,
        IEnumerable<FunctionalitySimpleDto> Functionalities,
        string Capsule) : HardwareDto(Id, Serial, Description, Functionalities);

    public record HardwareSimpleDto(
        int Id,
        string Serial);
}
