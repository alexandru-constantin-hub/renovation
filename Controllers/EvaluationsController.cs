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
    public class EvaluationsController : Controller
    {
        private readonly RenovationContext _context;

        public EvaluationsController(RenovationContext context)
        {
            _context = context;
        }

        // GET: Evaluations
        public async Task<IActionResult> Index()
        {
            var renovationContext = _context.Evaluations.Include(e => e.IdFurniseurNavigation).Include(e => e.IdMembreNavigation);
            return View(await renovationContext.ToListAsync());
        }

        // GET: Evaluations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Evaluations == null)
            {
                return NotFound();
            }

            var evaluation = await _context.Evaluations
                .Include(e => e.IdFurniseurNavigation)
                .Include(e => e.IdMembreNavigation)
                .FirstOrDefaultAsync(m => m.IdMembre == id);
            if (evaluation == null)
            {
                return NotFound();
            }

            return View(evaluation);
        }

        // GET: Evaluations/Create
        public IActionResult Create()
        {
            ViewData["IdFurniseur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur");
            ViewData["IdMembre"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur");
            return View();
        }

        // POST: Evaluations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMembre,IdFurniseur,Note,Commentaire")] Evaluation evaluation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evaluation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFurniseur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur", evaluation.IdFurniseur);
            ViewData["IdMembre"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur", evaluation.IdMembre);
            return View(evaluation);
        }

        // GET: Evaluations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Evaluations == null)
            {
                return NotFound();
            }

            var evaluation = await _context.Evaluations.FindAsync(id);
            if (evaluation == null)
            {
                return NotFound();
            }
            ViewData["IdFurniseur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur", evaluation.IdFurniseur);
            ViewData["IdMembre"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur", evaluation.IdMembre);
            return View(evaluation);
        }

        // POST: Evaluations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMembre,IdFurniseur,Note,Commentaire")] Evaluation evaluation)
        {
            if (id != evaluation.IdMembre)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evaluation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvaluationExists(evaluation.IdMembre))
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
            ViewData["IdFurniseur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur", evaluation.IdFurniseur);
            ViewData["IdMembre"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur", evaluation.IdMembre);
            return View(evaluation);
        }

        // GET: Evaluations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Evaluations == null)
            {
                return NotFound();
            }

            var evaluation = await _context.Evaluations
                .Include(e => e.IdFurniseurNavigation)
                .Include(e => e.IdMembreNavigation)
                .FirstOrDefaultAsync(m => m.IdMembre == id);
            if (evaluation == null)
            {
                return NotFound();
            }

            return View(evaluation);
        }

        // POST: Evaluations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Evaluations == null)
            {
                return Problem("Entity set 'RenovationContext.Evaluations'  is null.");
            }
            var evaluation = await _context.Evaluations.FindAsync(id);
            if (evaluation != null)
            {
                _context.Evaluations.Remove(evaluation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvaluationExists(int id)
        {
          return _context.Evaluations.Any(e => e.IdMembre == id);
        }
    }
}
