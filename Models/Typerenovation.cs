using System;
using System.Collections.Generic;

namespace RenovationFinale.Models;

public partial class Typerenovation
{
    public int IdTypeRenovation { get; set; }

    public string Titre { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Announce> Announces { get; } = new List<Announce>();
}
