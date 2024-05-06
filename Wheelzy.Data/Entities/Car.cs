using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wheelzy.Data.Entities;
public class Car
{
    [Key]
    public int CarID { get; set; }
    public int Year { get; set; }
    [Required, MaxLength(50)]
    public string Make { get; set; }
    [Required, MaxLength(50)]
    public string Model { get; set; }
    [MaxLength(50)]
    public string Submodel { get; set; }
    [Required, MaxLength(10)]
    public string ZipCode { get; set; }

    // Foreign key for CurrentBuyer
    public int? CurrentBuyerID { get; set; }
    
    [ForeignKey("CurrentBuyerID")]
    public Buyer CurrentBuyer { get; set; }

    // Navigation properties
    public ICollection<Quote> Quotes { get; set; }
    public ICollection<StatusHistory> StatusHistories { get; set; }
}