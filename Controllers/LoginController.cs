using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RenovationFinale.Models;
using System.Security.Claims;

namespace RenovationFinale.Controllers
{
    public class LoginController : Controller
    { 

        private readonly RenovationFinaleContext _context;
        public LoginController(RenovationFinaleContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Utilisateurs.FirstOrDefaultAsync(m => m.Email == email && m.MotDePasse == password);


            if (user != null && (user.Etat == true))
            {

                var userActiveMembre = await _context.Membres.FirstOrDefaultAsync(m => m.IdUtilisateur == user.IdUtilisateur);
                var userActiveFurniseur = await _context.Fournisseurs.FirstOrDefaultAsync(m => m.IdUtilisateur == user.IdUtilisateur);
                var userActiveAdministrateur = await _context.Administrateurs.FirstOrDefaultAsync(m => m.IdAdministrateur == user.IdUtilisateur);

                Response.Cookies.Append("logedIn", "true");


                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.IdUtilisateur.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties();

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();

                Response.Cookies.Append("role", user.Role);
                Response.Cookies.Append("nameIdentifier", user.IdUtilisateur.ToString());



                if (user.Role == "Administrateur")
                {

                    if (userActiveAdministrateur == null) {
                        return RedirectToAction("Create", "Administrateurs");
                    }
                    Response.Cookies.Append("name", userActiveAdministrateur.Nom);
                    return RedirectToAction("Index", "Administrateurs");
                }
                
                if (user.Role == "Membre")
                {
                    if (userActiveMembre == null)
                    {
                        return RedirectToAction("Create", "Utilisateurs");
                    }
                    Response.Cookies.Append("name", userActiveMembre.Nom);

                    return RedirectToAction("Apercu", "EspaceMembres");
                }
                if(user.Role == "Furnisseur")
                {
                    if (userActiveFurniseur == null)
                    {
                        return RedirectToAction("Create", "Furnisseurs");
                    }

                    Response.Cookies.Append("name", userActiveFurniseur.Nom);

                    return RedirectToAction("Index", "Furnisseurs");
                }


                return RedirectToAction("Index", "Utilisateurs");
            }

            if (user != null && (user.Etat == false))
            {
                return RedirectToAction("Desactive", "Login");

            }
           
            return RedirectToAction("Index", "Home");
        }

       

       
        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("logedIn");
            Response.Cookies.Delete("name");
            Response.Cookies.Delete("role");
            Response.Cookies.Delete("nameIdentifier");
            

            return RedirectToAction("Index", "Home");
        }



        public IActionResult Desactive()
        {
            return View();
        }



    }
}
