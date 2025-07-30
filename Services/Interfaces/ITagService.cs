using BusinessObjects.Models;
using Common.DTOs.TagDTOs;
using Common.Filters.Tags;
using Common.Pagination;
using Common.ViewModels;

namespace Services.Interfaces;

public interface ITagService
{
    IEnumerable<Tag> GetAll();
    Tag GetById(Guid id);
    void Add(Tag tag);
    void Update(Tag tag);
    void Delete(Guid id);
    void Delete(Tag tag);

    List<TagVM> GetAllTagVMs();
    PagedResult<TagVM> FilterTags(TagFilterCriteria criteria);
    void AddNewTag(CreateTagDTO tagDTO);
    void UpdateTag(UpdateTagDTO tagDTO);
} 