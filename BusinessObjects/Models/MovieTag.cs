using BusinessObjects.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class MovieTag : IBaseModel
{
    // Primary Key
    [Key]
    public Guid Id { get; set; }

    // Foreign Keys
    public Guid MovieId { get; set; }
    public Guid TagId { get; set; }

    // Navigation Properties
    public virtual Movie Movie { get; set; }
    public virtual Tag Tag { get; set; }

    // Audit Properties
    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? RemovedAt { get; set; }
    public Guid? RemovedBy { get; set; }
}
