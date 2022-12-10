using System;
using System.Collections.Generic;

namespace Renovation.Models;

public partial class Offre
{
    public int IdAnnounce { get; set; }

    public int IdFurniseur { get; set; }

    public decimal? Buget { get; set; }

    public int? Duree { get; set; }

    public DateTime? DateCommence { get; set; }

    public string? Etat { get; set; }

    public virtual Announce IdAnnounceNavigation { get; set; } = null!;

    public virtual Furniseur IdFurniseurNavigation { get; set; } = null!;
}
