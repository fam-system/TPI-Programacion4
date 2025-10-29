using Microsoft.EntityFrameworkCore;
using Domain.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Proceso> Procesos { get; set; }
    public DbSet<Archivo> Archivos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Usuario
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Nombre)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(u => u.Apellido)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(u => u.Dni)
                .IsRequired()
                .HasMaxLength(8);
            entity.Property(u => u.Direccion)
                .HasMaxLength(100);
            entity.Property(u => u.Telefono)
                .HasMaxLength(15);
            entity.Property(u => u.FechaIngreso)
                .IsRequired();
            entity.Property(u => u.NombreUsuario)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(u => u.PasswordHash)
                .IsRequired();
            entity.Property(u => u.Estado)
                .IsRequired();
            entity.Property(u => u.Rol)
                .IsRequired();
        });

        // Producto
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);
        });

        // Proceso
        modelBuilder.Entity<Proceso>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(p => p.EstadoProceso)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(p => p.FechaEntrega)
                .IsRequired();
            entity.Property(p => p.CantidadProducto)
                .IsRequired();

            entity.HasOne(p => p.Producto)
                .WithMany(p => p.Procesos)
                .HasForeignKey(p => p.ProductoId);
        });

        // Archivo
        modelBuilder.Entity<Archivo>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Nombre)
                .IsRequired()
                .HasMaxLength(150);

            entity.HasOne(a => a.Producto)
                .WithMany(p => p.Archivos)
                .HasForeignKey(a => a.ProductoId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}