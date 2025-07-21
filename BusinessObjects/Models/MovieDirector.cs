﻿using BusinessObjects.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class MovieDirector : IBaseModel
{
    // Primary Key
    [Key]
    public Guid Id { get; set; }

    // Foreign Keys
    public Guid MovieId { get; set; }
    public Guid DirectorId { get; set; }

    // Navigation Properties
    public virtual Movie Movie { get; set; }
    public virtual Director Director { get; set; }

    // Audit Properties
    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? RemovedAt { get; set; }
    public Guid? RemovedBy { get; set; }
}
