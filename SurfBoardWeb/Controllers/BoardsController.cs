using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public BoardsController(SurfBoardWebContext context)
        {
            _context = context;
        }

        // GET: Boards
        public async Task<IActionResult> Index(string BoardType, string searchString, string sortOrder)
        {
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
                        || model.Equipment!.Contains(searchString));
                        //||model.Price == double.Parse(searchString));

                // tilføj mere søgefunktioner
            }

            if (!string.IsNullOrEmpty(BoardType))
            {
                boards = boards.Where(x => x.Type == BoardType);
            }

            var BoardTypeVM = new BoardTypeViewModel
            {
                Types = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Boards = await boards.ToListAsync()
            };

            return View(BoardTypeVM);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Length,Width,Thickness,Volume,Type,Price,Equipment,PicturePath")] Board board)
        {
            if (id != board.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(board);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardExists(board.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(board);
        }

        // GET: Boards/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Boards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Board == null)
            {
                return Problem("Entity set 'SurfBoardWebContext.Board'  is null.");
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
