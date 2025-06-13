using System.ComponentModel.DataAnnotations;

namespace CRUDaster.Core.Application.DTOs
{
    public class DraftDto : CreatedUpdatedDtoBaseClass
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Model3Dfile { get; set; } = string.Empty;
        public string Model2Dfile { get; set; } = string.Empty;
        public string ImageFile { get; set; } = string.Empty;
    }

    public class CreateDraftDto
    {
        [Required(ErrorMessage = "Description is required")]
        [StringLength(255, ErrorMessage = "Description is too long")]
        public string Description { get; set; } = string.Empty;

        public string Model3Dfile { get; set; } = string.Empty;
        public string Model2Dfile { get; set; } = string.Empty;
        public string ImageFile { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class UpdateDraftDto
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Model3Dfile { get; set; } = string.Empty;
        public string Model2Dfile { get; set; } = string.Empty;
        public string ImageFile { get; set; } = string.Empty;
    }
}
