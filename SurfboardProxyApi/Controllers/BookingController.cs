using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SurfboardApi.Data;
using SurfboardApi.Models;
using SurfboardApi.Models.ViewModels;

namespace SurfboardApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : Controller
    {
        #region constructor and variables
        private readonly ILogger<BookingController> _logger;
        private readonly IConfiguration _config;
        private HttpClient _httpClient;
        private readonly SurfBoardApiContext _context;


        public BookingController(IConfiguration config, ILogger<BookingController> logger, HttpClient httpClient, SurfBoardApiContext context)
        {
            _config = config;
            _logger = logger;
            _httpClient = httpClient;
            _context = context;
        }
        #endregion


        [HttpGet, ActionName("GetBookings")]
        public async Task<IActionResult> GetBooking()
        {
            var resultbooking = _context.Bookings;

            if (resultbooking == null)
            {
                return NotFound();
            }

            return Ok(await resultbooking.ToListAsync());
        }


        [HttpPost , ActionName("PostBooking")]
        public async Task<IActionResult> CreateBooking(BookingRequestVM bookings) //booking request viewmodel
        {
            if (ModelState.IsValid)
            {
                foreach (Board board in _context.Board)
                {
                    if (bookings.BoardId == board.BoardId)
                    {
                        board.IsBooked = true;
                        break;
                    }
                }
                try
                {
                    Bookings bookingstest = new Bookings(bookings.StartDate, bookings.EndDate, bookings.UserId, bookings.BoardId);
                    _context.Bookings.Add(bookingstest);
                    await _context.SaveChangesAsync();
                    return Ok(new { Message = "Booking created" });
                }
                catch (Exception ex)
                {
                    return NotFound(ex);
                }
            }
            return BadRequest(new { Message = "Booking not created " });
            //ViewData["SurfboardId"] = new SelectList(_context.Board, "Id", "Name", booking.BoardId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", booking.UserId);
            //return View(booking);

            //not


            //Bookings createdBooking = new Bookings();
            //createdBooking.EndDate = endtime;
            //createdBooking.StartDate = startdate;
            //createdBooking.UserId = userId;
            //createdBooking.BoardId = givenBoard.Id;
        }
    }
}
