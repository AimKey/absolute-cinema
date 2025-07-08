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
using Common.ViewModels.ShowtimeVMs;
using Common.Constants;
using Common;
using Common.DTOs.ShowtimeDTOs;

namespace Absolute_cinema.Controllers
{
    public class ShowtimesController : Controller
    {
        private IShowtimeService _showtimeService;
        private readonly IRoomService _roomService;
        private readonly IMovieService _movieService;

        public ShowtimesController(IMovieService movieService, IRoomService roomService, IShowtimeService showtimeService)
        {
            _movieService = movieService;
            _roomService = roomService;
            _showtimeService = showtimeService;
        }

        // GET: Showtimes
        public IActionResult Index(ViewAllShowtimeVM vm)
        {
            var vms = _showtimeService.GetAllVM(vm);
            return View(vms);
        }

        // GET: Showtimes/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var showtime = _showtimeService.GetById(id.Value);
            if (showtime == null)
            {
                return NotFound();
            }

            return View(showtime);
        }

        // GET: Showtimes/Create
        public IActionResult Create()
        {
            var movies = _movieService.GetAll();
            var rooms = _roomService.GetAll();
            ViewData["MovieId"] = new SelectList(movies, "Id", "Title");
            ViewData["RoomId"] = new SelectList(rooms, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateShowtimeDTO showtimeDTO)
        {
            string msg = "Something went wrong, Please try again later";
            string msgType = "error";
            if (ModelState.IsValid)
            {
                try
                {
                    showtimeDTO.Id = Guid.NewGuid();
                    _showtimeService.AddShowtimeDTO(showtimeDTO);
                    TempData[StatusConstants.Message] = "New showtime added";
                    TempData[StatusConstants.MessageType] = StatusConstants.Success;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    msg = $"An error has occured: {e.Message}";
                }
            }
            SetupSelectListForMovieAndRoom();
            TempData[StatusConstants.Message] = msg;
            TempData[StatusConstants.MessageType] = msgType;
            return View(showtimeDTO);
        }

        // GET: Showtimes/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var showtime = _showtimeService.GetById(id.Value);
            if (showtime == null)
            {
                return NotFound();
            }
            SetupSelectListForMovieAndRoom();
            return View(showtime);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,StartTime,EndTime,BasePrice,Status,RoomId,MovieId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,RemovedAt,RemovedBy")] Showtime showtime)
        {
            if (id != showtime.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _showtimeService.Update(showtime);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowtimeExists(showtime.Id))
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
            SetupSelectListForMovieAndRoom();
            return View(showtime);
        }

        // GET: Showtimes/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var showtime = _showtimeService.GetById(id.Value);
            if (showtime == null)
            {
                return NotFound();
            }
            return View(showtime);
        }

        // POST: Showtimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var showtime = _showtimeService.GetById(id);
            if (showtime != null)
            {
                _showtimeService.Delete(showtime);
            }
            else
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ShowtimeExists(Guid id)
        {
            return _showtimeService.GetById(id) != null;
        }

        private void SetupSelectListForMovieAndRoom()
        {
            var movies = _movieService.GetAll();
            var rooms = _roomService.GetAll();
            ViewData["MovieId"] = new SelectList(movies, "Id", "Title");
            ViewData["RoomId"] = new SelectList(rooms, "Id", "Name");
        }

        public IActionResult GetCurrentShowtimeOfRoomInADay(Guid roomId, DateTime date)
        {
            List<PreviewRoomShowtimeVM> showtimes = _showtimeService
                                    .GetCurrentShowtimeOfRoomInADay(roomId, date);
            return PartialView("Showtime/PreviewRoomShowtimeInADay", showtimes);
        }
    }
}
