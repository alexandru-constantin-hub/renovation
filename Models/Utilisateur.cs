using System;
using System.Collections.Generic;

namespace Renovation.Models;

public partial class Utilisateur
{
    public int IdUtilisateur { get; set; }

    public string Email { get; set; } = null!;

    public string MotDePasse { get; set; } = null!;

    public bool Etat { get; set; }

    public string Role { get; set; } = null!;

    public int? IdActivateur { get; set; }

    public int? IdDesactivateur { get; set; }

    public virtual Administrator? Administrator { get; set; }

    public virtual ICollection<Announce> Announces { get; } = new List<Announce>();

    public virtual ICollection<Evaluation> EvaluationIdFurniseurNavigations { get; } = new List<Evaluation>();

    public virtual ICollection<Evaluation> EvaluationIdMembreNavigations { get; } = new List<Evaluation>();

    public virtual Furniseur? Furniseur { get; set; }

    public virtual Membre? Membre { get; set; }
}
