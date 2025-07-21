using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DataAccessObjects;
using Services.Interfaces;
using Common.Filters.Tags;
using Azure;
using Common.DTOs.TagDTOs;

namespace Absolute_cinema.Controllers.Tags;


[Route("Admin/Tags")]
public class TagsController : Controller
{
    private readonly AbsoluteCinemaContext _context;
    private readonly ITagService _tagService;

    public TagsController(AbsoluteCinemaContext context, ITagService tagService)
    {
        _context = context;
        _tagService = tagService;
    }

    // GET: Tags
    [HttpGet("")]
    public async Task<IActionResult> Index(
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
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tag = await _context.Tags
            .FirstOrDefaultAsync(m => m.Id == id);
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
    public async Task<IActionResult> Create(CreateTagDTO tagDTO)
    {
        _tagService.AddNewTag(tagDTO);
        return RedirectToAction(nameof(Index));
    }

    // GET: Tags/Edit/5
    [HttpGet("Edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var tag = _tagService.GetById(id.Value);
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
    public async Task<IActionResult> Edit(UpdateTagDTO tagDTO)
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
    public async Task<IActionResult> Delete(Guid id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag == null)
        {
            SetTempMessage("Tag not found.", "error");
            return RedirectToAction(nameof(Index));
        }

        try
        {
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
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
