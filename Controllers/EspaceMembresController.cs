using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RenovationFinale.Models;
using System.Linq;
using System.Security.Claims;

namespace RenovationFinale.Controllers
{
    public class EspaceMembresController : Controller
    {

        private readonly RenovationFinaleContext _context;
        

        public EspaceMembresController(RenovationFinaleContext context)
        {
            _context = context;
        }

        public IActionResult Apercu()
        { 
           return View("Apercu");
        }

        // GET: Announces/Create
        public IActionResult CreeAnnounce()
        {
            ViewData["IdDesactivateur"] = new SelectList(_context.Administrateurs, "IdAdministrateur", "IdAdministrateur");
            ViewData["IdPiece"] = new SelectList(_context.Typepieces, "IdPiece", "Titre");
            ViewData["IdTypeRenovation"] = new SelectList(_context.Typerenovations, "IdTypeRenovation", "Titre");
            ViewData["IdUtilisateur"] = new SelectList(_context.Utilisateurs, "IdUtilisateur", "IdUtilisateur");
            return View("NouvelleAnnounce");
        }

        // GET: Announces
        public async Task<IActionResult> MesAnnonces()
        {

            int idUser = int.Parse(Request.Cookies["NameIdentifier"]);
            var renovationFinaleContext = _context.Announces.Include(a => a.IdDesactivateurNavigation).Include(a => a.IdPieceNavigation).Include(a => a.IdTypeRenovationNavigation).Include(a => a.IdUtilisateurNavigation.Membre).Where(a => a.IdUtilisateur == idUser);
            return View(await renovationFinaleContext.ToListAsync());
        }


        

        //GET: List of announcements with offres
        public async Task<IActionResult> Offres()
        {
            int idUser = int.Parse(Request.Cookies["NameIdentifier"]);
            List<JoinAO> renovationFinaleContext = _context.Announces.Where(a => a.IdUtilisateur == idUser).Include(a => a.IdPieceNavigation).Include(a => a.IdTypeRenovationNavigation).Join(_context.Offres.Include(a => a.IdFournisseurNavigation), a => a.IdAnnounce, o => o.IdAnnounce, (a, o) => new { a, o }).Select(x => new JoinAO { announceVM = x.a, offreVM = x.o }).ToList();
            return View(renovationFinaleContext);
        }


        //Accept or not the offer

        // GET: Offres/Edit/5
        public async Task<IActionResult> EditOffre(int? id)
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
            ViewData["IdFournisseur"] = new SelectList(_context.Fournisseurs, "IdUtilisateur", "IdUtilisateur", offre.IdFournisseur);
            return View("EditOffre");
        }

        // POST: Offres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOffre(int id, [Bind("IdAnnounce,IdFournisseur,Budget,Duree,DateCommence,Etat")] Offre offre)
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
            ViewData["IdFournisseur"] = new SelectList(_context.Fournisseurs, "IdUtilisateur", "IdUtilisateur", offre.IdFournisseur);
            return View("Offres");
        }


        private bool OffreExists(int id)
        {
            return _context.Offres.Any(e => e.IdAnnounce == id);
        }


    }
}
