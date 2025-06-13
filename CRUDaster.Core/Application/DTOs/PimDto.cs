using System.ComponentModel.DataAnnotations;

namespace CRUDaster.Core.Application.DTOs
{
    public record PimDto(
        int Id,
        string Name,
        string Description,
        string Content,
        string CreatorId,
        DateTime CreatedAt,
        string? UpdaterId,
        DateTime? UpdatedAt);

    public record PimCreateDto(
        [StringLength(255), Required] string Name,
        [StringLength(255)] string? Description,
        [StringLength(65535), Required] string Content);

    public record PimUpdateDto(
        [Required] int Id,
        [StringLength(255)] string? Name,
        [StringLength(255)] string? Description,
        [StringLength(65535)] string? Content);
}
