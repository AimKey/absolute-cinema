using BusinessObjects.Models.BaseModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class UserDetail : IBaseModel
{
    // Primary Key
    [Key]
    [ValidateNever]
    public Guid Id { get; set; }

    // Normal properties
    [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters.")]
    public string FullName { get; set; }
    public string AvatarURL { get; set; }
    public string Gender { get; set; }

    [Phone(ErrorMessage = "Invalid phone number format.")]
    public string Phone { get; set; }
    public DateOnly? Dob { get; set; }

    // Foreign Key
    public Guid UserId { get; set; }

    // Navigation properties
    public virtual User User { get; set; }

    // Navigation Collections

    // Audit properties
    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? RemovedAt { get; set; }
    public Guid? RemovedBy { get; set; }
}
