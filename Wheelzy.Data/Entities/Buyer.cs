using System.ComponentModel.DataAnnotations;

namespace Wheelzy.Data.Entities;
public class Buyer
{
    [Key]
    public int BuyerID { get; set; }
    
    [Required, MaxLength(100)]
    public string Name { get; set; }

    // Navigation properties
    public ICollection<Quote> Quotes { get; set; }
}