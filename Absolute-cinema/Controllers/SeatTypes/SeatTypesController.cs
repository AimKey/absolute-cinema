using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DataAccessObjects;

namespace Absolute_cinema.Controllers.SeatTypes
{
    public class SeatTypesController : Controller
    {
        private readonly AbsoluteCinemaContext _context;

        public SeatTypesController(AbsoluteCinemaContext context)
        {
            _context = context;
        }

        // GET: SeatTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.SeatTypes.ToListAsync());
        }

        // GET: SeatTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seatType = await _context.SeatTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seatType == null)
            {
                return NotFound();
            }

            return View(seatType);
        }

        // GET: SeatTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SeatTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PriceMutiplier,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,RemovedAt,RemovedBy")] SeatType seatType)
        {
            if (ModelState.IsValid)
            {
                seatType.Id = Guid.NewGuid();
                _context.Add(seatType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seatType);
        }

        // GET: SeatTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seatType = await _context.SeatTypes.FindAsync(id);
            if (seatType == null)
            {
                return NotFound();
            }
            return View(seatType);
        }

        // POST: SeatTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,PriceMutiplier,CreatedAt,CreatedBy,UpdatedAt,UpdatedBy,RemovedAt,RemovedBy")] SeatType seatType)
        {
            if (id != seatType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seatType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeatTypeExists(seatType.Id))
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
            return View(seatType);
        }

        // GET: SeatTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seatType = await _context.SeatTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seatType == null)
            {
                return NotFound();
            }

            return View(seatType);
        }

        // POST: SeatTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var seatType = await _context.SeatTypes.FindAsync(id);
            if (seatType != null)
            {
                _context.SeatTypes.Remove(seatType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeatTypeExists(Guid id)
        {
            return _context.SeatTypes.Any(e => e.Id == id);
        }
    }
}
