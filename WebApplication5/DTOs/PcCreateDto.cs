namespace WebApplication5.DTOs;

public class PcCreateDto
{
    public string Name { get; set; }
    public decimal Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
}