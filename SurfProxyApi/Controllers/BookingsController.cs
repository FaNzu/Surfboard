using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurfProxyApi.Models;
using SurfProxyApi.Data;
using SurfProxyApi.Areas.Identity;
using Microsoft.AspNetCore.Identity;
using SurfBoardWeb.Migrations;
using Microsoft.EntityFrameworkCore;

namespace SurfProxyApi.Controllers
{
    //HTTPGET - Mangler
    //HTTPPOST - Mangler
    //HTTPPUT - Mangler
    //HTTPDELETE - Mangler

    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly SurfBoardWebContext _context;
        private readonly UserManager<DefaultUser> _userManager;

        public BookingsController(SurfBoardWebContext context, UserManager<DefaultUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [ProducesResponseType(typeof(Bookings), 201)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        [HttpPost("CreateBooking", Name = "Create Booking ")]
        public async Task<ActionResult> Create([Bind("BookingStartDate,BookingEndDate,UserId,SurfboardId")] Bookings booking)
        {

            var user = await _userManager.GetUserAsync(User);
            booking.UserId = user.Id;
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok();

        }
        //[HttpGet]
        //public async Task<List<Booking>> Get(Booking booking)
        //{
        //    var user = await _userManager.GetUserAsync(User);

        //    var bookings = from s in _context.
        //                   select s;
        //    bookings = bookings.Where(b => b.UserId == user.Id);
        //    return await bookings.ToListAsync();
        //}
        //[HttpPost("Put")]
        //public async Task<IActionResult> Put(Booking booking)
        //{

        //}
        //[HttpPost("Delete")]
        //public async Task<IActionResult> Delete(Booking booking)
        //{

        //}
    }
}
