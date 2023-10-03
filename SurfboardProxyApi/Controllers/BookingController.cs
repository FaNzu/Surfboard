using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SurfboardApi.Data;
using SurfboardApi.Models;

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


        public BookingController (IConfiguration config,ILogger<BookingController> logger, HttpClient httpClient, SurfBoardApiContext context)
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
            var resultbooking= _context.Bookings;

            if (resultbooking == null)
            {
                return NotFound();
            }

            return Ok(await resultbooking.ToListAsync());
        }


        [HttpPost, ActionName("PostBooking")]
        public async Task<IActionResult> CreateBooking(Bookings givenBooking, Board givenBoard, string userId) //booking request viewmodel
        {
            if (ModelState.IsValid)
            {
                var surfboardExist = _context.Board.Where(x => x.Id == givenBoard.Id);
                if (surfboardExist.First().IsBooked == true)
                {
                    //ViewBag.Message = "This surfboard is booked";
                    return BadRequest(givenBoard);
                }
                else
                {
                    _context.Add(givenBooking);
                    givenBooking.BoardId = givenBoard.Id;

                    //if userid == nul return bad request
                    givenBooking.UserId = userId;

                    
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            //ViewData["SurfboardId"] = new SelectList(_context.Board, "Id", "Name", booking.BoardId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", booking.UserId);
            //return View(booking);

            //not


            //Bookings createdBooking = new Bookings();
            //createdBooking.EndDate = endtime;
            //createdBooking.StartDate = startdate;
            //createdBooking.UserId = userId;
            //createdBooking.BoardId = givenBoard.Id;



            return NotFound();
        }
    }
}
