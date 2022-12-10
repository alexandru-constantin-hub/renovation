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
    public class OffresController : Controller
    {
        private readonly RenovationContext _context;

        public OffresController(RenovationContext context)
        {
            _context = context;
        }

        // GET: Offres
        public async Task<IActionResult> Index()
        {
            var renovationContext = _context.Offres.Include(o => o.IdAnnounceNavigation).Include(o => o.IdFurniseurNavigation);
            return View(await renovationContext.ToListAsync());
        }

        // GET: Offres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Offres == null)
            {
                return NotFound();
            }

            var offre = await _context.Offres
                .Include(o => o.IdAnnounceNavigation)
                .Include(o => o.IdFurniseurNavigation)
                .FirstOrDefaultAsync(m => m.IdAnnounce == id);
            if (offre == null)
            {
                return NotFound();
            }

            return View(offre);
        }

        // GET: Offres/Create
        public IActionResult Create()
        {
            ViewData["IdAnnounce"] = new SelectList(_context.Announces, "IdAnnounce", "IdAnnounce");
            ViewData["IdFurniseur"] = new SelectList(_context.Furniseurs, "IdFurniseur", "IdFurniseur");
            return View();
        }

        // POST: Offres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAnnounce,IdFurniseur,Buget,Duree,DateCommence,Etat")] Offre offre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(offre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAnnounce"] = new SelectList(_context.Announces, "IdAnnounce", "IdAnnounce", offre.IdAnnounce);
            ViewData["IdFurniseur"] = new SelectList(_context.Furniseurs, "IdFurniseur", "IdFurniseur", offre.IdFurniseur);
            return View(offre);
        }

        // GET: Offres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Offres == null)
            {
                return NotFound();
            }

            var offre = await _context.Offres.FindAsync(id);
            if (offre == null)
            {
                return NotFound();
            }
            ViewData["IdAnnounce"] = new SelectList(_context.Announces, "IdAnnounce", "IdAnnounce", offre.IdAnnounce);
            ViewData["IdFurniseur"] = new SelectList(_context.Furniseurs, "IdFurniseur", "IdFurniseur", offre.IdFurniseur);
            return View(offre);
        }

        // POST: Offres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAnnounce,IdFurniseur,Buget,Duree,DateCommence,Etat")] Offre offre)
        {
            if (id != offre.IdAnnounce)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OffreExists(offre.IdAnnounce))
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
            ViewData["IdAnnounce"] = new SelectList(_context.Announces, "IdAnnounce", "IdAnnounce", offre.IdAnnounce);
            ViewData["IdFurniseur"] = new SelectList(_context.Furniseurs, "IdFurniseur", "IdFurniseur", offre.IdFurniseur);
            return View(offre);
        }

        // GET: Offres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Offres == null)
            {
                return NotFound();
            }

            var offre = await _context.Offres
                .Include(o => o.IdAnnounceNavigation)
                .Include(o => o.IdFurniseurNavigation)
                .FirstOrDefaultAsync(m => m.IdAnnounce == id);
            if (offre == null)
            {
                return NotFound();
            }

            return View(offre);
        }

        // POST: Offres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Offres == null)
            {
                return Problem("Entity set 'RenovationContext.Offres'  is null.");
            }
            var offre = await _context.Offres.FindAsync(id);
            if (offre != null)
            {
                _context.Offres.Remove(offre);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OffreExists(int id)
        {
          return _context.Offres.Any(e => e.IdAnnounce == id);
        }
    }
}
