using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace S.G.H.Models
{
    public partial class AppDbContext : IdentityDbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Chambre> Chambres { get; set; }
        public virtual DbSet<Docteur> Docteurs { get; set; }
        public virtual DbSet<Facture> Factures { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-PMF7JRN\\SQLEXPRESS;Initial Catalog=S_G_H_DB;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chambre>(entity =>
            {
                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Chambres)
                    .HasForeignKey(d => d.PatientMatricule)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Docteur>(entity =>
            {
                entity.HasKey(e => e.Matricule)
                    .HasName("PK__Docteurs__0FB9FB42F5614EB3");
            });

            modelBuilder.Entity<Facture>(entity =>
            {
                entity.Property(e => e.DatePaiement).HasColumnType("date");

                entity.Property(e => e.Montant).HasColumnType("money");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Factures)
                    .HasForeignKey(d => d.PatientMatricule)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Matricule)
                    .HasName("PK__Patients__0FB9FB4204DF1F0C");

                entity.HasOne(d => d.SonDocteur)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.DocteurMatricule)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
