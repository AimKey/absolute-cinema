using BusinessObjects.Models;

namespace Absolute_cinema.Models.ViewModels;

public class TagVM
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public virtual List<MovieTagVM> MovieTags { get; set; }
}
