using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Renovation.Models;

namespace Renovation.Controllers
{
    public class FurniseursController : Controller
    {
        private readonly RenovationContext _context;

        public FurniseursController(RenovationContext context)
        {
            _context = context;
        }

        // GET: Furniseurs
        public async Task<IActionResult> Index()
        {
            var renovationContext = _context.Furniseurs.Include(f => f.IdFurniseurNavigation);
            return View(await renovationContext.ToListAsync());
        }

        // GET: Furniseurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Furniseurs == null)
            {
                return NotFound();
            }

            var furniseur = await _context.Furniseurs
                .Include(f => f.IdFurniseurNavigation)
                .FirstOrDefaultAsync(m => m.IdFurniseur == id);
            if (furniseur == null)
            {
                return NotFound();
            }

            return View(furniseur);
        }

        // GET: Furniseurs/Create
        public IActionResult Create()
        {
            ViewData["IdFurniseur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur");
            return View();
        }

        // POST: Furniseurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFurniseur,Nom,CodeFiscale,Adresse,Telephone")] Furniseur furniseur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(furniseur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFurniseur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur", furniseur.IdFurniseur);
            return View(furniseur);
        }

        // GET: Furniseurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Furniseurs == null)
            {
                return NotFound();
            }

            var furniseur = await _context.Furniseurs.FindAsync(id);
            if (furniseur == null)
            {
                return NotFound();
            }
            ViewData["IdFurniseur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur", furniseur.IdFurniseur);
            return View(furniseur);
        }

        // POST: Furniseurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFurniseur,Nom,CodeFiscale,Adresse,Telephone")] Furniseur furniseur)
        {
            if (id != furniseur.IdFurniseur)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(furniseur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FurniseurExists(furniseur.IdFurniseur))
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
            ViewData["IdFurniseur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur", furniseur.IdFurniseur);
            return View(furniseur);
        }

        // GET: Furniseurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Furniseurs == null)
            {
                return NotFound();
            }

            var furniseur = await _context.Furniseurs
                .Include(f => f.IdFurniseurNavigation)
                .FirstOrDefaultAsync(m => m.IdFurniseur == id);
            if (furniseur == null)
            {
                return NotFound();
            }

            return View(furniseur);
        }

        // POST: Furniseurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Furniseurs == null)
            {
                return Problem("Entity set 'RenovationContext.Furniseurs'  is null.");
            }
            var furniseur = await _context.Furniseurs.FindAsync(id);
            if (furniseur != null)
            {
                _context.Furniseurs.Remove(furniseur);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FurniseurExists(int id)
        {
          return _context.Furniseurs.Any(e => e.IdFurniseur == id);
        }
    }
}
