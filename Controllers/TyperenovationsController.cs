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
    public class TyperenovationsController : Controller
    {
        private readonly RenovationFinaleContext _context;

        public TyperenovationsController(RenovationFinaleContext context)
        {
            _context = context;
        }

        // GET: Typerenovations
        public async Task<IActionResult> Index()
        {
              return View(await _context.Typerenovations.ToListAsync());
        }

        // GET: Typerenovations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Typerenovations == null)
            {
                return NotFound();
            }

            var typerenovation = await _context.Typerenovations
                .FirstOrDefaultAsync(m => m.IdTypeRenovation == id);
            if (typerenovation == null)
            {
                return NotFound();
            }

            return View(typerenovation);
        }

        // GET: Typerenovations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Typerenovations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titre,Description")] Typerenovation typerenovation)
        {
            if (ModelState.IsValid)
            {
                typerenovation.IdTypeRenovation = _context.Typerenovations.Count() + 1;
                _context.Add(typerenovation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typerenovation);
        }

        // GET: Typerenovations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Typerenovations == null)
            {
                return NotFound();
            }

            var typerenovation = await _context.Typerenovations.FindAsync(id);
            if (typerenovation == null)
            {
                return NotFound();
            }
            return View(typerenovation);
        }

        // POST: Typerenovations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTypeRenovation,Titre,Description")] Typerenovation typerenovation)
        {
            if (id != typerenovation.IdTypeRenovation)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typerenovation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TyperenovationExists(typerenovation.IdTypeRenovation))
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
            return View(typerenovation);
        }

        // GET: Typerenovations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Typerenovations == null)
            {
                return NotFound();
            }

            var typerenovation = await _context.Typerenovations
                .FirstOrDefaultAsync(m => m.IdTypeRenovation == id);
            if (typerenovation == null)
            {
                return NotFound();
            }

            return View(typerenovation);
        }

        // POST: Typerenovations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Typerenovations == null)
            {
                return Problem("Entity set 'RenovationFinaleContext.Typerenovations'  is null.");
            }
            var typerenovation = await _context.Typerenovations.FindAsync(id);
            if (typerenovation != null)
            {
                _context.Typerenovations.Remove(typerenovation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TyperenovationExists(int id)
        {
          return _context.Typerenovations.Any(e => e.IdTypeRenovation == id);
        }
    }
}
