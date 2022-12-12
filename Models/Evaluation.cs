using System;
using System.Collections.Generic;

namespace RenovationFinale.Models;

public partial class Evaluation
{
    public int IdUtilisateur { get; set; }

    public int IdFournisseur { get; set; }

    public int Note { get; set; }

    public string? Commentaire { get; set; }

    public virtual Fournisseur IdFournisseurNavigation { get; set; } = null!;

    public virtual Membre IdUtilisateurNavigation { get; set; } = null!;
}
