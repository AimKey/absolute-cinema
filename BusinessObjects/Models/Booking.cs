using BusinessObjects.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class Booking : IBaseModel
{
    // Primary Key
    [Key]
    public Guid Id { get; set; }

    // Normal Properties
    public DateTime BookingDate { get; set; }
    public int NumberOfTickets { get; set; }            // calculated based on the number of ticket
    public decimal TotalPrice { get; set; }             // calculated based on the number of tickets and price per ticket

    // Foreign Key
    public Guid UserId { get; set; }

    // Navigation Properties
    public virtual User User { get; set; }
    public virtual Payment Payment { get; set; }

    // Navigation Collections
    public virtual IEnumerable<Ticket> Tickets { get; set; }

    // Audit Properties
    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? RemovedAt { get; set; }
    public Guid? RemovedBy { get; set; }
}
