using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Data;

public partial class PersonaDbContext : DbContext
{
    public PersonaDbContext()
    {
    }

    public PersonaDbContext(DbContextOptions<PersonaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<estudio> estudios { get; set; }

    public virtual DbSet<persona> personas { get; set; }

    public virtual DbSet<profesion> profesions { get; set; }

    public virtual DbSet<telefono> telefonos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=JUAN_CIFUENTES\\SQLEXPRESS01;Database=persona_db;Trusted_Connection=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<estudio>(entity =>
        {
            entity.HasKey(e => new { e.id_prof, e.cc_per });

            entity.Property(e => e.univer).HasMaxLength(50);

            entity.HasOne(d => d.cc_perNavigation).WithMany(p => p.estudios)
                .HasForeignKey(d => d.cc_per)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_estudios_persona");

            entity.HasOne(d => d.id_profNavigation).WithMany(p => p.estudios)
                .HasForeignKey(d => d.id_prof)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_estudios_profesion");
        });

        modelBuilder.Entity<persona>(entity =>
        {
            entity.HasKey(e => e.cc).HasName("PK__persona__3213666D0CD297F7");

            entity.ToTable("persona");

            entity.Property(e => e.cc).ValueGeneratedNever();
            entity.Property(e => e.apellido).HasMaxLength(45);
            entity.Property(e => e.genero)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.nombre).HasMaxLength(45);
        });

        modelBuilder.Entity<profesion>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__profesio__3213E83F524A0AD5");

            entity.ToTable("profesion");

            entity.Property(e => e.id).ValueGeneratedNever();
            entity.Property(e => e.nom).HasMaxLength(90);
        });

        modelBuilder.Entity<telefono>(entity =>
        {
            entity.HasKey(e => e.num).HasName("PK__telefono__DF908D655D2B1433");

            entity.ToTable("telefono");

            entity.Property(e => e.num).HasMaxLength(15);
            entity.Property(e => e.oper).HasMaxLength(45);

            entity.HasOne(d => d.duenioNavigation).WithMany(p => p.telefonos)
                .HasForeignKey(d => d.duenio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_telefono_persona");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
