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
		private readonly RoleManager<IdentityRole> _roleManager;

		public BookingsController(SurfBoardWebContext context, UserManager<DefaultUser> userManager, HttpClient httpClient, RoleManager<IdentityRole> roleManager)
		{
			_context = context;
			_userManager = userManager;
			_httpClient = httpClient;
			_roleManager = roleManager;
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
		public async Task<IActionResult> Create([Bind("StartDate,EndDate,UserId,BoardId,email,phoneNumber")] BookingRequestVM booking) //Ændre alt til BookingRequestVM requestedVM
		{
			if (ModelState.IsValid)
			{
				var board = _context.Board.First(a => a.BoardId == booking.BoardId);
				if (board == null) { return BadRequest("boardet du booker findes ikke"); }
				if (User.Identity.Name == null)
				{
					#region CREATES SEUDO USER
					try
					{
						DefaultUser newUser = new DefaultUser();
						newUser.Email = booking.email;
						newUser.UserName = booking.email;
						newUser.PhoneNumber = booking.phoneNumber;
						var result = await _userManager.CreateAsync(newUser);

						if (result.Succeeded)
						{
							var roleName = "User";

							var role = await _roleManager.FindByNameAsync(roleName);
							if (role != null)
							{
								var addToRollR = await _userManager.AddToRoleAsync(newUser, roleName);

								if (!addToRollR.Succeeded)
								{
									foreach (var error in addToRollR.Errors)
									{
										ModelState.AddModelError(string.Empty, error.Description);
									}
									return BadRequest();
								}
							}
						}
						DefaultUser user = await _userManager.FindByEmailAsync(booking.email);
						booking.UserId = user.Id;

					}
					catch
					{
						return BadRequest("failed to save user info");
					}
                    #endregion

                    var response = await _httpClient.PostAsJsonAsync(@"https://localhost:7163/api/v1.0/Booking/Create", booking);
					bool returnValue = response.IsSuccessStatusCode;
					if(returnValue == false)
					{
						return BadRequest("You can only rent 1 board when you are not logged in");
					}
					
				}

				else
				{
					var user = _context.Users.FirstOrDefault(a=>a.UserName == User.Identity.Name);
					booking.UserId = user.Id;

					await _httpClient.PostAsJsonAsync(@"https://localhost:7163/api/v2.0/Booking/Create", booking);
				}

				//ændre board til at være booket
				var tempboard = _context.Board.First(a => a.BoardId == booking.BoardId);
				tempboard.IsBooked = true;
				_context.SaveChanges();

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
				ViewData["ConcurrencyErrorMessage"] = "The record you attempted to delete " + "was modified by another user after you got the original values. "
					+ "The delete operation was canceled and the current values in the " + "database have been displayed. If you still want to delete this "
					+ "record, click the Delete button again. Otherwise " + "click the Back to List hyperlink.";
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
				return Problem("Entity set 'Bookings'  is null.");
			}
			var booking = await _context.Bookings.FindAsync(id);
			if (booking != null)
			{
				var board = _context.Board.First(a => a.BoardId == booking.BoardId);
				board.IsBooked = false;
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
