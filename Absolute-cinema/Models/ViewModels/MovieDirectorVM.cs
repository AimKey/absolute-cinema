namespace Absolute_cinema.Models.ViewModels;

public class MovieDirectorVM
{
    public Guid Id { get; set; }
    public Guid MovieId { get; set; }
    public Guid DirectorId { get; set; }

    public virtual MovieVM Movie { get; set; }
    public virtual DirectorVM Director { get; set; }
}
