using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RenovationFinale.Models;

public partial class RenovationFinaleContext : DbContext
{
    public RenovationFinaleContext()
    {
    }

    public RenovationFinaleContext(DbContextOptions<RenovationFinaleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrateur> Administrateurs { get; set; }

    public virtual DbSet<Announce> Announces { get; set; }

    public virtual DbSet<Evaluation> Evaluations { get; set; }

    public virtual DbSet<Fournisseur> Fournisseurs { get; set; }

    public virtual DbSet<Membre> Membres { get; set; }

    public virtual DbSet<Offre> Offres { get; set; }

    public virtual DbSet<Typepiece> Typepieces { get; set; }

    public virtual DbSet<Typerenovation> Typerenovations { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-S4FU1DHS;Database=RenovationFinale;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrateur>(entity =>
        {
            entity.HasKey(e => e.IdAdministrateur).HasName("pk_ADMINISTRATEUR");

            entity.ToTable("ADMINISTRATEUR");

            entity.Property(e => e.IdAdministrateur)
                .ValueGeneratedNever()
                .HasColumnName("idAdministrateur");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nom");
            entity.Property(e => e.Prenom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("prenom");

            entity.HasOne(d => d.IdAdministrateurNavigation).WithOne(p => p.Administrateur)
                .HasForeignKey<Administrateur>(d => d.IdAdministrateur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ADMINISTRATEUR");
        });

        modelBuilder.Entity<Announce>(entity =>
        {
            entity.HasKey(e => e.IdAnnounce).HasName("pk_ANNOUNCE");

            entity.ToTable("ANNOUNCE");

            entity.Property(e => e.IdAnnounce)
                .ValueGeneratedNever()
                .HasColumnName("idAnnounce");
            entity.Property(e => e.Adresse)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("adresse");
            entity.Property(e => e.AutreInformations)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("autreInformations");
            entity.Property(e => e.Dimensions)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("dimensions");
            entity.Property(e => e.Duree)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("duree");
            entity.Property(e => e.Etat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("etat");
            entity.Property(e => e.IdDesactivateur).HasColumnName("idDesactivateur");
            entity.Property(e => e.IdPiece).HasColumnName("idPiece");
            entity.Property(e => e.IdTypeRenovation).HasColumnName("idTypeRenovation");
            entity.Property(e => e.IdUtilisateur).HasColumnName("idUtilisateur");

            entity.HasOne(d => d.IdDesactivateurNavigation).WithMany(p => p.Announces)
                .HasForeignKey(d => d.IdDesactivateur)
                .HasConstraintName("FK__ANNOUNCE__idDesa__37A5467C");

            entity.HasOne(d => d.IdPieceNavigation).WithMany(p => p.Announces)
                .HasForeignKey(d => d.IdPiece)
                .HasConstraintName("FK__ANNOUNCE__idPiec__398D8EEE");

            entity.HasOne(d => d.IdTypeRenovationNavigation).WithMany(p => p.Announces)
                .HasForeignKey(d => d.IdTypeRenovation)
                .HasConstraintName("FK__ANNOUNCE__idType__3A81B327");

            entity.HasOne(d => d.IdUtilisateurNavigation).WithMany(p => p.Announces)
                .HasForeignKey(d => d.IdUtilisateur)
                .HasConstraintName("FK__ANNOUNCE__idUtil__38996AB5");
        });

        modelBuilder.Entity<Evaluation>(entity =>
        {
            entity.HasKey(e => new { e.IdUtilisateur, e.IdFournisseur });

            entity.ToTable("EVALUATION");

            entity.Property(e => e.IdUtilisateur).HasColumnName("idUtilisateur");
            entity.Property(e => e.IdFournisseur).HasColumnName("idFournisseur");
            entity.Property(e => e.Commentaire)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("commentaire");
            entity.Property(e => e.Note).HasColumnName("note");

            entity.HasOne(d => d.IdFournisseurNavigation).WithMany(p => p.Evaluations)
                .HasForeignKey(d => d.IdFournisseur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EVALUATIO__idFou__45F365D3");

            entity.HasOne(d => d.IdUtilisateurNavigation).WithMany(p => p.Evaluations)
                .HasForeignKey(d => d.IdUtilisateur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EVALUATIO__idUti__44FF419A");
        });

        modelBuilder.Entity<Fournisseur>(entity =>
        {
            entity.HasKey(e => e.IdUtilisateur).HasName("pk_FOURNISSEUR");

            entity.ToTable("FOURNISSEUR");

            entity.HasIndex(e => e.CodeFiscale, "UQ__FOURNISS__C5DCB742E4BABA5D").IsUnique();

            entity.Property(e => e.IdUtilisateur)
                .ValueGeneratedNever()
                .HasColumnName("idUtilisateur");
            entity.Property(e => e.Adresse)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("adresse");
            entity.Property(e => e.CodeFiscale)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("codeFiscale");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nom");
            entity.Property(e => e.Telephone)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("telephone");

            entity.HasOne(d => d.IdUtilisateurNavigation).WithOne(p => p.Fournisseur)
                .HasForeignKey<Fournisseur>(d => d.IdUtilisateur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_FOURNISSEUR");
        });

        modelBuilder.Entity<Membre>(entity =>
        {
            entity.HasKey(e => e.IdUtilisateur).HasName("pk_MEMBRE");

            entity.ToTable("MEMBRE");

            entity.Property(e => e.IdUtilisateur)
                .ValueGeneratedNever()
                .HasColumnName("idUtilisateur");
            entity.Property(e => e.Adresse)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("adresse");
            entity.Property(e => e.Nom)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("nom");
            entity.Property(e => e.Prenom)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("prenom");
            entity.Property(e => e.Telephone)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("telephone");

            entity.HasOne(d => d.IdUtilisateurNavigation).WithOne(p => p.Membre)
                .HasForeignKey<Membre>(d => d.IdUtilisateur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_MEMBRE");
        });

        modelBuilder.Entity<Offre>(entity =>
        {
            entity.HasKey(e => new { e.IdAnnounce, e.IdFournisseur });

            entity.ToTable("OFFRE");

            entity.Property(e => e.IdAnnounce).HasColumnName("idAnnounce");
            entity.Property(e => e.IdFournisseur).HasColumnName("idFournisseur");
            entity.Property(e => e.Budget)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("budget");
            entity.Property(e => e.DateCommence)
                .HasColumnType("date")
                .HasColumnName("dateCommence");
            entity.Property(e => e.Duree).HasColumnName("duree");
            entity.Property(e => e.Etat)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("etat");

            entity.HasOne(d => d.IdAnnounceNavigation).WithMany(p => p.Offres)
                .HasForeignKey(d => d.IdAnnounce)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OFFRE__idAnnounc__3E52440B");

            entity.HasOne(d => d.IdFournisseurNavigation).WithMany(p => p.Offres)
                .HasForeignKey(d => d.IdFournisseur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OFFRE__idFournis__3F466844");
        });

        modelBuilder.Entity<Typepiece>(entity =>
        {
            entity.HasKey(e => e.IdPiece).HasName("pk_TYPEPIECE");

            entity.ToTable("TYPEPIECE");

            entity.Property(e => e.IdPiece)
                .ValueGeneratedNever()
                .HasColumnName("idPiece");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Titre)
                .HasMaxLength(225)
                .IsUnicode(false)
                .HasColumnName("titre");
        });

        modelBuilder.Entity<Typerenovation>(entity =>
        {
            entity.HasKey(e => e.IdTypeRenovation).HasName("pk_TYPERENOVATION");

            entity.ToTable("TYPERENOVATION");

            entity.Property(e => e.IdTypeRenovation)
                .ValueGeneratedNever()
                .HasColumnName("idTypeRenovation");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Titre)
                .HasMaxLength(225)
                .IsUnicode(false)
                .HasColumnName("titre");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.IdUtilisateur).HasName("pk_UTILISATEUR");

            entity.ToTable("UTILISATEUR");

            entity.HasIndex(e => e.Email, "UQ__UTILISAT__AB6E61640513D537").IsUnique();

            entity.Property(e => e.IdUtilisateur)
                .ValueGeneratedNever()
                .HasColumnName("idUtilisateur");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Etat).HasColumnName("etat");
            entity.Property(e => e.IdActivateur).HasColumnName("idActivateur");
            entity.Property(e => e.IdDesactivateur).HasColumnName("idDesactivateur");
            entity.Property(e => e.MotDePasse)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("motDePasse");
            entity.Property(e => e.Role)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("role");

            entity.HasOne(d => d.IdActivateurNavigation).WithMany(p => p.Utilisateurs)
                .HasForeignKey(d => d.IdActivateur)
                .HasConstraintName("Fk_UtilisateurAdminstrateur");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
