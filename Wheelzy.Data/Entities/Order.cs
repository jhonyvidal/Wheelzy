namespace Wheelzy.Data.Entities;
public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }
    public int StatusId { get; set; }
    public bool IsActive { get; set; }

    // Navigation properties
    public Customer Customer { get; set; }
    public Status Status { get; set; }
}