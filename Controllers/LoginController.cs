using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Renovation.Models;


namespace Renovation.Controllers
{
    public class LoginController : Controller
    {

        private readonly RenovationContext _context;
        public LoginController(RenovationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,MotDePasse")] Utilisateur utilisateur)
        {

            

            var user = await _context.Utilisateurs.FirstOrDefaultAsync(m => m.Email == utilisateur.Email && m.MotDePasse == utilisateur.MotDePasse);
            
            if (user == null)
            {
                return View("Index");
            }


            if (user.Etat == false)
            {
                return View("Inactive");
            }

            Response.Cookies.Append("active", "yes");
            Response.Cookies.Append("role", user.Role);

            if (user.Role == "Membre")
            {
                var checkAddress = await _context.Membres.FirstOrDefaultAsync(m => m.IdMembre == user.IdUtilisateur);
                if (checkAddress.Adresse == null)
                {
                    return RedirectToAction("Create", "Membres");
                }
                else
                {
                    return RedirectToAction("Index", "Membres");

                }
            }


            if (user.Role == "Furniseur")
            {
                var checkAddress = await _context.Furniseurs.FirstOrDefaultAsync(m => m.IdFurniseur == user.IdUtilisateur);
                if (checkAddress.Adresse == null)
                {
                    return RedirectToAction("Create", "Furniseurs");
                }
                else
                {
                    
                    return RedirectToAction("Index", "Furniseurs");

                }
            }

            if (user.Role == "Admnistrateur")
            {
                return RedirectToAction("Index", "Administrateurs");
            }
           

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Logout() {
            Response.Cookies.Delete("active");
            Response.Cookies.Delete("role");


            return RedirectToAction("Index");
        }


    }
}
