
using BusinessObjects.Models.BaseModels;

namespace BusinessObjects.Models;

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
    public Showtime Showtime { get; set; }
    public Seat Seat { get; set; }
    public Ticket Ticket { get; set; }

    // Navigation Collections


    // Audit Properties
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
    public DateTime RemovedAt { get; set; }
    public Guid RemovedBy { get; set; }
}
