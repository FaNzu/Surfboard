using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SurfboardApi.Data;
using SurfboardApi.Models;
using SurfboardApi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Versioning;

//blazor api
namespace SurfboardApi.Controllers.v3
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("3.0")]
	public class BoardController : Controller
    {
        #region constructor and variables
        private readonly ILogger<BoardController> _logger;
        private readonly IConfiguration _config;
        private HttpClient _httpClient;
        private readonly SurfBoardApiContext _context;


        public BoardController(IConfiguration config, ILogger<BoardController> logger, HttpClient httpClient, SurfBoardApiContext context)
        {
            _config = config;
            _logger = logger;
            _httpClient = httpClient;
            _context = context;
        }
        #endregion

        //public ProductModel? Get(string sku)
        //{
        //    return _storageService.Products.FirstOrDefault(p => p.Sku == sku);
        //}

        #region GET - Bookings
        [HttpGet("GetBoard"), ActionName("GetBoard")]
        [MapToApiVersion("3.0")]
        public async Task<IActionResult> GetBoard()
        {
            var resultboard = _context.Board;

            if (resultboard == null)
            {
                return NotFound();
            }

            return Ok(await resultboard.ToListAsync());
        }


        [HttpGet("GetByBoardId"), ActionName("GetByBoardId")]
        [MapToApiVersion("3.0")]
        public async Task<IActionResult> GetByBoardId(int id)
        {

            if (_context.Board == null)
            {
                return Problem("Entity set 'surfboardwebcontext.Board'  is null.");
            }
            var resultboard = await _context.Board.FindAsync(id);

            if (resultboard == null)
            {
                return NotFound();
            }

            return Ok(resultboard);
        }
        #endregion
    }
}
