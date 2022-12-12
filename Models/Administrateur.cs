using System;
using System.Collections.Generic;

namespace RenovationFinale.Models;

public partial class Administrateur
{
    public int IdAdministrateur { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public virtual ICollection<Announce> Announces { get; } = new List<Announce>();

    public virtual Utilisateur IdAdministrateurNavigation { get; set; } = null!;

    public virtual ICollection<Utilisateur> Utilisateurs { get; } = new List<Utilisateur>();
}
