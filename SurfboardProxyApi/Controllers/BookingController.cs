using Microsoft.AspNetCore.Mvc;
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
            var surfBoardWebContext = _context.Bookings;

            if (surfBoardWebContext == null)
            {
                return NotFound();
            }

            return Ok(await surfBoardWebContext.ToListAsync());
        }
    }
}
