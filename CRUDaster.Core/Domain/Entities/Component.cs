namespace CRUDaster.Core.Domain.Entities
{
    public class Component : CreatorUpdaterBase
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string AlterName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string VendorCode { get; set; } = string.Empty;
        public bool CanHasChildren { get; set; } = true;
        public bool Virtual { get; set; } = false;
        public string ErpCode { get; set; } = "";
        public double Length { get; set; } = 0.0;
        public double Width { get; set; } = 0.0;
        public double Height { get; set; } = 0.0;
        public required Status Status { get; set; }
        public required Category Category { get; set; }
        public Brand? Brand { get; set; }
        public Model? Model { get; set; }
        public Pim? Pim { get; set; }
        public ICollection<Protocol> Protocols { get; set; } = [];
        public bool HasSerial { get; set; }
        public bool CanMountInCabinet { get; set; }
    }
}
