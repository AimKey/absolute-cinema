using BusinessObjects.Models;
using Repositories;
using Services.Interfaces;

namespace Services.Implementations;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public IEnumerable<Tag> GetAll()
    {
        return _tagRepository.Get().ToList();
    }

    public Tag GetById(Guid id)
    {
        return _tagRepository.GetByID(id);
    }

    public void Add(Tag tag)
    {
        _tagRepository.Insert(tag);
        _tagRepository.Save();
    }

    public void Update(Tag tag)
    {
        _tagRepository.Update(tag);
        _tagRepository.Save();
    }

    public void Delete(Guid id)
    {
        _tagRepository.Delete(id);
        _tagRepository.Save();
    }

    public void Delete(Tag tag)
    {
        _tagRepository.Delete(tag);
        _tagRepository.Save();
    }
} 