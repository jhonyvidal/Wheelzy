using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wheelzy.Data.Entities;
public class StatusHistory
{
    [Key]
    public int StatusHistoryID { get; set; }
    public int CarID { get; set; }
    public int StatusID { get; set; }
    [Required]
    public DateTime StatusDate { get; set; }
    [Required, MaxLength(100)]
    public string ChangedBy { get; set; }

    // Navigation properties
    [ForeignKey("CarID")]
    public Car Car { get; set; }
    
    [ForeignKey("StatusID")]
    public Status Status { get; set; }
}