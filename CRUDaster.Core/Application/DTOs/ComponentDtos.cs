using CRUDaster.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace CRUDaster.Core.Application.DTOs
{
    public record ComponentDto(
        int Id,
        string Name,
        string AlterName,
        string Description,
        string VendorCode,
        bool CanHasChildren,
        bool Virtual,
        string ErpCode,
        double Length,
        double Width,
        double Height,
        Status Status,
        Category Category,
        Brand? Brand,
        Model? Model,
        Pim? Pim,
        ICollection<ProtocolSimpleDto> ProtocolDtos,
        string CreatorId,
        DateTime CreatedAt,
        string? UpdaterId,
        DateTime? UpdatedAt
    );

    public record ComponentForTablesDto(
        int Id,
        string Name,
        string AlterName,
        string Description,
        string? BrandName,
        int CategoryId,
        string ErpCode,
        ICollection<int> ProtocolsId
    );

    public record ComponentSimpleDto(
        int Id,
        string Name
    );

    public record ComponentCreateDto(
        [Required][StringLength(255)] string Name,
        [StringLength(255)] string AlterName,
        [Required][StringLength(255)] string Description,
        [Required][StringLength(255)] string VendorCode,
        bool CanHasChildren,
        bool Virtual,
        [StringLength(32)] string ErpCode,
        double Length,
        double Width,
        double Height,
        [Required] int StatusId,
        [Required] int CategoryId,
        [Required] int BrandId,
        int ModelId,
        int PimId,
        IEnumerable<int> ProtocolIds);

    public record ComponentUpdateDto(
        [Required] int Id,
        [StringLength(255)] string Name,
        [StringLength(255)] string AlterName,
        [StringLength(255)] string Description,
        [StringLength(255)] string VendorCode,
        bool CanHasChildren,
        bool Virtual,
        [StringLength(32)] string ErpCode,
        double Length,
        double Width,
        double Height,
        int StatusId,
        int CategoryId,
        int BrandId,
        int ModelId,
        int PimId,
        IEnumerable<int> ProtocolIds);
}
