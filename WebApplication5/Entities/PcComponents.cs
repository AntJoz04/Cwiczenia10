namespace WebApplication5.Entities;

public class PcComponents
{
    public int PcId { get; set; }
    public string ComponentCode {get; set;}
    public int Ammount { get; set; }
    public Pcs Pc {get; set;}
    public Components Component {get; set;}
}