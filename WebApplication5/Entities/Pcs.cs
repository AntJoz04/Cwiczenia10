namespace WebApplication5.Entities;

public class Pcs
{
    public int  Id { get; set; }
    public string Name { get; set; }
    public decimal Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
    public ICollection<PcComponents> PcComponents { get; set; } = [];
}