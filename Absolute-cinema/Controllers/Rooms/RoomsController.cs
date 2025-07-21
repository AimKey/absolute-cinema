
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DataAccessObjects;
using Microsoft.Data.SqlClient;
using Common.ViewModels;
using Common;
using Services.Interfaces;

namespace Absolute_cinema.Controllers.Rooms;

public class RoomsController : Controller
{
    private readonly AbsoluteCinemaContext _context;
    private readonly IRoomService _roomService;

    public RoomsController(AbsoluteCinemaContext context, IRoomService roomService)
    {
        _context = context;
        _roomService = roomService;
    }

    // GET: Rooms
    public async Task<IActionResult> Index()
    {
        return View(await _context.Rooms.ToListAsync());
    }

    // GET: Rooms/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var room = await _context.Rooms
            .FirstOrDefaultAsync(m => m.Id == id);
        if (room == null)
        {
            return NotFound();
        }

        return View(room);
    }

    // GET: Rooms/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Rooms/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Capacity,ScreenType,Description,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,RemovedAt,RemovedBy")] Room room)
    {
        if (ModelState.IsValid)
        {
            room.Id = Guid.NewGuid();
            _context.Add(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(room);
    }

    // GET: Rooms/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var room = await _context.Rooms.FindAsync(id);
        if (room == null)
        {
            return NotFound();
        }
        return View(room);
    }

    // POST: Rooms/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Capacity,ScreenType,Description,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,RemovedAt,RemovedBy")] Room room)
    {
        if (id != room.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(room);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(room.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(room);
    }


    // POST: Rooms/Delete/5
    [HttpPost]
    [Route("Rooms/Delete/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room == null)
        {
            return NotFound();
        }

        if (room != null)
        {
            try
            {
                _roomService.Delete(room);
                SetTempMessage($"Delete room successfully!", StatusConstants.Success);
            }
            catch (SqlException)
            {
                SetTempMessage("Cannot delete this room because it is referenced by other entities.", StatusConstants.Error);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                SetTempMessage($"An error occurred while trying to delete the room.", StatusConstants.Error);
                return RedirectToAction(nameof(Index));
            }
        }

        return RedirectToAction(nameof(Index));
    }

    private bool RoomExists(Guid id)
    {
        return _context.Rooms.Any(e => e.Id == id);
    }


    public IActionResult ManageSeats(Guid id)
    {
        var room = _context.Rooms.FirstOrDefault(r => r.Id == id && r.RemovedAt == null);
        if (room == null)
        {
            return NotFound();
        }
        ViewBag.RoomName = room.Name;
        ViewBag.RoomId = room.Id;
        return View("ManageSeats", room); 
    }

    private void SetTempMessage(string message, string messageType)
    {
        TempData["msg"] = message;
        TempData["msgType"] = messageType;
    }
}
