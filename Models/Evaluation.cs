using System;
using System.Collections.Generic;

namespace Renovation.Models;

public partial class Evaluation
{
    public int IdMembre { get; set; }

    public int IdFurniseur { get; set; }

    public int Note { get; set; }

    public string? Commentaire { get; set; }

    public virtual Utilisateur IdFurniseurNavigation { get; set; } = null!;

    public virtual Utilisateur IdMembreNavigation { get; set; } = null!;
}
