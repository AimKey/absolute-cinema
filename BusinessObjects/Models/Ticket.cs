using BusinessObjects.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class Ticket : IBaseModel
{
    // Primary Key
    [Key]
    public Guid Id { get; set; }

    // Normal Properties
    public string TicketCode { get; set; }                  // unique
    public decimal Price { get; set; }                      // calculated based on the showtime base price x seat type price multiplier

    // Foreign Key
    public Guid BookingId { get; set; }            
    public Guid ShowtimeSeatId { get; set; }

    // Navigation Properties
    public virtual Booking Booking { get; set; }
    public virtual ShowtimeSeat ShowtimeSeat { get; set; }

    // Navigation Collections

    // Audit Properties
    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? RemovedAt { get; set; }
    public Guid? RemovedBy { get; set; }
}
