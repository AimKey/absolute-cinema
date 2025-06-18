using BusinessObjects.Models.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models;

public class Movie : IBaseModel
{
    // Primary Key
    [Key]
    public Guid Id { get; set; }

    // Normal Properties
    public string Title { get; set; }
    public string OriginalTitle { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public DateTime ReleaseDate { get; set; }
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
    public IEnumerable<MovieTag> MovieTags { get; set; }
    public IEnumerable<MovieActor> MovieActors { get; set; }
    public IEnumerable<MovieDirector> MovieDirectors { get; set; }
    public IEnumerable<Review> Reviews { get; set; }
    public IEnumerable<Showtime> Showtimes { get; set; }

    // Audit Properties
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
    public DateTime RemovedAt { get; set; }
    public Guid RemovedBy { get; set; }
}
