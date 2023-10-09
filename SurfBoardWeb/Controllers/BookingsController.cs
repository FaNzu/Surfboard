using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SurfBoardWeb.Data;
using SurfBoardWeb.Models;
using SurfBoardWeb.Models.ViewModels;
using static System.Net.WebRequestMethods;

namespace SurfBoardWeb.Controllers
{
    public class BookingsController : Controller
    {
        private readonly SurfBoardWebContext _context;
        private readonly UserManager<DefaultUser> _userManager;
		private readonly HttpClient _httpClient;

		public BookingsController(SurfBoardWebContext context, UserManager<DefaultUser> userManager, HttpClient httpClient)
		{
			_context = context;
			_userManager = userManager;
			_httpClient = httpClient;
		}

		// GET: Bookings
		public async Task<IActionResult> Index()
        {
            var surfBoardWebContext = _context.Bookings.Include(b => b.UserId);
            return View(await surfBoardWebContext.ToListAsync());
        }



        // GET: Bookings/Create
        public IActionResult Create(int id)
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["BoardId"] = id;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StartDate,EndDate,UserId,BoardId,email,phoneNumber")] BookingRequestVM booking, int id) //Ændre alt til BookingRequestVM requestedVM
        {
            //booking.BoardId = id;
            if (ModelState.IsValid)
            {
                Bookings createdBooking;

                if (User.Identity.Name == null)
                {
                    //create seudo user with manager
                    DefaultUser newUser = new DefaultUser();
                    newUser.Email = booking.email;
                    newUser.PhoneNumber = booking.phoneNumber;
                    await _userManager.CreateAsync(newUser);
                    //kald light version api
                    createdBooking = await _httpClient.GetFromJsonAsync<Bookings>(@"");
                    //ændre til rigtig 


                }

                else
                {
                    //hvodan sender man info med?
                    createdBooking = await _httpClient.GetFromJsonAsync<Bookings>(@"");

                    string userName = User.Identity.Name;

                    foreach (IdentityUser user in _userManager.Users)
                    {
                        if (user.UserName == userName)
                        {
						    createdBooking.UserId = user.Id;
                            break;
                        }
                    }
                }

                //ændre board til at være booket
                foreach (Board surfboard in _context.Board)
                {
                    if (booking.BoardId == surfboard.BoardId)
                    {
                        surfboard.IsBooked = true;
                        break;
                    }
                }
                
                return Redirect("/Boards");
            }

            return BadRequest(ModelState);
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
                .FirstOrDefaultAsync(m => m.BookingsId == id);

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
                    if (booking.BoardId == surfboard.BoardId)
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
            return _context.Bookings.Any(e => e.BookingsId == id);
        }
    }
}
