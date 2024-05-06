namespace Wheelzy.Core.DTO;

public class CarInfoDTO
{
    public int Year { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public string Submodel { get; set; }
    public string CurrentBuyerName { get; set; }
    public decimal CurrentQuote { get; set; }
    public string CurrentStatus { get; set; }
    public DateTime? StatusDate { get; set; }
}