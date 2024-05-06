namespace Wheelzy.Core.DTO;
public class OrderDTO
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }
    public int StatusId { get; set; }
}