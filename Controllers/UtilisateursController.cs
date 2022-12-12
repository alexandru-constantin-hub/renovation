﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Renovation.Models;
using System.Diagnostics;

namespace Renovation.Controllers
{
    public class UtilisateursController : Controller
    {
        private readonly RenovationContext _context;

        public UtilisateursController(RenovationContext context)
        {
            _context = context;
        }

        // GET: Utilisateurs
        public async Task<IActionResult> Index()
        {
              return View(await _context.Utilisateurs.ToListAsync());
        }

        // GET: Utilisateurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Utilisateurs == null)
            {
                return NotFound();
            }

            var utilisateur = await _context.Utilisateurs
                .FirstOrDefaultAsync(m => m.IdUtilisateur == id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        // GET: Utilisateurs/Create
        public IActionResult RegisterMembre()
        {
            return View();
        }

        // POST: Utilisateurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterMembre([Bind("IdUtilisateur,Email,MotDePasse,Etat,Role,IdActivateur,IdDesactivateur")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                utilisateur.Role = "Membre";
                utilisateur.Etat = false;
                _context.Add(utilisateur);
                await _context.SaveChangesAsync();
                ViewBag.Message = new { Text = "Inscrivez-vous avec succès", CssClass = "alert alert-success" };
                return View();
            }
            ViewBag.Message = new { Text = "Il ya un problème...", CssClass = "alert alert-danger" };
            return View(utilisateur);
        }



        // GET: Utilisateurs/Create
        public IActionResult RegisterFurniseur()
        {
            return View();
        }
       
        // POST: Utilisateurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterFurniseur([Bind("IdUtilisateur,Email,MotDePasse,Etat,Role,IdActivateur,IdDesactivateur")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                utilisateur.Role = "Furniseur";
                utilisateur.Etat = false;
                _context.Add(utilisateur);
                await _context.SaveChangesAsync();
                ViewBag.Message = new { Text = "Inscrivez-vous avec succès", CssClass = "alert alert-success" };
                return View();
            }
            
            ViewBag.Message = new { Text = "Il ya un problème...", CssClass = "alert alert-danger" };
            return View(utilisateur);
        }


        // GET: Utilisateurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Utilisateurs == null)
            {
                return NotFound();
            }

            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }
            return View(utilisateur);
        }

        // POST: Utilisateurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUtilisateur,Email,MotDePasse,Etat,Role,IdActivateur,IdDesactivateur")] Utilisateur utilisateur)
        {
            if (id != utilisateur.IdUtilisateur)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utilisateur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilisateurExists(utilisateur.IdUtilisateur))
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
            return View(utilisateur);
        }

        // GET: Utilisateurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Utilisateurs == null)
            {
                return NotFound();
            }

            var utilisateur = await _context.Utilisateurs
                .FirstOrDefaultAsync(m => m.IdUtilisateur == id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        // POST: Utilisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Utilisateurs == null)
            {
                return Problem("Entity set 'RenovationContext.Utilisateurs'  is null.");
            }
            var utilisateur = await _context.Utilisateurs.FindAsync(id);
            if (utilisateur != null)
            {
                _context.Utilisateurs.Remove(utilisateur);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilisateurExists(int id)
        {
          return _context.Utilisateurs.Any(e => e.IdUtilisateur == id);
        }

      

    }
}