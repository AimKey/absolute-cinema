
namespace Common.ViewModels;

public class TagVM
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? RemovedAt { get; set; }
    public virtual IEnumerable<MovieTagVM> MovieTags { get; set; }
}
