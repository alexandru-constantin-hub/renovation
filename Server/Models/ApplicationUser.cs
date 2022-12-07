using Microsoft.AspNetCore.Identity;

namespace Renovation.Server.Models
{

    public class ApplicationUser : IdentityUser
    {
        public bool Etat { get; set; } = false;
    }
}