using BusinessObjects.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class UserDetail : IBaseModel
{
    // Primary Key
    [Key]
    public Guid Id { get; set; }

    // Normal properties
    public string FullName { get; set; }
    public string AvatarURL { get; set; }
    public string Gender { get; set; }
    public string Phone { get; set; }
    public DateTime Dob { get; set; }

    // Foreign Key
    public Guid UserId { get; set; }

    // Navigation properties
    public User User { get; set; }

    // Navigation Collections

    // Audit properties
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
    public DateTime RemovedAt { get; set; }
    public Guid RemovedBy { get; set; }
}
