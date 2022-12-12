using System;
using System.Collections.Generic;

namespace RenovationFinale.Models;

public partial class Utilisateur
{
    public int IdUtilisateur { get; set; }

    public string Email { get; set; } = null!;

    public string MotDePasse { get; set; } = null!;

    public bool Etat { get; set; }

    public string Role { get; set; } = null!;

    public int? IdActivateur { get; set; }

    public int? IdDesactivateur { get; set; }

    public virtual Administrateur? Administrateur { get; set; }

    public virtual ICollection<Announce> Announces { get; } = new List<Announce>();

    public virtual Fournisseur? Fournisseur { get; set; }

    public virtual Administrateur? IdActivateurNavigation { get; set; }

    public virtual Membre? Membre { get; set; }
}
