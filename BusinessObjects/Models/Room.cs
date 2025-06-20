using BusinessObjects.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class Room : IBaseModel
{
    // Primary Key
    [Key]
    public Guid Id { get; set; }

    // Normal Properties
    [StringLength(100, ErrorMessage = "Room name cannot exceed 100 characters.")]
    public string Name {  get; set; }
    public int Capacity { get; set; }
    [StringLength(50, ErrorMessage = "Screen type cannot exceed 50 characters.")]
    public string ScreenType { get; set; }                      // e.g., 2D, 3D, IMAX

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string Description { get; set; }

    // Foreign Key

    // Navigation Properties

    // Navigation Collections
    public virtual IEnumerable<Seat> Seats { get; set; }
    public virtual IEnumerable<Showtime> Showtimes { get; set; }

    // Audit Properties
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
    public DateTime RemovedAt { get; set; }
    public Guid RemovedBy { get; set; }
}
