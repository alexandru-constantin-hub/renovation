﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RenovationFinale.Models;

namespace RenovationFinale.Controllers
{
    public class EspaceFournisseursController : Controller
    {

        private readonly RenovationFinaleContext _context;

        public EspaceFournisseursController(RenovationFinaleContext context)
        {
            _context = context;
        }


        public IActionResult Apercu()
        {
            return View();
        }

        // GET: Offres/Create
        public IActionResult CreateOffre(int? id)
        {
            ViewBag.IdAnnounce = id;
            
            return View();
        }

        // POST: Offres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOffre([Bind("IdAnnounce,IdFournisseur,Budget,Duree,DateCommence,Etat")] Offre offre)
        {
          
            
                _context.Add(offre);
                await _context.SaveChangesAsync();
                return RedirectToAction("AnnouncesFourniseur");
            
            //ViewData["IdAnnounce"] = new SelectList(_context.Announces, "IdAnnounce", "IdAnnounce", offre.IdAnnounce);
            //ViewData["IdFournisseur"] = new SelectList(_context.Fournisseurs, "IdUtilisateur", "IdUtilisateur", offre.IdFournisseur);
            //return View(offre);
        }


        // GET: Announces with name for piece and renovation
        public async Task<IActionResult> AnnouncesFourniseur()
        {
            var userId = Int32.Parse(Request.Cookies["nameIdentifier"]);
            //List<JoinAO> renovationFinaleContext1 = _context.Announces.Include(a => a.IdPieceNavigation).Include(a => a.IdTypeRenovationNavigation).Join(_context.Offres.Include(a => a.IdFournisseurNavigation), a => a.IdAnnounce, o => o.IdAnnounce, (a, o) => new { a, o }).Select(x => new JoinAO { announceVM = x.a, offreVM = x.o }).ToList();
            List<JoinAO> renovationFinaleContext = _context.Announces.Include(a => a.IdDesactivateurNavigation).Include(a => a.IdPieceNavigation).Include(a => a.IdTypeRenovationNavigation).Include(a => a.IdUtilisateurNavigation.Membre).Where(e=>e.Etat == "Activé").Join(_context.Offres.Include(a => a.IdFournisseurNavigation).Where(id=>id.IdFournisseur != userId), a => a.IdAnnounce, o => o.IdAnnounce, (a, o) => new { a, o }).Select(x => new JoinAO { announceVM = x.a, offreVM = x.o }).ToList();
            return View(renovationFinaleContext);
        }


        // GET: Announces with name for piece and renovation
        public async Task<IActionResult> OffresFourniseur()
        {
            var userId = Int32.Parse(Request.Cookies["nameIdentifier"]);
            //List<JoinAO> renovationFinaleContext1 = _context.Announces.Include(a => a.IdPieceNavigation).Include(a => a.IdTypeRenovationNavigation).Join(_context.Offres.Include(a => a.IdFournisseurNavigation), a => a.IdAnnounce, o => o.IdAnnounce, (a, o) => new { a, o }).Select(x => new JoinAO { announceVM = x.a, offreVM = x.o }).ToList();
            List<JoinAO> renovationFinaleContext = _context.Announces.Include(a => a.IdDesactivateurNavigation).Include(a => a.IdPieceNavigation).Include(a => a.IdTypeRenovationNavigation).Include(a => a.IdUtilisateurNavigation.Membre).Where(e => e.Etat == "Activé").Join(_context.Offres.Include(a => a.IdFournisseurNavigation).Where(id => id.IdFournisseur == userId), a => a.IdAnnounce, o => o.IdAnnounce, (a, o) => new { a, o }).Select(x => new JoinAO { announceVM = x.a, offreVM = x.o }).ToList();
            return View(renovationFinaleContext);
        }




    }
}
