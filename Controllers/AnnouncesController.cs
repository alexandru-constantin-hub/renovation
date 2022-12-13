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
    public class AnnouncesController : Controller
    {
        private readonly RenovationFinaleContext _context;

        public AnnouncesController(RenovationFinaleContext context)
        {
            _context = context;
        }

        // GET: Announces
        public async Task<IActionResult> Index()
        {
            var renovationFinaleContext = _context.Announces.Include(a => a.IdDesactivateurNavigation).Include(a => a.IdPieceNavigation).Include(a => a.IdTypeRenovationNavigation).Include(a => a.IdUtilisateurNavigation);
            return View(await renovationFinaleContext.ToListAsync());
        }

        // GET: Announces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Announces == null)
            {
                return NotFound();
            }

            var announce = await _context.Announces
                .Include(a => a.IdDesactivateurNavigation)
                .Include(a => a.IdPieceNavigation)
                .Include(a => a.IdTypeRenovationNavigation)
                .Include(a => a.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(m => m.IdAnnounce == id);
            if (announce == null)
            {
                return NotFound();
            }

            return View(announce);
        }

        // GET: Announces/Create
        public IActionResult Create()
        {
            ViewData["IdDesactivateur"] = new SelectList(_context.Administrateurs, "IdAdministrateur", "IdAdministrateur");
            ViewData["IdPiece"] = new SelectList(_context.Typepieces, "IdPiece", "IdPiece");
            ViewData["IdTypeRenovation"] = new SelectList(_context.Typerenovations, "IdTypeRenovation", "IdTypeRenovation");
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur");
            return View();
        }

        // POST: Announces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAnnounce,Adresse,Duree,Dimensions,AutreInformations,Etat,IdUtilisateur,IdPiece,IdTypeRenovation,IdDesactivateur")] Announce announce)
        {
            if (ModelState.IsValid)
            {
                _context.Add(announce);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDesactivateur"] = new SelectList(_context.Administrateurs, "IdAdministrateur", "IdAdministrateur", announce.IdDesactivateur);
            ViewData["IdPiece"] = new SelectList(_context.Typepieces, "IdPiece", "IdPiece", announce.IdPiece);
            ViewData["IdTypeRenovation"] = new SelectList(_context.Typerenovations, "IdTypeRenovation", "IdTypeRenovation", announce.IdTypeRenovation);
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur", announce.IdUtilisateur);
            return View(announce);
        }

        // GET: Announces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Announces == null)
            {
                return NotFound();
            }

            var announce = await _context.Announces.FindAsync(id);
            if (announce == null)
            {
                return NotFound();
            }
            ViewData["IdDesactivateur"] = new SelectList(_context.Administrateurs, "IdAdministrateur", "IdAdministrateur", announce.IdDesactivateur);
            ViewData["IdPiece"] = new SelectList(_context.Typepieces, "IdPiece", "IdPiece", announce.IdPiece);
            ViewData["IdTypeRenovation"] = new SelectList(_context.Typerenovations, "IdTypeRenovation", "IdTypeRenovation", announce.IdTypeRenovation);
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur", announce.IdUtilisateur);
            return View(announce);
        }

        // POST: Announces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAnnounce,Adresse,Duree,Dimensions,AutreInformations,Etat,IdUtilisateur,IdPiece,IdTypeRenovation,IdDesactivateur")] Announce announce)
        {
            if (id != announce.IdAnnounce)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(announce);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnounceExists(announce.IdAnnounce))
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
            ViewData["IdDesactivateur"] = new SelectList(_context.Administrateurs, "IdAdministrateur", "IdAdministrateur", announce.IdDesactivateur);
            ViewData["IdPiece"] = new SelectList(_context.Typepieces, "IdPiece", "IdPiece", announce.IdPiece);
            ViewData["IdTypeRenovation"] = new SelectList(_context.Typerenovations, "IdTypeRenovation", "IdTypeRenovation", announce.IdTypeRenovation);
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur", announce.IdUtilisateur);
            return View(announce);
        }

        // GET: Announces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Announces == null)
            {
                return NotFound();
            }

            var announce = await _context.Announces
                .Include(a => a.IdDesactivateurNavigation)
                .Include(a => a.IdPieceNavigation)
                .Include(a => a.IdTypeRenovationNavigation)
                .Include(a => a.IdUtilisateurNavigation)
                .FirstOrDefaultAsync(m => m.IdAnnounce == id);
            if (announce == null)
            {
                return NotFound();
            }

            return View(announce);
        }

        // POST: Announces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Announces == null)
            {
                return Problem("Entity set 'RenovationFinaleContext.Announces'  is null.");
            }
            var announce = await _context.Announces.FindAsync(id);
            if (announce != null)
            {
                _context.Announces.Remove(announce);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnounceExists(int id)
        {
          return _context.Announces.Any(e => e.IdAnnounce == id);
        }
    }
}
