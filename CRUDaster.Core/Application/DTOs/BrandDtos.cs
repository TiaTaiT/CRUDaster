using System.ComponentModel.DataAnnotations;

namespace CRUDaster.Core.Application.DTOs
{
    public record BrandDto (
        int Id,
        string Name,
        string CreatorId,
        DateTime CreatedAt,
        string? UpdaterId,
        DateTime? UpdatedAt);

    public record BrandCreateDto(
        [StringLength(255)] string Name);

    public record BrandUpdateDto(
        [Required] int Id,
        [StringLength(255), Required] string Name);
}
