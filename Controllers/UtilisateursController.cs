using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RenovationFinale.Models;

namespace RenovationFinale.Controllers
{
    
    public class UtilisateursController : Controller
    {
        private readonly RenovationFinaleContext _context;

        public UtilisateursController(RenovationFinaleContext context)
        {
            _context = context;
        }

        // GET: Utilisateurs
        [Authorize]
        public async Task<IActionResult> Index()
        {

            List<Utilisateur> utilisateurs = _context.Utilisateurs.ToList();
            List<Membre> membres = _context.Membres.ToList();
            

            var renovationFinaleContext = _context.Utilisateurs.Join(_context.Membres, u => u.IdUtilisateur, m => m.IdUtilisateur, (u, m) => new JoinUM{ utilisateurVM = u, membreVM = m });
            return View(await renovationFinaleContext.ToListAsync());
        }

        // GET: Utilisateurs/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Utilisateurs == null)
            {
                return NotFound();
            }

            var utilisateur = await _context.Utilisateurs
                .Include(u => u.IdActivateurNavigation)
                .FirstOrDefaultAsync(m => m.IdUtilisateur == id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            return View(utilisateur);
        }

        // GET: Utilisateurs/Create
        public IActionResult Create()
        {
            ViewData["IdActivateur"] = new SelectList(_context.Administrateurs, "IdAdministrateur", "IdAdministrateur");
            return View();
        }

        // POST: Utilisateurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,MotDePasse,Role")] Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {

                var checkUserExist = _context.Utilisateurs.FirstOrDefault(e => e.Email == utilisateur.Email);
                int howMany = _context.Utilisateurs.OrderByDescending(i => i.IdUtilisateur).FirstOrDefault().IdUtilisateur;
                

                if (checkUserExist != null)
                {

                    ViewData["message"] = "alreadyExists";
                    if (utilisateur.Role == "Fournisseur")
                    {
                        return View("CreateFurniseur");
                    }
                    return View("CreateMember");
                }
                int idUtil = howMany + 1;
                Debug.WriteLine("ID USER : " + idUtil);
                Debug.WriteLine("ID USER : " + utilisateur.Role);
                utilisateur.IdUtilisateur = idUtil;
                _context.Add(utilisateur);
                await _context.SaveChangesAsync();
                ViewData["message"] = "createSuccess";
                TempData["idUser"] = idUtil;
                

                if (utilisateur.Role == "Fournisseur")
                {
                    
                    return RedirectToAction("Create", "Fournisseurs");
                }
                
                return RedirectToAction("Create", "Membres");
            }
            Debug.WriteLine("A sarit");
            ViewData["message"] = "createError";
            return View(utilisateur);
        }

        // GET: Utilisateurs/Edit/5
        [Authorize]
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
            ViewData["IdActivateur"] = new SelectList(_context.Administrateurs, "IdAdministrateur", "IdAdministrateur", utilisateur.IdActivateur);
            return View(utilisateur);
        }

        // POST: Utilisateurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
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
            ViewData["IdActivateur"] = new SelectList(_context.Administrateurs, "IdAdministrateur", "IdAdministrateur", utilisateur.IdActivateur);
            return View(utilisateur);
        }

        // GET: Utilisateurs/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Utilisateurs == null)
            {
                return NotFound();
            }

            var utilisateur = await _context.Utilisateurs
                .Include(u => u.IdActivateurNavigation)
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
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Utilisateurs == null)
            {
                return Problem("Entity set 'RenovationFinaleContext.Utilisateurs'  is null.");
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

        public IActionResult CreateFurniseur()
        {
            
            return View();
        }
        
        
        public IActionResult CreateMember() {
            return View();
        }

    }
}
