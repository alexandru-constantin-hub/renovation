using System;
using System.Collections.Generic;

namespace RenovationFinale.Models;

public partial class Offre
{
    public int IdAnnounce { get; set; }

    public int IdFournisseur { get; set; }

    public decimal? Budget { get; set; }

    public int? Duree { get; set; }

    public DateTime? DateCommence { get; set; }

    public string? Etat { get; set; }

    public virtual Announce IdAnnounceNavigation { get; set; } = null!;

    public virtual Fournisseur IdFournisseurNavigation { get; set; } = null!;
}
