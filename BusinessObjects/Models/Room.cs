using BusinessObjects.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class Room : IBaseModel
{
    // Primary Key
    [Key]
    public Guid Id { get; set; }

    // Normal Properties
    public string Name {  get; set; }
    public int Capacity { get; set; }
    public string ScreenType { get; set; }                      // e.g., 2D, 3D, IMAX
    public string Description { get; set; }

    // Foreign Key

    // Navigation Properties

    // Navigation Collections
    public IEnumerable<Seat> Seats { get; set; }
    public IEnumerable<Showtime> Showtimes { get; set; }

    // Audit Properties
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
    public DateTime RemovedAt { get; set; }
    public Guid RemovedBy { get; set; }
}
