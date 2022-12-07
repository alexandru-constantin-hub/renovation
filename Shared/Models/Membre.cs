using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Renovation.Shared.Models
{
    public class Membre
    {
        public int Id { get; set; }

        public string? Nom { get; set; }

        public string? Prenom { get; set; }

        public string? Address { get; set; }

        public string? Ville { get; set; }

        public string? CodePostal { get; set; }

        public string? Region { get; set; }

        public string? Telephone { get; set; }
    }
}
