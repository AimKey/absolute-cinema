
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Common.Filters.Tags;
using Common.DTOs.TagDTOs;
using Common;

namespace Absolute_cinema.Controllers.Tags;


[Route("Admin/Tags")]
public class TagsController : Controller
{
    private readonly ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }

    // GET: Tags
    [HttpGet("")]
    public IActionResult Index(
        string search, 
        string status, 
        string sort, 
        int page = 1
        )
    {
        var criteria = new TagFilterCriteria
        {
            Search = search,
            Status = status,
            Sort = sort,
            Page = page,
        };

        // filter and sort movies based on the criteria
        var result = _tagService.FilterTags(criteria);

        // set ViewBag properties for the view
        ViewBag.Criteria = criteria;
        ViewBag.CurrentPage = result.CurrentPage;
        ViewBag.TotalPages = result.TotalPages;
        ViewBag.TotalCount = result.TotalCount;
        ViewBag.PageSize = result.PageSize;

        // stats viewbags
        var allTags = _tagService.GetAll();
        ViewBag.OverallTotalTags = allTags.Count();
        ViewBag.OverallActiveTags = allTags.Count(t => t.RemovedAt == null);
        ViewBag.OverallDeletedTags = allTags.Count(t => t.RemovedAt != null);
        ViewBag.OverallMonthAddedTags = allTags.Count(t => t.CreatedAt.HasValue && t.CreatedAt.Value.Month == DateTime.Now.Month && t.CreatedAt.Value.Year == DateTime.Now.Year);

        return View(result.Items);
    }

    // GET: Tags/Details/5
    public IActionResult Details(Guid id)
    {

        var tag = _tagService.GetById(id);

        if (tag == null)
        {
            return NotFound();
        }

        return View(tag);
    }

    // GET: Tags/Create
    [HttpGet("Create")]
    public IActionResult Create()
    {
        CreateTagDTO tagDTO = new CreateTagDTO
        {
            IsActive = true
        };
        return View(tagDTO); // Mặc định IsActive là true
    }

    // POST: Tags/Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateTagDTO tagDTO)
    {
        try
        {
            _tagService.AddNewTag(tagDTO);
            SetTempMessage($"Tag '{tagDTO.Name}' has been created successfully.", StatusConstants.Success);
        }
        catch (Exception)
        {
            SetTempMessage("An error occurred while creating the tag. Please try again.", StatusConstants.Error);
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Tags/Edit/5
    [HttpGet("Edit/{id:guid}")]
    public IActionResult Edit(Guid id)
    {
        var tag = _tagService.GetById(id);
        if (tag == null)
        {
            return NotFound();
        }

        var tagDTO = new UpdateTagDTO
        {
            Id = tag.Id,
            Name = tag.Name,
            IsActive = tag.RemovedAt == null // If RemovedAt is null, the tag is active
        };

        return View(tagDTO);
    }

    // POST: Tags/Edit/5
    [HttpPost("Edit/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(UpdateTagDTO tagDTO)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _tagService.UpdateTag(tagDTO);
                SetTempMessage($"Tag '{tagDTO.Name}' has been updated successfully.", "success");
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                SetTempMessage("An error occurred while updating the tag. Please try again.", "error");
            }
            return RedirectToAction(nameof(Index));
        }
        return View(tagDTO);
    }

    // POST: Tags/Delete/5
    [HttpPost]
    [Route("Delete/{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var tag = _tagService.GetById(id);
        if (tag == null)
        {
            SetTempMessage("Tag not found.", "error");
            return RedirectToAction(nameof(Index));
        }

        try
        {
            _tagService.Delete(tag);
            SetTempMessage($"Tag '{tag.Name}' has been deleted successfully.", "success");
        }
        catch (Exception)
        {
            SetTempMessage("An error occurred while deleting the tag. Please try again.", "error");
        }

        return RedirectToAction(nameof(Index));
    }


    private void SetTempMessage(string message, string messageType)
    {
        TempData["msg"] = message;
        TempData["msgType"] = messageType;
    }
}
