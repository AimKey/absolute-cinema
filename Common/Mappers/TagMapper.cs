using BusinessObjects.Models;
using Common.ViewModels;

namespace Common.Mappers;

public class TagMapper
{
    public static TagVM MaptoTagVM(Tag tag)
    {
        return new TagVM
        {
            Id = tag.Id,
            Name = tag.Name,
            CreatedAt = tag.CreatedAt.HasValue ? tag.CreatedAt.Value : DateTime.Now,
            UpdatedAt = tag.UpdatedAt,
            RemovedAt = tag.RemovedAt
        };
    }
}
