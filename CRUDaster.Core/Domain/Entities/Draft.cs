namespace CRUDaster.Core.Domain.Entities
{
    public class Draft : CreatorUpdaterBase
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Model3Dfile { get; set; } = string.Empty;
        public string Model2Dfile { get; set; } = string.Empty;
        public string ImageFile { get; set; } = string.Empty;
    }
}
