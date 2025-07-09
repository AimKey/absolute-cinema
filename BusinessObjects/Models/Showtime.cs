using BusinessObjects.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class Showtime : IBaseModel
{
    // Primary Key
    [Key]
    public Guid Id { get; set; }

    // Normal Properties
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal BasePrice { get; set; }
    // This property is used to format the BasePrice for display purposes (It will not be stored in the database)
    public string BasePriceFormatted => BasePrice.ToString("C0", System.Globalization.CultureInfo.GetCultureInfo("vi-VN"));
    public bool Status { get; set; } = true;

    // Foreign Key
    public Guid RoomId { get; set; }
    public Guid MovieId { get; set; }

    // Navigation Properties
    public virtual Room Room { get; set; }
    public virtual Movie Movie { get; set; }

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
