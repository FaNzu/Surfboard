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


        [HttpGet("GetBookings"), ActionName("GetBookings")]
        [MapToApiVersion("2.0")]
        public async Task<IActionResult> GetBooking(bool premiumUser)
        {
            var resultbooking = _context.Bookings;

            if (resultbooking == null)
            {
                return NotFound();
            }
            if (!premiumUser) //sikkerheds tjek i database burde bruges. men ikke nødvendigt i minimal viable produkt
            {
                return BadRequest("User not authanticated"); 
            }

            return Ok(await resultbooking.ToListAsync());
        }


		//[HttpGet("GetBookings"), ActionName("GetBookings")]
        //[MapToApiVersion("2.1")]
        //public async Task<IActionResult> GetBookingV21()
		//{
		//	var resultbooking = _context.Bookings;

		//	if (resultbooking == null)
		//	{
		//		return NotFound();
		//	}

		//	return Ok(await resultbooking.ToListAsync());
		//}


		[HttpPost("Create") , ActionName("PostBooking")]
        public async Task<IActionResult> CreateBooking(BookingRequestVM bookings, bool premiumUser) //booking request viewmodel
        {
            if (ModelState.IsValid && premiumUser) // hvis bookings ikke er gyldig eller er premium
            {
                bool boardexists = false;
                foreach (Board board in _context.Board)
                {
                    if (bookings.BoardId == board.BoardId)
                    {
                        board.IsBooked = true;
                        boardexists = true;
                        break;
                    }
                }
                if (!boardexists) { return BadRequest("Board not found with the specified productnumber."); }


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
    }
}
