using System;
using System.Collections.Generic;

namespace Renovation.Models;

public partial class Membre
{
    public int IdMembre { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string? Adresse { get; set; }

    public decimal? Telephone { get; set; }

    public virtual Utilisateur IdMembreNavigation { get; set; } = null!;
}
