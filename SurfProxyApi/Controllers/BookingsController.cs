using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Create(int id)
        {
            //var surfboardExist = _context.Board.Where(x => x.Id == id);
            //if (surfboardExist.First().IsBooked == true)
            //{
            //    ViewBag.Message = "This surfboard is booked";
            //    return View();
            //}
            //kontrol af hvis booking af boardid findes, hvis boarded allerede har booking fortæl user der er en fejl., hvis ikke fortsæt.
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
                var surfboardExist = _context.Board.Where(x => x.Id == id);
                if (surfboardExist.First().IsBooked == true)
                {
                    ViewBag.Message = "This surfboard is booked";
                    return View(booking);
                }
                else
                {
                    _context.Add(booking);
                    booking.SurfboardId = id;
                    booking.UserName = User.Identity.Name;

                    foreach (IdentityUser user in _userManager.Users)
                    {
                        if (user.UserName == booking.UserName)
                        {
                            booking.UserId = user.Id;
                            break;
                        }
                    }
                    foreach (Board surfboard in _context.Board)
                    {
                        if (booking.SurfboardId == surfboard.Id)
                        {
                            surfboard.IsBooked = true;
                            break;
                        }
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
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

            var booking = await _context.Bookings.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartDate,EndDate,UserId,UserName,SurfboardId")] byte[] rowVersion)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingToUpdate = await _context.Bookings.FirstOrDefaultAsync(i => i.Id == id);
            if (bookingToUpdate == null)
            {
                Bookings deletedBooking = new Bookings();
                await TryUpdateModelAsync(deletedBooking);
                ModelState.AddModelError(string.Empty, "Unable to save changes. The booking was deleted by another user.");
                ViewData["Bookings"] = new SelectList(_context.Bookings, "Id", "Username", deletedBooking.Id);
                return View(deletedBooking);
            }
            _context.Entry(bookingToUpdate).Property("RowVersion").OriginalValue = rowVersion;
            if (await TryUpdateModelAsync<Bookings>(bookingToUpdate, "", s => s.StartDate, s => s.EndDate, s => s.SurfboardId, s => s.UserName))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Bookings)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty, "Unable to save changes. The surfboard was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Bookings)databaseEntry.ToObject();
                        if (databaseValues.StartDate != clientValues.StartDate)
                        {
                            ModelState.AddModelError("Bookings Start Date", $"Current value: {databaseValues.StartDate}");
                        }
                        if (databaseValues.EndDate != clientValues.EndDate)
                        {
                            ModelState.AddModelError("Bookings End Date", $"Current value: {databaseValues.EndDate}");
                        }
                        if (databaseValues.SurfboardId != clientValues.SurfboardId)
                        {
                            ModelState.AddModelError("Surfboard", $"Current value: {databaseValues.SurfboardId}");
                        }
                        if (databaseValues.UserName != clientValues.UserName)
                        {
                            ModelState.AddModelError("Username", $"Current value: {databaseValues.UserName}");
                        }

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                            + "was modified by another user after you got the original value. The"
                            + "edit operation was canceled and the current values in the database "
                            + "have been displayed. If you still want to edit this record, click "
                            + "the Save button again. Otherwise click the Back to List hyperlink.");
                    }
                }
            }
            //ViewData["Bookings"] = new SelectList(_context.Bookings, "Id", "Name", bookingToUpdate.Id);
            return View(bookingToUpdate);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? concurrencyError)
        {
            if (id == null || _context.Bookings == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            if (concurrencyError.GetValueOrDefault())
            {
                ViewData["ConcurrencyErrorMessage"] = "The record you attempted to delete "
                    + "was modified by another user after you got the original values. "
                    + "The delete operation was canceled and the current values in the "
                    + "database have been displayed. If you still want to delete this "
                    + "record, click the Delete button again. Otherwise "
                    + "click the Back to List hyperlink.";
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bookings == null)
            {
                return Problem("Entity set 'Project_ShowtimeContext.Bookings'  is null.");
            }
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                foreach (Board surfboard in _context.Board)
                {
                    if (booking.SurfboardId == surfboard.Id)
                    {
                        surfboard.IsBooked = false;
                        break;
                    }
                }
                _context.Bookings.Remove(booking);
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
