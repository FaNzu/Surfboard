using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurfProxyApi.Data;
using SurfProxyApi.Models;

namespace SurfProxyApi.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly SurfBoardWebContext _context;

        public BoardController(SurfBoardWebContext context)
        {
            _context = context;
        }

        [ProducesResponseType(typeof(Board), 201)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        [HttpPost("Create", Name = "Create Surfboard")]
        public async Task<ActionResult> Create([FromBody] Board board)
        {
            if (ModelState.IsValid)
            {
                _context.Add(board);
                await _context.SaveChangesAsync();
                return new ObjectResult(board) { StatusCode = StatusCodes.Status201Created };
            }
            return BadRequest();
        }

        [ProducesResponseType(typeof(Board), 201)]
        [ProducesResponseType(typeof(IDictionary<string, string>), 400)]
        [ProducesResponseType(500)]
        [HttpGet("Read", Name = "List of Surfboards")]
        public async Task<ActionResult> Get([FromBody] Board board)
        {
            if (ModelState.IsValid)
            {
                _context.Add(board);
                await _context.SaveChangesAsync();
                return new ObjectResult(board) { StatusCode = StatusCodes.Status201Created };
            }
            return BadRequest();
        }

    }
}
