using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SurfBoardWeb.Data;
using SurfBoardWeb.Models;

namespace SurfBoardWeb.Controllers
{
    public class BoardsController : Controller
    {
        private readonly SurfBoardWebContext _context;
        private readonly UserManager<DefaultUser> _userManager;

        public BoardsController(SurfBoardWebContext context)
        {
            _context = context;
        }

        // GET: Boards
        public async Task<IActionResult> Index(string BoardType, string searchString, string sortOrder, string currentFilter, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;


            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["LengthSortParm"] = sortOrder == "Length" ? "length_desc" : "Length";
            ViewData["WidthSortParm"] = sortOrder == "Width" ? "width_desc" : "Width";
            ViewData["ThicknessSortParm"] = sortOrder == "Thickness" ? "thickness_desc" : "Thickness";
            ViewData["VolumeSortParm"] = sortOrder == "Volume" ? "volume_desc" : "Volume";
            ViewData["TypeSortParm"] = sortOrder == "Type" ? "type_desc" : "Type";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["EquipmentSortParm"] = sortOrder == "Equipment" ? "equipment_desc" : "Equipment";
            ViewData["CurrentFilter"] = searchString;

            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Board
                                            orderby m.Type
                                            select m.Type;
            var boards = from m in _context.Board
                         select m;
            switch (sortOrder)
            {
                case "name_desc":
                    boards = boards.OrderByDescending(m => m.Name);
                    break;
                case "Length":
                    boards = boards.OrderBy(m => m.Length);
                    break;
                case "length_desc":
                    boards = boards.OrderByDescending(m => m.Length);
                    break;
                case "Width":
                    boards = boards.OrderBy(m => m.Width);
                    break;
                case "width_desc":
                    boards = boards.OrderByDescending(m => m.Width);
                    break;
                case "Thickness":
                    boards = boards.OrderBy(m => m.Thickness);
                    break;
                case "thickness_desc":
                    boards = boards.OrderByDescending(m => m.Thickness);
                    break;
                case "Volume":
                    boards = boards.OrderBy(m => m.Volume);
                    break;
                case "volume_desc":
                    boards = boards.OrderByDescending(m => m.Volume);
                    break;
                case "Type":
                    boards = boards.OrderBy(m => m.Type);
                    break;
                case "type_desc":
                    boards = boards.OrderByDescending(m => m.Type);
                    break;
                case "Price":
                    boards = boards.OrderBy(m => m.Price);
                    break;
                case "price_desc":
                    boards = boards.OrderByDescending(m => m.Price);
                    break;
                case "Equipment":
                    boards = boards.OrderBy(m => m.Equipment);
                    break;
                case "equipment_desc":
                    boards = boards.OrderByDescending(m => m.Equipment);
                    break;
                default:
                    boards = boards.OrderBy(m => m.Name);
                    break;
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                boards = boards.Where(model => model.Name!.Contains(searchString)
                        || model.Equipment!.Contains(searchString)
                        || model.Type!.Contains(searchString));

                // tilføj mere søgefunktioner
            }

            if (!string.IsNullOrEmpty(BoardType))
            {
                boards = boards.Where(x => x.Type == BoardType);
            }

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var BoardTypeVM = new BoardTypeViewModel
            {
                Types = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Boards = await boards.ToListAsync()
            };

            int pageSize = 5;



            return View(await PaginatedList<Board>.CreateAsync(boards.AsNoTracking(), pageNumber ?? 1, pageSize));

            //return View(BoardTypeVM);
        }

        // GET: Boards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Board == null)
            {
                return NotFound();
            }

            var board = await _context.Board
                .FirstOrDefaultAsync(m => m.Id == id);
            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }

        // GET: Boards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Boards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Length,Width,Thickness,Volume,Type,Price,Equipment,PicturePath")] Board board)
        {
            if (ModelState.IsValid)
            {
                _context.Add(board);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(board);
        }

        // GET: Boards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Board == null)
            {
                return NotFound();
            }

            var board = await _context.Board.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }
            return View(board);
        }

        // POST: Boards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, byte[] rowVersion)
        {
            if (id == null)
            {
                return NotFound();
            }

            var BoardToUpdate = await _context.Board.FirstOrDefaultAsync(m => m.Id == id);

            if (BoardToUpdate == null)
            {
                Board BoardDepartment = new Board();
                await TryUpdateModelAsync(BoardDepartment);
                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. The department was deleted by another user.");
                return View(BoardDepartment);
            }

            _context.Entry(BoardToUpdate).Property("RowVersion").OriginalValue = rowVersion;

            if (await TryUpdateModelAsync<Board>(
                BoardToUpdate,
                "",
                s => s.Name, s => s.Length, s => s.Width, s => s.Thickness, s => s.Volume, s => s.Type, s => s.Price, s => s.RowVersion, s=>s.Equipment, s => s.PicturePath, s => s.IsBooked))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var exceptionEntry = ex.Entries.Single();
                    var clientValues = (Board)exceptionEntry.Entity;
                    var databaseEntry = exceptionEntry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The department was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Board)databaseEntry.ToObject();

                        if (databaseValues.Name != clientValues.Name)
                        {
                            ModelState.AddModelError("Name", $"Current value: {databaseValues.Name}");
                        }
                        if (databaseValues.Length != clientValues.Length)
                        {
                            ModelState.AddModelError("Length", $"Current value: {databaseValues.Length}");
                        }
                        if (databaseValues.Width != clientValues.Width)
                        {
                            ModelState.AddModelError("Width", $"Current value: {databaseValues.Width}");
                        }
                        if (databaseValues.Thickness != clientValues.Thickness)
                        {
                            ModelState.AddModelError("Thickness", $"Current value: {databaseValues.Thickness}");
                        }
                        if (databaseValues.Volume != clientValues.Volume)
                        {
                            ModelState.AddModelError("Volume", $"Current value: {databaseValues.Volume}");
                        }
                        if (databaseValues.Type != clientValues.Type)
                        {
                            ModelState.AddModelError("Type", $"Current value: {databaseValues.Type}");
                        }
                        if (databaseValues.Price != clientValues.Price)
                        {
                            ModelState.AddModelError("Price", $"Current value: {databaseValues.Price}");
                        }
                        if (databaseValues.Equipment != clientValues.Equipment)
                        {
                            ModelState.AddModelError("Equipment", $"Current value: {databaseValues.Equipment}");
                        }
                        if (databaseValues.PicturePath != clientValues.PicturePath)
                        {
                            ModelState.AddModelError("PicturePath", $"Current value: {databaseValues.PicturePath}");
                        }


                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                                + "was modified by another user after you got the original value. The "
                                + "edit operation was canceled and the current values in the database "
                                + "have been displayed. If you still want to edit this record, click "
                                + "the Save button again. Otherwise click the Back to List hyperlink.");
                        BoardToUpdate.RowVersion = (byte[])databaseValues.RowVersion;
                        ModelState.Remove("RowVersion");
                    }
                }
            }
            return View(BoardToUpdate);
        }
        

        // GET: Boards/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? concurrencyError)
        {
            if (id == null || _context.Board == null)
            {
                return NotFound();
            }

            var surfboard = await _context.Board
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (surfboard == null)
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

            return View(surfboard);
        }

        // POST: Boards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
             if (_context.Board == null)
            {
                return Problem("Entity set 'surfboardwebcontext.Board'  is null.");
            }
            var board = await _context.Board.FindAsync(id);
            if (board != null)
            {
                _context.Board.Remove(board);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoardExists(int id)
        {
          return (_context.Board?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
