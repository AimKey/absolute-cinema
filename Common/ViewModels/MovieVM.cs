using BusinessObjects.Models;

namespace Common.ViewModels;

public class MovieVM
{
    public Guid Id { get; set; }
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

    public decimal ViewCount { get; set; }
    public string Language { get; set; }
    public string Quality { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsNewRelease { get; set; }
    public List<MovieTagVM> MovieTags;
    public List<MovieActorVM> MovieActors;
    public List<MovieDirectorVM> MovieDirectors;
    public List<ReviewVM> Reviews { get; set; }
    public decimal Price { get; set; }
    public bool IsFree { get; set; }
    public DateTime CreatedAt { get; set; }

}
