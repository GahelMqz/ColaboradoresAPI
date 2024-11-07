using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace netColaboradores.Models;

public partial class NetExamenContext : DbContext
{
    public NetExamenContext()
    {
    }

    public NetExamenContext(DbContextOptions<NetExamenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrativo> Administrativos { get; set; }

    public virtual DbSet<Colaborador> Colaboradors { get; set; }

    public virtual DbSet<Profesor> Profesors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrativo>(entity =>
        {
            entity.HasKey(e => e.IdAdministrativo).HasName("PK__administ__E2120FB8953F6AE1");

            entity.ToTable("administrativo");

            entity.Property(e => e.IdAdministrativo).HasColumnName("idAdministrativo");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Fkcolaborador).HasColumnName("FKColaborador");
            entity.Property(e => e.Nomina)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("nomina");
            entity.Property(e => e.Puesto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("puesto");

            entity.HasOne(d => d.FkcolaboradorNavigation).WithMany(p => p.Administrativos)
                .HasForeignKey(d => d.Fkcolaborador)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__administr__FKCol__4F7CD00D");
        });

        modelBuilder.Entity<Colaborador>(entity =>
        {
            entity.HasKey(e => e.IdColaborador).HasName("PK__colabora__A6A5C396C1CA1432");

            entity.ToTable("colaborador");

            entity.Property(e => e.IdColaborador).HasColumnName("idColaborador");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IsProfesor).HasColumnName("isProfesor");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Profesor>(entity =>
        {
            entity.HasKey(e => e.IdProfesor).HasName("PK__profesor__E4BBA604529BB66E");

            entity.ToTable("profesor");

            entity.Property(e => e.IdProfesor).HasColumnName("idProfesor");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Departamento)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("departamento");
            entity.Property(e => e.Fkcolaborador).HasColumnName("FKColaborador");

            entity.HasOne(d => d.FkcolaboradorNavigation).WithMany(p => p.Profesors)
                .HasForeignKey(d => d.Fkcolaborador)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__profesor__FKCola__4CA06362");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
