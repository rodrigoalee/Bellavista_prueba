using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PruebaBellaVista.Models;

namespace PruebaBellaVista.Models;

public partial class BellaVistaDbContext : DbContext
{
    public BellaVistaDbContext()
    {
    }

    public BellaVistaDbContext(DbContextOptions<BellaVistaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Tipocafe> Tipocaves { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-M5BHLGN\\SQLEXPRESS;Database=BellaVistaDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__FF341C0DDFAD96BC");

            entity.ToTable("Producto");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdTipocafe).HasColumnName("id_tipocafe");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("nombre_producto");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Presentacion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("presentacion");

            entity.HasOne(d => d.IdTipocafeNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdTipocafe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_producto_tipocafe");
        });

        modelBuilder.Entity<Tipocafe>(entity =>
        {
            entity.HasKey(e => e.IdTipocafe).HasName("PK__tipocafe__07A95F09E1BFA467");

            entity.ToTable("tipocafe");

            entity.Property(e => e.IdTipocafe).HasColumnName("id_tipocafe");
            entity.Property(e => e.NombreCafe)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("nombre_cafe");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
