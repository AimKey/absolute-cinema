using BusinessObjects.Models;
using CloudinaryDotNet;
using Common.Constants;
using Common.DTOs.TagDTOs;
using Common.Filters.Tags;
using Common.Mappers;
using Common.Pagination;
using Common.ViewModels;
using Microsoft.IdentityModel.Tokens;
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

    public List<TagVM> GetAllTagVMs()
    {
        List<Tag> tags = GetAll().ToList();
        return tags.Select(t => TagMapper.MaptoTagVM(t)).ToList();
    }

    public PagedResult<TagVM> FilterTags(TagFilterCriteria criteria)
    {
        var tags = _tagRepository.Get()
        .AsQueryable();

        if (!string.IsNullOrEmpty(criteria.Search))
        {
            tags = tags.Where(t => t.Name.Contains(criteria.Search, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(criteria.Status))
        {
            if (criteria.Status.Equals("active", StringComparison.OrdinalIgnoreCase))
            {
                tags = tags.Where(t => t.RemovedAt == null);
            }
            else if (criteria.Status.Equals("inactive", StringComparison.OrdinalIgnoreCase))
            {
                tags = tags.Where(t => t.RemovedAt != null);
            }
        }

        // 2. Sắp xếp dữ liệu
        switch (criteria.Sort)
        {
            case "name_asc":
                tags = tags.OrderBy(t => t.Name);
                break;
            case "name_desc":
                tags = tags.OrderByDescending(t => t.Name);
                break;
            case "newest":
                tags = tags.OrderByDescending(t => t.CreatedAt);
                break;
            default: // Mặc định sắp xếp theo mới nhất nếu không có tham số sort
                tags = tags.OrderByDescending(t => t.CreatedAt);
                break;
        }

        // Pagination
        int totalFilteredCount = tags.Count();
        int pageSize = PageConstants.PageSize;
        var pageItems = tags.Skip((criteria.Page - 1) * pageSize).Take(pageSize).ToList();

        var tagVMs = pageItems.Select(m => TagMapper.MaptoTagVM(m)).ToList();

        return new PagedResult<TagVM>
        {
            Items = tagVMs,
            TotalCount = totalFilteredCount,
            PageSize = pageSize,
            CurrentPage = criteria.Page
        };
    }

    public void AddNewTag(CreateTagDTO tagDTO)
    {
        Tag newTag = new();
        newTag.Name = tagDTO.Name;

        if (!tagDTO.IsActive)
        {
            newTag.RemovedAt = DateTime.MinValue;
        }

        newTag.CreatedAt = DateTime.Now;

        Add(newTag);
    }

    public void UpdateTag(UpdateTagDTO tagDTO)
    {
        Tag existedTag = GetById(tagDTO.Id);

        // Check if the tag exists
        if (existedTag == null)
        {
            throw new KeyNotFoundException($"Tag with ID {tagDTO.Id} not found.");
        }

        // Update the tag properties
        existedTag.Name = tagDTO.Name;
        
        if (tagDTO.IsActive)
        {
            existedTag.RemovedAt = null; 
        }
        else
        {
            existedTag.RemovedAt = DateTime.Now;
        }

        existedTag.UpdatedAt = DateTime.Now;

        // Save the changes
        Update(existedTag);
    }

} 