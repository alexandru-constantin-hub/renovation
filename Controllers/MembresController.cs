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
    public class MembresController : Controller
    {
        private readonly RenovationContext _context;

        public MembresController(RenovationContext context)
        {
            _context = context;
        }

        // GET: Membres
        public async Task<IActionResult> Index()
        {
            var renovationContext = _context.Membres.Include(m => m.IdMembreNavigation);
            return View(await renovationContext.ToListAsync());
        }

        // GET: Membres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Membres == null)
            {
                return NotFound();
            }

            var membre = await _context.Membres
                .Include(m => m.IdMembreNavigation)
                .FirstOrDefaultAsync(m => m.IdMembre == id);
            if (membre == null)
            {
                return NotFound();
            }

            return View(membre);
        }

        // GET: Membres/Create
        public IActionResult Create()
        {
            ViewData["IdMembre"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur");
            return View();
        }

        // POST: Membres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMembre,Nom,Prenom,Adresse,Telephone")] Membre membre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMembre"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur", membre.IdMembre);
            return View(membre);
        }

        // GET: Membres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Membres == null)
            {
                return NotFound();
            }

            var membre = await _context.Membres.FindAsync(id);
            if (membre == null)
            {
                return NotFound();
            }
            ViewData["IdMembre"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur", membre.IdMembre);
            return View(membre);
        }

        // POST: Membres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMembre,Nom,Prenom,Adresse,Telephone")] Membre membre)
        {
            if (id != membre.IdMembre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembreExists(membre.IdMembre))
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
            ViewData["IdMembre"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur", membre.IdMembre);
            return View(membre);
        }

        // GET: Membres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Membres == null)
            {
                return NotFound();
            }

            var membre = await _context.Membres
                .Include(m => m.IdMembreNavigation)
                .FirstOrDefaultAsync(m => m.IdMembre == id);
            if (membre == null)
            {
                return NotFound();
            }

            return View(membre);
        }

        // POST: Membres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Membres == null)
            {
                return Problem("Entity set 'RenovationContext.Membres'  is null.");
            }
            var membre = await _context.Membres.FindAsync(id);
            if (membre != null)
            {
                _context.Membres.Remove(membre);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembreExists(int id)
        {
          return _context.Membres.Any(e => e.IdMembre == id);
        }
    }
}
