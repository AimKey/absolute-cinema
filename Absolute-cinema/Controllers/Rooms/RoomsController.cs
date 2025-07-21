
using Microsoft.AspNetCore.Mvc;
using DataAccessObjects;
using Common;
using Services.Interfaces;
using Common.DTOs.RoomDTOs;

namespace Absolute_cinema.Controllers.Rooms;

public class RoomsController : Controller
{
    private readonly IRoomService _roomService;

    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    // GET: Rooms
    public IActionResult Index()
    {
        var rooms = _roomService.GetAll();
        return View(rooms);
    }

    // GET: Rooms/Details/5
    public IActionResult Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var room = _roomService.GetById(id.Value);

        if (room == null)
        {
            return NotFound();
        }

        return View(room);
    }

    // GET: Rooms/Create
    public IActionResult Create()
    {
        CreateRoomDTO createRoomDTO = new CreateRoomDTO
        {
            Capacity = 0,
        };
        return View(createRoomDTO);
    }

    // POST: Rooms/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateRoomDTO createRoomDTO)
    {
        if (ModelState.IsValid)
        {
            // add new room
            try
            {
                _roomService.AddNewRoom(createRoomDTO);
            }
            catch (Exception)
            {
                SetTempMessage($"An error occurred while creating the room", StatusConstants.Error);
                return View(createRoomDTO);
            }
            // set a success message
            SetTempMessage($"Create room {createRoomDTO.Name} successfully!", StatusConstants.Success);
            
            return RedirectToAction(nameof(Index));
        }
        return View(createRoomDTO);
    }

    // GET: Rooms/Edit/5
    public IActionResult Edit(Guid id)
    {
        // Check if the room exists
        var room = _roomService.GetById(id);
        if (room == null)
        {
            return NotFound();
        }

        // Prepare the UpdateRoomDTO for the view
        UpdateRoomDTO updateRoomDTO = new UpdateRoomDTO
        {
            Id = room.Id,
            Name = room.Name,
            Capacity = room.Capacity,
            ScreenType = room.ScreenType,
            Description = room.Description
        };

        return View(updateRoomDTO);
    }

    // POST: Rooms/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(UpdateRoomDTO updateRoomDTO)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _roomService.UpdateRoom(updateRoomDTO);
                SetTempMessage($"Update room {updateRoomDTO.Name} successfully!", StatusConstants.Success);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                SetTempMessage($"An error occurred while updating the room", StatusConstants.Error);
            }
        }

        return View(updateRoomDTO);
    }


    // POST: Rooms/Delete/5
    [HttpPost]
    [Route("Rooms/Delete/{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        try
        {
            var room = _roomService.GetById(id);
            if (room == null)
            {
                SetTempMessage($"Room with ID {id} not found", StatusConstants.Error);
                return RedirectToAction(nameof(Index));
            }
            _roomService.Delete(room);
            SetTempMessage($"Room {room.Name} has been deleted successfully!", StatusConstants.Success);
        }
        catch (Exception)
        {
            SetTempMessage($"An error occurred while deleting the room", StatusConstants.Error);
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: Rooms/ManageSeats/5
    public IActionResult ManageSeats(Guid id)
    {
        var room = _roomService.GetById(id);
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
