using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RenovationFinale.Models;

namespace RenovationFinale.Controllers
{
    [Authorize]
    public class AdministrateursController : Controller
    {
        private readonly RenovationFinaleContext _context;

        public AdministrateursController(RenovationFinaleContext context)
        {
            _context = context;
        }

        // GET: Administrateurs
        public async Task<IActionResult> Index()
        {
            var renovationFinaleContext = _context.Administrateurs.Include(a => a.IdAdministrateurNavigation);
            return View(await renovationFinaleContext.ToListAsync());
        }

        // GET: Administrateurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Administrateurs == null)
            {
                return NotFound();
            }

            var administrateur = await _context.Administrateurs
                .Include(a => a.IdAdministrateurNavigation)
                .FirstOrDefaultAsync(m => m.IdAdministrateur == id);
            if (administrateur == null)
            {
                return NotFound();
            }

            return View(administrateur);
        }

        // GET: Administrateurs/Create
        public IActionResult Create()
        {
            ViewData["IdAdministrateur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur");
            return View();
        }

        // POST: Administrateurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAdministrateur,Nom,Prenom")] Administrateur administrateur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(administrateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAdministrateur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur", administrateur.IdAdministrateur);
            return View(administrateur);
        }

        // GET: Administrateurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Administrateurs == null)
            {
                return NotFound();
            }

            var administrateur = await _context.Administrateurs.FindAsync(id);
            if (administrateur == null)
            {
                return NotFound();
            }
            ViewData["IdAdministrateur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur", administrateur.IdAdministrateur);
            return View(administrateur);
        }

        // POST: Administrateurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAdministrateur,Nom,Prenom")] Administrateur administrateur)
        {
            if (id != administrateur.IdAdministrateur)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(administrateur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministrateurExists(administrateur.IdAdministrateur))
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
            ViewData["IdAdministrateur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur", administrateur.IdAdministrateur);
            return View(administrateur);
        }

        // GET: Administrateurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Administrateurs == null)
            {
                return NotFound();
            }

            var administrateur = await _context.Administrateurs
                .Include(a => a.IdAdministrateurNavigation)
                .FirstOrDefaultAsync(m => m.IdAdministrateur == id);
            if (administrateur == null)
            {
                return NotFound();
            }

            return View(administrateur);
        }

        // POST: Administrateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Administrateurs == null)
            {
                return Problem("Entity set 'RenovationFinaleContext.Administrateurs'  is null.");
            }
            var administrateur = await _context.Administrateurs.FindAsync(id);
            if (administrateur != null)
            {
                _context.Administrateurs.Remove(administrateur);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdministrateurExists(int id)
        {
          return _context.Administrateurs.Any(e => e.IdAdministrateur == id);
        }


        //special methods
        [Authorize]
        public IActionResult gestionUtilisateurs()
        {
            
            List<Utilisateur> utilisateurs = _context.Utilisateurs.OrderByDescending(c => c.IdUtilisateur).ToList();
            List<Membre> membres = _context.Membres.OrderByDescending(c => c.IdUtilisateur).ToList();
            List<Fournisseur> fournisseurs = _context.Fournisseurs.OrderByDescending(c => c.IdUtilisateur).ToList();
            
            ViewData["um"] = _context.Utilisateurs.OrderByDescending(c => c.IdUtilisateur).Join(_context.Membres, u => u.IdUtilisateur, m => m.IdUtilisateur, (u, m) => new JoinUM { utilisateurVM = u, membreVM = m });
            ViewData["uf"] = _context.Utilisateurs.OrderByDescending(c => c.IdUtilisateur).Join(_context.Fournisseurs, u => u.IdUtilisateur, f => f.IdUtilisateur, (u, f) => new JoinUF { utilisateurVM = u, fournisseurVM = f });
            return View();
        }


        [Authorize]
        public async Task<IActionResult> Active(int? id, bool etat)
        {

            int idAdmin = Int16.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            if (id == null || _context.Utilisateurs == null)
            {
                return NotFound();
            }

            var utlisateur = await _context.Utilisateurs.FindAsync(id);
            if (utlisateur == null)
            {
                return NotFound();
            }
            utlisateur.Etat = etat;
            if (etat)
            {
                utlisateur.IdActivateur = idAdmin;
            }
            else
            {
                utlisateur.IdDesactivateur = idAdmin;
            }
            
            _context.Update(utlisateur);
            await _context.SaveChangesAsync();

            if (etat) {
                TempData["MessageSuccess"] = "Utilisateur Active";
            } else
            {
                TempData["MessageSuccess"] = "Utilisateur Desactive";
            }
            

            return RedirectToAction("GestionUtilisateurs");
        }

    }
}
