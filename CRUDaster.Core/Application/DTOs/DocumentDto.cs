using System.ComponentModel.DataAnnotations;

namespace CRUDaster.Core.Application.DTOs
{
    public record DocumentDto(
    int Id,
    string Name,
    string CreatorId,
    DateTime CreatedAt,
    string? UpdaterId,
    DateTime? UpdatedAt);

    public record DocumentCreateDto(
        [StringLength(255)] string Name);

    public record DocumentUpdateDto(
        [Required] int Id,
        [StringLength(255), Required] string Name);
}
