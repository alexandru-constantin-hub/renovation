using System;
using System.Collections.Generic;

namespace Renovation.Models;

public partial class Furniseur
{
    public int IdFurniseur { get; set; }

    public string Nom { get; set; } = null!;

    public decimal? CodeFiscale { get; set; }

    public string? Adresse { get; set; }

    public decimal? Telephone { get; set; }

    public virtual Utilisateur IdFurniseurNavigation { get; set; } = null!;

    public virtual ICollection<Offre> Offres { get; } = new List<Offre>();
}
