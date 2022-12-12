using System;
using System.Collections.Generic;

namespace RenovationFinale.Models;

public partial class Announce
{
    public int IdAnnounce { get; set; }

    public string? Adresse { get; set; }

    public decimal? Duree { get; set; }

    public decimal Dimensions { get; set; }

    public string AutreInformations { get; set; } = null!;

    public string? Etat { get; set; }

    public int? IdUtilisateur { get; set; }

    public int? IdPiece { get; set; }

    public int? IdTypeRenovation { get; set; }

    public int? IdDesactivateur { get; set; }

    public virtual Administrateur? IdDesactivateurNavigation { get; set; }

    public virtual Typepiece? IdPieceNavigation { get; set; }

    public virtual Typerenovation? IdTypeRenovationNavigation { get; set; }

    public virtual Utilisateur? IdUtilisateurNavigation { get; set; }

    public virtual ICollection<Offre> Offres { get; } = new List<Offre>();
}
