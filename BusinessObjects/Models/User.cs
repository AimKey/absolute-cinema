using BusinessObjects.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class User : IBaseModel
{
    // Primary Key
    [Key]
    public Guid Id { get; set; }

    // Normal Properties
    [Required]
    [StringLength(100)]
    public string Username { get; set; }
    
    [Required]
    [StringLength(100)]   
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
    public string Role { get; set; }
    public bool IsVerify { get; set; } = false;
    public bool IsActive { get; set; } = true;

    // Navigation Properties
    public virtual UserDetail UserDetail { get; set; }

    // Navigation Collections
    public virtual IEnumerable<Booking> Bookings { get; set; }

    // Audit Properties
    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? RemovedAt { get; set; }
    public Guid? RemovedBy { get; set; }
}
