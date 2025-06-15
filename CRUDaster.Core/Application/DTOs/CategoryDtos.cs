using System.ComponentModel.DataAnnotations;

namespace CRUDaster.Core.Application.DTOs
{
    public record CategoryDto(
        int Id,
        string Name,
        string Description
    );

    public record CategoryCreateDto(
        [StringLength(20)] string Name,
        [StringLength(255)] string Description
    );

    public record CategoryUpdateDto(
        [Required] int Id,
        [StringLength(255)] string? Name,
        [StringLength(255)] string? Description
    );
}
