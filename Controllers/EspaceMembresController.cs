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

            int idUser = int.Parse(Request.Cookies["NameIdentifier"]);
            int announcesNumber = _context.Announces.Include(a => a.IdDesactivateurNavigation).Include(a => a.IdPieceNavigation).Include(a => a.IdTypeRenovationNavigation).Include(a => a.IdUtilisateurNavigation.Membre).Where(a => a.IdUtilisateur == idUser).Count();
            List<JoinAO> offresNumber = _context.Announces.Where(a => a.IdUtilisateur == idUser).Include(a => a.IdPieceNavigation).Include(a => a.IdTypeRenovationNavigation).Join(_context.Offres.Include(a => a.IdFournisseurNavigation), a => a.IdAnnounce, o => o.IdAnnounce, (a, o) => new { a, o }).Select(x => new JoinAO { announceVM = x.a, offreVM = x.o }).ToList();
            int offresNumberAttendre = offresNumber.Where(e => e.offreVM.Etat == "Attendre").Count();
            int offresNumberAccepte = offresNumber.Where(e => e.offreVM.Etat == "Accepte").Count();
            int offresNumberRefuse = offresNumber.Where(e => e.offreVM.Etat == "Refuse").Count();

            ViewBag.offresNumber = offresNumber.Count();
            ViewBag.offresNumberAttendre = offresNumberAttendre;
            ViewBag.offresNumberAccepte = offresNumberAccepte;
            ViewBag.offresNumberRefuse = offresNumberRefuse;
            ViewBag.announcesNumber = announcesNumber;


            return View();
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
        public async Task<IActionResult> MesAnnounces()
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
        public async Task<IActionResult> EditOffre(int? idAnnounce, int? idFournisseur, string message)
        {
          
            var offre = await _context.Offres.FindAsync(idAnnounce, idFournisseur);
            if (offre == null)
            {
                return NotFound();
            }


            offre.Etat = message;
            _context.Update(offre);
            await _context.SaveChangesAsync();

            return RedirectToAction("Offres");
        }





        // GET: Announces/Edit/5
        public async Task<IActionResult> Desactive(int? id)
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
            announce.Etat = "Désactivé";
            _context.Update(announce);
            await _context.SaveChangesAsync();
            TempData["MessageSuccess"] = "Message désactivé";

            return RedirectToAction("MesAnnounces");
        }


        // GET: Announces/Edit/5
        public async Task<IActionResult> Active(int? id)
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
            announce.Etat = "Activé";
            _context.Update(announce);
            await _context.SaveChangesAsync();
            TempData["MessageSuccess"] = "Message Active";

            return RedirectToAction("MesAnnounces");
        }



        // GET: Announces/Details/5
        public async Task<IActionResult> AnnounceDetails(int? id)
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


        // GET: Announces/Edit/5
        public async Task<IActionResult> AnnounceEdit(int? id)
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
        public async Task<IActionResult> AnnounceEdit(int id, [Bind("IdAnnounce,Adresse,Duree,Dimensions,AutreInformations,Etat,IdUtilisateur,IdPiece,IdTypeRenovation,IdDesactivateur")] Announce announce)
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




        private bool OffreExists(int id)
        {
            return _context.Offres.Any(e => e.IdAnnounce == id);
        }

        private bool AnnounceExists(int id)
        {
            return _context.Announces.Any(e => e.IdAnnounce == id);
        }


    }
}
