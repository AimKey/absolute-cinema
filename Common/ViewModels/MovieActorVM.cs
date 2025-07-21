namespace Common.ViewModels;

public class MovieActorVM
{
    public Guid Id { get; set; }
    public Guid MovieId { get; set; }
    public Guid ActorId { get; set; }

    public virtual MovieVM Movie { get; set; }
    public virtual ActorVM Actor { get; set; }
}
