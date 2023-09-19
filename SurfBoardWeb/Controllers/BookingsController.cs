using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SurfBoardWeb.Data;
using SurfBoardWeb.Models;

namespace SurfBoardWeb.Controllers
{
    public class BookingsController : Controller
    {
        private readonly SurfBoardWebContext _context;  
		private readonly UserManager<DefaultUser> _userManager;

		public BookingsController(SurfBoardWebContext context, UserManager<DefaultUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		// GET: Bookings
		public async Task<IActionResult> Index()
        {
            var surfBoardWebContext = _context.Bookings.Include(b => b.User);
            return View(await surfBoardWebContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .Include(b => b.SurfboardId)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["BoardId"] = new SelectList(_context.Board, "Id", "Id");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("BookingStartDate,BookingEndDate,UserId,SurfboardId")] Bookings booking, int id)
		{
			if (ModelState.IsValid)
			{
				_context.Add(booking);
				booking.SurfboardId = id;
				booking.UserName = User.Identity.Name;

				foreach (IdentityUser user in _userManager.Users)
				{
					if (user.UserName == booking.UserName)
					{
						booking.UserId = user.Id;
					}
				}
				foreach (Board surfboard in _context.Board)
				{
					if (booking.SurfboardId == surfboard.Id)
					{
						surfboard.IsBooked = true;
					}
				}
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
            else
            {
                List<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
            }
            ViewData["SurfboardId"] = new SelectList(_context.Board, "Id", "Name", booking.SurfboardId);
			ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", booking.UserId);
			return View(booking);
		}

		// GET: Bookings/Edit/5
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings == null)
            {
                return NotFound();
            }
            ViewData["SurfBoardid"] = new SelectList(_context.Bookings, "Id", "Name", bookings.Id);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bookings.UserId);
            return View(bookings);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,UserId,UserName,SurfboardId")] Bookings bookings)
        {
            if (id != bookings.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingsExists(bookings.Id))
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
			ViewData["SurfboardId"] = new SelectList(_context.Board, "Id", "Name", bookings.SurfboardId);
			ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bookings.UserId);
            return View(bookings);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .Include(bookings => bookings.Id)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bookings == null)
            {
                return Problem("Entity set 'SurfBoardWebContext.Bookings'  is null.");
            }
            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings != null)
            {
                _context.Bookings.Remove(bookings);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingsExists(int id)
        {
          return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
