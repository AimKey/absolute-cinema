using BusinessObjects.Models;

namespace Services.Interfaces;

public interface ITagService
{
    IEnumerable<Tag> GetAll();
    Tag GetById(Guid id);
    void Add(Tag tag);
    void Update(Tag tag);
    void Delete(Guid id);
    void Delete(Tag tag);
} 