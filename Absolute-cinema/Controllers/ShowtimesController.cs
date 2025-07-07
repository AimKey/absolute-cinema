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
        public async Task<IActionResult> Index()
        {
            var list = _showtimeService.GetAll();
            return View(list);
        }

        // GET: Showtimes/Details/5
        public async Task<IActionResult> Details(Guid? id)
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
            SetupSelectListForMovieAndRoom();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartTime,EndTime,BasePrice,Status,RoomId,MovieId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,RemovedAt,RemovedBy")] Showtime showtime)
        {
            if (ModelState.IsValid)
            {
                showtime.Id = Guid.NewGuid();
                _showtimeService.Add(showtime);
                return RedirectToAction(nameof(Index));
            }
            SetupSelectListForMovieAndRoom();
            return View(showtime);
        }

        // GET: Showtimes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,StartTime,EndTime,BasePrice,Status,RoomId,MovieId,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,RemovedAt,RemovedBy")] Showtime showtime)
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
        public async Task<IActionResult> Delete(Guid? id)
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
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var showtime = _showtimeService.GetById(id);
            if (showtime != null)
            {
                _showtimeService.Delete(showtime);
            } else
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ShowtimeExists(Guid id)
        {
            return _showtimeService.GetAll().Any(e => e.Id == id);
        }

        private void SetupSelectListForMovieAndRoom()
        {
            var movies = _movieService.GetAll();
            var rooms = _roomService.GetAll();
            ViewData["MovieId"] = new SelectList(movies, "Id", "Title");
            ViewData["RoomId"] = new SelectList(rooms, "Id", "Name");
        }
    }
}
