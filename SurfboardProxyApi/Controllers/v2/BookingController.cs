using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SurfboardApi.Data;
using SurfboardApi.Models;
using SurfboardApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace SurfboardApi.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("2.0")]
	//[ApiVersion("2.1")]
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

        #region GET - Bookings
        [HttpGet("GetBookings"), ActionName("GetBookings")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> GetBooking()
        {
            var resultbooking = _context.Bookings;

            if (resultbooking == null)
            {
                return NotFound();
            }

            return Ok(await resultbooking.ToListAsync());
        }
        #endregion

        #region POST - CREATE Booking
        [HttpPost("Create") , ActionName("PostBooking")]
        public async Task<IActionResult> CreateBooking(BookingRequestVM bookings) //booking request viewmodel
        {
            if (ModelState.IsValid) // hvis bookings ikke er gyldig eller er premium
            {
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
            return BadRequest(new { Message = "Booking not created or user not authanticated" });
        }
        #endregion
    }
}
