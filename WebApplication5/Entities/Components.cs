namespace WebApplication5.Entities;

public class Components
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ComponentManufacturer ComponentManufacturer { get; set; }
    public int ComponentManufacturerId{get;set;}
    public ComponentTypes ComponentType { get; set; }
    public int ComponentTypeId { get; set; }
    public ICollection<PcComponents> PcComponents { get; set; } = [];
}