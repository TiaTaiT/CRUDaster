using System.ComponentModel.DataAnnotations;

namespace CRUDaster.Core.Application.DTOs
{
    public record ModelDto(
        int Id,
        string Name,
        string Description,
        string Model2dFile,
        string Model3dFile,
        string ImageFile,
        string CreatorId,
        DateTime CreatedAt,
        string? UpdaterId,
        DateTime? UpdatedAt
    );

    public record ModelCreateDto(
        [StringLength(255)] string Name,
        [StringLength(255)] string Description,
        [StringLength(255)] string Model2dDFile,
        [StringLength(255)] string Model3dFile,
        [StringLength(255)] string ImageFile
    );

    public record ModelUpdateDto(
        [Required] int Id,
        [StringLength(255)] string Name,
        [StringLength(255)] string Description,
        [StringLength(255)] string Model2dDFile,
        [StringLength(255)] string Model3dFile,
        [StringLength(255)] string ImageFile
    );
}
