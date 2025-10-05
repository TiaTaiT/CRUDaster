namespace CRUDaster.Core.Domain.Entities
{
    public class Model : CreatorUpdaterBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Model3dFile { get; set; } = ""; // file name
        public string Model2dFile { get; set; } = ""; // file name
        public string ImageFile { get; set; } = ""; // file name
    }
}
