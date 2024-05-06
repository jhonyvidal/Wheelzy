using System.ComponentModel.DataAnnotations;

namespace Wheelzy.Data.Entities;
public class Status
{
    [Key]
    public int StatusID { get; set; }
    
    [Required, MaxLength(50)]
    public string Name { get; set; }

    // Navigation properties
    public ICollection<StatusHistory> StatusHistories { get; set; }
}