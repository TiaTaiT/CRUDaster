using System.ComponentModel.DataAnnotations;

namespace CRUDaster.Core.Application.DTOs
{
    public record StatusDto(
    int Id,
    string Name
);

    public record StatusCreateDto(
        [StringLength(20)] string Name
    );

    public record StatusUpdateDto(
        [Required] int Id,
        [StringLength(20)] string? Name
    );
}
