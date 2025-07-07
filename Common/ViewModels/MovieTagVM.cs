namespace Absolute_cinema.Models.ViewModels;

public class MovieTagVM
{
    public Guid MovieId { get; set; }
    public Guid TagId { get; set; }

    public virtual MovieTagVM Movie { get; set; }
    public virtual TagVM Tag { get; set; }
}
