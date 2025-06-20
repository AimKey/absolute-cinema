
using BusinessObjects.Models.BaseModels;

namespace BusinessObjects.Models;

// Composite key defined in the DbContext configuration
public class ShowtimeSeat : IBaseModel
{
    // Primary Key
    public Guid Id { get; set; }

    // Normal Properties

    // Foreign Key
    public Guid ShowtimeId { get; set; }
    public Guid SeatId { get; set; }
    public Guid TicketId { get; set; }

    // Navigation Properties
    public virtual Showtime Showtime { get; set; }
    public virtual Seat Seat { get; set; }
    public virtual Ticket Ticket { get; set; }

    // Navigation Collections


    // Audit Properties
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
    public DateTime RemovedAt { get; set; }
    public Guid RemovedBy { get; set; }
}
