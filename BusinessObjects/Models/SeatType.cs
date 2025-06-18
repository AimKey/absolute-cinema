using BusinessObjects.Models.BaseModels;

namespace BusinessObjects.Models;

public class SeatType : IBaseModel
{
    // Primary Key
    public Guid Id { get; set; }

    // Normal Properties
    public string Name { get; set; }                                // e.g., Standard, Premium, VIP
    public decimal PriceMutiplier { get; set; }                      // e.g., "1.0" for Standard, "1.5" for Premium, "2.0" for VIP          
    
    // Foreign Key

    // Navigation Properties

    // Navigation Collections
    public IEnumerable<Seat> Seats { get; set; } 

    // Audit Properties
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
    public DateTime RemovedAt { get; set; }
    public Guid RemovedBy { get; set; }
}
