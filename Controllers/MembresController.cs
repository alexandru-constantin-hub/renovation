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
    
    public class MembresController : Controller
    {
        private readonly RenovationFinaleContext _context;

        public MembresController(RenovationFinaleContext context)
        {
            _context = context;
        }

        // GET: Membres
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var renovationFinaleContext = _context.Membres.Include(m => m.IdUtilisateurNavigation);
            return View(await renovationFinaleContext.ToListAsync());
        }

        // GET: Membres/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Membres == null)
            {
                return NotFound();
            }

            var membre = await _context.Membres
                .Include(m => m.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(m => m.IdUtilisateur == id);
            if (membre == null)
            {
                return NotFound();
            }

            return View(membre);
        }

        // GET: Membres/Create
        public IActionResult Create()
        {
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur");
            return View();
        }

        // POST: Membres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUtilisateur, Nom,Prenom,Adresse,Telephone")] Membre membre)
        {
            
                _context.Add(membre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur", membre.IdUtilisateur);
            return RedirectToAction("Index", "Login");
        }

        // GET: Membres/Edit/5
        [Authorize]
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
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur", membre.IdUtilisateur);
            return View(membre);
        }

        // POST: Membres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUtilisateur,Nom,Prenom,Adresse,Telephone")] Membre membre)
        {
            if (id != membre.IdUtilisateur)
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
                    if (!MembreExists(membre.IdUtilisateur))
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
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur", membre.IdUtilisateur);
            return View(membre);
        }

        // GET: Membres/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Membres == null)
            {
                return NotFound();
            }

            var membre = await _context.Membres
                .Include(m => m.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(m => m.IdUtilisateur == id);
            if (membre == null)
            {
                return NotFound();
            }

            return View(membre);
        }

        // POST: Membres/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Membres == null)
            {
                return Problem("Entity set 'RenovationFinaleContext.Membres'  is null.");
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
          return _context.Membres.Any(e => e.IdUtilisateur == id);
        }
    }
}
