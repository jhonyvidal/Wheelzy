using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wheelzy.Data.Entities;
public class Quote
{
    [Key]
    public int QuoteID { get; set; }
    public int CarID { get; set; }
    public int BuyerID { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Amount { get; set; }
    public bool IsCurrent { get; set; }

    // Navigation properties
    [ForeignKey("CarID")]
    public Car Car { get; set; }
    
    [ForeignKey("BuyerID")]
    public Buyer Buyer { get; set; }
}