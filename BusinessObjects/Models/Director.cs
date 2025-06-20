using BusinessObjects.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class Director : IBaseModel
{
    // Primary Key
    [Key]
    public Guid Id { get; set; }

    // Normal Properties
    public string Name { get; set; }
    public string AvatarURL { get; set; }
    public DateTime Dob { get; set; }
    public string Gender { get; set; }
    public string Description { get; set; }

    // Foreign Key


    // Navigation Properties


    // Navigation Collections
    public IEnumerable<MovieDirector> MovieDirectors { get; set; }

    // Audit Properties
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
    public DateTime RemovedAt { get; set; }
    public Guid RemovedBy { get; set; }
}
