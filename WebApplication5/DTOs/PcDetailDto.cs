namespace WebApplication5.DTOs;

public class PcDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
    public List<ComponentListDto> ComponentList { get; set; }
    
}

public class ComponentListDto
{
    public int Ammount { get; set; }
    public ComponentDto Component { get; set; }
    public ComponentTypeDto ComponentTypes { get; set; }
}

public class ComponentDto
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ManufacturerDto Manufacturer { get; set; }
}
public class ManufacturerDto
{
    public int Id { get; set; }
    public string Abbrevation { get; set; }
    public string Fullname { get; set; }
    public DateTime FoundationDate { get; set; }
}

public class ComponentTypeDto
{
    public int Id { get; set; }
    public string Abbrevation { get; set; }
    public string Name { get; set; }
}
