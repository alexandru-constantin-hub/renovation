using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Renovation.Models;

public partial class RenovationContext : DbContext
{
    public RenovationContext()
    {
    }

    public RenovationContext(DbContextOptions<RenovationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<Announce> Announces { get; set; }

    public virtual DbSet<Evaluation> Evaluations { get; set; }

    public virtual DbSet<Furniseur> Furniseurs { get; set; }

    public virtual DbSet<Membre> Membres { get; set; }

    public virtual DbSet<Offre> Offres { get; set; }

    public virtual DbSet<Typepiece> Typepieces { get; set; }

    public virtual DbSet<Typerenovation> Typerenovations { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-S4FU1DHS;Database=Renovation;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.IdAdministrator).HasName("PK__ADMINIST__97EB8096D02A286D");

            entity.ToTable("ADMINISTRATOR");

            entity.Property(e => e.IdAdministrator)
                .ValueGeneratedNever()
                .HasColumnName("idAdministrator");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nom");
            entity.Property(e => e.Prenom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("prenom");

            entity.HasOne(d => d.IdAdministratorNavigation).WithOne(p => p.Administrator)
                .HasForeignKey<Administrator>(d => d.IdAdministrator)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ADMINISTR__idAdm__300424B4");
        });

        modelBuilder.Entity<Announce>(entity =>
        {
            entity.HasKey(e => e.IdAnnounce).HasName("PK__ANNOUNCE__32AD64EBC9CBC65A");

            entity.ToTable("ANNOUNCE");

            entity.Property(e => e.IdAnnounce).HasColumnName("idAnnounce");
            entity.Property(e => e.Adresse)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("adresse");
            entity.Property(e => e.AutreInformation)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("autreInformation");
            entity.Property(e => e.Dimensione)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("dimensione");
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
                .HasConstraintName("FK__ANNOUNCE__idDesa__36B12243");

            entity.HasOne(d => d.IdPieceNavigation).WithMany(p => p.Announces)
                .HasForeignKey(d => d.IdPiece)
                .HasConstraintName("FK__ANNOUNCE__idPiec__38996AB5");

            entity.HasOne(d => d.IdTypeRenovationNavigation).WithMany(p => p.Announces)
                .HasForeignKey(d => d.IdTypeRenovation)
                .HasConstraintName("FK__ANNOUNCE__idType__398D8EEE");

            entity.HasOne(d => d.IdUtilisateurNavigation).WithMany(p => p.Announces)
                .HasForeignKey(d => d.IdUtilisateur)
                .HasConstraintName("FK__ANNOUNCE__idUtil__37A5467C");
        });

        modelBuilder.Entity<Evaluation>(entity =>
        {
            entity.HasKey(e => new { e.IdMembre, e.IdFurniseur });

            entity.ToTable("EVALUATION");

            entity.Property(e => e.IdMembre).HasColumnName("idMembre");
            entity.Property(e => e.IdFurniseur).HasColumnName("idFurniseur");
            entity.Property(e => e.Commentaire)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("commentaire");
            entity.Property(e => e.Note).HasColumnName("note");

            entity.HasOne(d => d.IdFurniseurNavigation).WithMany(p => p.EvaluationIdFurniseurNavigations)
                .HasForeignKey(d => d.IdFurniseur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EVALUATIO__idFur__440B1D61");

            entity.HasOne(d => d.IdMembreNavigation).WithMany(p => p.EvaluationIdMembreNavigations)
                .HasForeignKey(d => d.IdMembre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EVALUATIO__idMem__4316F928");
        });

        modelBuilder.Entity<Furniseur>(entity =>
        {
            entity.HasKey(e => e.IdFurniseur).HasName("PK__FURNISEU__C67CB0D82B962E1C");

            entity.ToTable("FURNISEUR");

            entity.HasIndex(e => e.CodeFiscale, "UQ__FURNISEU__C5DCB742DF7D1F27").IsUnique();

            entity.Property(e => e.IdFurniseur)
                .ValueGeneratedNever()
                .HasColumnName("idFurniseur");
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

            entity.HasOne(d => d.IdFurniseurNavigation).WithOne(p => p.Furniseur)
                .HasForeignKey<Furniseur>(d => d.IdFurniseur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FURNISEUR__idFur__2D27B809");
        });

        modelBuilder.Entity<Membre>(entity =>
        {
            entity.HasKey(e => e.IdMembre).HasName("PK__MEMBRE__7399C5D7017ADE45");

            entity.ToTable("MEMBRE");

            entity.Property(e => e.IdMembre)
                .ValueGeneratedNever()
                .HasColumnName("idMembre");
            entity.Property(e => e.Adresse)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("adresse");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nom");
            entity.Property(e => e.Prenom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("prenom");
            entity.Property(e => e.Telephone)
                .HasColumnType("numeric(10, 0)")
                .HasColumnName("telephone");

            entity.HasOne(d => d.IdMembreNavigation).WithOne(p => p.Membre)
                .HasForeignKey<Membre>(d => d.IdMembre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MEMBRE__idMembre__29572725");
        });

        modelBuilder.Entity<Offre>(entity =>
        {
            entity.HasKey(e => new { e.IdAnnounce, e.IdFurniseur });

            entity.ToTable("OFFRE");

            entity.Property(e => e.IdAnnounce).HasColumnName("idAnnounce");
            entity.Property(e => e.IdFurniseur).HasColumnName("idFurniseur");
            entity.Property(e => e.Buget)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("buget");
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
                .HasConstraintName("FK__OFFRE__idAnnounc__3D5E1FD2");

            entity.HasOne(d => d.IdFurniseurNavigation).WithMany(p => p.Offres)
                .HasForeignKey(d => d.IdFurniseur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OFFRE__idFurnise__3E52440B");
        });

        modelBuilder.Entity<Typepiece>(entity =>
        {
            entity.HasKey(e => e.IdPiece).HasName("PK__TYPEPIEC__9A67589EAC866831");

            entity.ToTable("TYPEPIECE");

            entity.Property(e => e.IdPiece).HasColumnName("idPiece");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Titre)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("titre");
        });

        modelBuilder.Entity<Typerenovation>(entity =>
        {
            entity.HasKey(e => e.IdTypeRenovation).HasName("PK__TYPERENO__E0F654614ECAA1FE");

            entity.ToTable("TYPERENOVATION");

            entity.Property(e => e.IdTypeRenovation).HasColumnName("idTypeRenovation");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Titre)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("titre");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.IdUtilisateur).HasName("PK__UTILISAT__5366DB197EF5076C");

            entity.ToTable("UTILISATEUR");

            entity.HasIndex(e => e.Email, "UQ__UTILISAT__AB6E6164B16ACEA3").IsUnique();

            entity.Property(e => e.IdUtilisateur).HasColumnName("idUtilisateur");
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
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
