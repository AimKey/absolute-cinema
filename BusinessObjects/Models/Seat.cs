using BusinessObjects.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class Seat : IBaseModel
{
    // Primary Key
    [Key]
    public Guid Id { get; set; }

    // Normal Properties
    [StringLength(50, ErrorMessage = "Seat row name cannot exceed 50 characters.")]
    public string SeatRow { get; set; }                             // e.g., A, B, C, etc.
    public int SeatNumber { get; set; }                             // e.g., 1, 2, 3, etc.

    [StringLength(200, ErrorMessage = "Seat description cannot exceed 200 characters.")]
    public string Description { get; set; }                        

    // Foreign Key
    public Guid SeatTypeId { get; set; }                      
    public Guid RoomId { get; set; }             

    // Navigation Properties
    public virtual SeatType SeatType { get; set; }  
    public virtual Room Room { get; set; }

    // Navigation Collections
    public virtual IEnumerable<ShowtimeSeat> ShowtimeSeats { get; set; } 

    // Audit Properties
    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? RemovedAt { get; set; }
    public Guid? RemovedBy { get; set; }
}
