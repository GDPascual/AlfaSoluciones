﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class DpascualAlfaSolucionesContext : DbContext
{
    public DpascualAlfaSolucionesContext()
    {
    }

    public DpascualAlfaSolucionesContext(DbContextOptions<DpascualAlfaSolucionesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Beca> Becas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=WIN-PC-ISAAC; Database=DPascualAlfaSoluciones; Trusted_Connection=True; TrustServerCertificate=True; Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.IdAlumno).HasName("PK__Alumno__460B4740314CB3AE");

            entity.ToTable("Alumno");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdBecaNavigation).WithMany(p => p.Alumnos)
                .HasForeignKey(d => d.IdBeca)
                .HasConstraintName("FK__Alumno__IdBeca__3A81B327");
        });

        modelBuilder.Entity<Beca>(entity =>
        {
            entity.HasKey(e => e.IdBeca).HasName("PK__Beca__23D228E0D8545CFA");

            entity.ToTable("Beca");

            entity.Property(e => e.IdBeca).ValueGeneratedOnAdd();
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
