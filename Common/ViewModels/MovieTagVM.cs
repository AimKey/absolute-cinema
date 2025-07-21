namespace Common.ViewModels;

public class MovieTagVM
{
    public Guid Id { get; set; }
    public Guid MovieId { get; set; }
    public Guid TagId { get; set; }

    public virtual MovieTagVM Movie { get; set; }
    public virtual TagVM Tag { get; set; }
}
