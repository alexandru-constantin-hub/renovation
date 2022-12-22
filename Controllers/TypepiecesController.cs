using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RenovationFinale.Models;

namespace RenovationFinale.Controllers
{
    [Authorize]
    public class TypepiecesController : Controller
    {
        private readonly RenovationFinaleContext _context;

        public TypepiecesController(RenovationFinaleContext context)
        {
            _context = context;
        }

        // GET: Typepieces
        public async Task<IActionResult> Index()
        {
              return View(await _context.Typepieces.ToListAsync());
        }

        // GET: Typepieces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Typepieces == null)
            {
                return NotFound();
            }

            var typepiece = await _context.Typepieces
                .FirstOrDefaultAsync(m => m.IdPiece == id);
            if (typepiece == null)
            {
                return NotFound();
            }

            return View(typepiece);
        }

        // GET: Typepieces/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Typepieces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titre,Description")] Typepiece typepiece)
        {
            if (ModelState.IsValid)
            {
                typepiece.IdPiece = _context.Typepieces.Count() + 1;
                _context.Add(typepiece);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typepiece);
        }

        // GET: Typepieces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Typepieces == null)
            {
                return NotFound();
            }

            var typepiece = await _context.Typepieces.FindAsync(id);
            if (typepiece == null)
            {
                return NotFound();
            }
            return View(typepiece);
        }

        // POST: Typepieces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPiece,Titre,Description")] Typepiece typepiece)
        {
            if (id != typepiece.IdPiece)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typepiece);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypepieceExists(typepiece.IdPiece))
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
            return View(typepiece);
        }

        // GET: Typepieces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Typepieces == null)
            {
                return NotFound();
            }

            var typepiece = await _context.Typepieces
                .FirstOrDefaultAsync(m => m.IdPiece == id);
            if (typepiece == null)
            {
                return NotFound();
            }

            return View(typepiece);
        }

        // POST: Typepieces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Typepieces == null)
            {
                return Problem("Entity set 'RenovationFinaleContext.Typepieces'  is null.");
            }
            var typepiece = await _context.Typepieces.FindAsync(id);
            if (typepiece != null)
            {
                _context.Typepieces.Remove(typepiece);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypepieceExists(int id)
        {
          return _context.Typepieces.Any(e => e.IdPiece == id);
        }
    }
}
