using BusinessObjects.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class Movie : IBaseModel
{
    // Primary Key
    [Key]
    public Guid Id { get; set; }

    // Normal Properties
    [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
    public string Title { get; set; }

    [StringLength(200, ErrorMessage = "Original title cannot exceed 200 characters.")]
    public string OriginalTitle { get; set; }

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string Description { get; set; }
    public int Duration { get; set; }
    public DateTime ReleaseDate { get; set; }

    [StringLength(100, ErrorMessage = "Country name cannot exceed 100 characters.")]
    public string Country { get; set; }
    public string PosterURL { get; set; }
    public string BackgroundURL { get; set; }
    public string TrailerURL { get; set; }
    public decimal ImdbRating { get; set; }
    public int AgeRestriction { get; set; }
    public string Status { get; set; }

    // Foreign Key

    // Navigation Properties

    // Navigation Collections
    public virtual IEnumerable<MovieTag> MovieTags { get; set; }
    public virtual IEnumerable<MovieActor> MovieActors { get; set; }
    public virtual IEnumerable<MovieDirector> MovieDirectors { get; set; }
    public virtual IEnumerable<Review> Reviews { get; set; }
    public virtual IEnumerable<Showtime> Showtimes { get; set; }

    // Audit Properties
    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? RemovedAt { get; set; }
    public Guid? RemovedBy { get; set; }
}
