using BusinessObjects.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class Director : IBaseModel
{
    // Primary Key
    [Key]
    public Guid Id { get; set; }

    // Normal Properties
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; }
    public string AvatarURL { get; set; }
    public DateTime Dob { get; set; }
    public string Gender { get; set; }

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string Description { get; set; }

    // Foreign Key


    // Navigation Properties


    // Navigation Collections
    public virtual IEnumerable<MovieDirector> MovieDirectors { get; set; }

    // Audit Properties
    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? RemovedAt { get; set; }
    public Guid? RemovedBy { get; set; }
}
