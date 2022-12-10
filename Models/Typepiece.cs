using System;
using System.Collections.Generic;

namespace Renovation.Models;

public partial class Typepiece
{
    public int IdPiece { get; set; }

    public string Titre { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Announce> Announces { get; } = new List<Announce>();
}
