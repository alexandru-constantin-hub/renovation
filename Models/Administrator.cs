using System;
using System.Collections.Generic;

namespace Renovation.Models;

public partial class Administrator
{
    public int IdAdministrator { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public virtual ICollection<Announce> Announces { get; } = new List<Announce>();

    public virtual Utilisateur IdAdministratorNavigation { get; set; } = null!;
}
