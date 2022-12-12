using System;
using System.Collections.Generic;

namespace RenovationFinale.Models;

public partial class Fournisseur
{
    public int IdUtilisateur { get; set; }

    public string Nom { get; set; } = null!;

    public decimal? CodeFiscale { get; set; }

    public string? Adresse { get; set; }

    public decimal? Telephone { get; set; }

    public virtual ICollection<Evaluation> Evaluations { get; } = new List<Evaluation>();

    public virtual Utilisateur IdUtilisateurNavigation { get; set; } = null!;

    public virtual ICollection<Offre> Offres { get; } = new List<Offre>();
}
