using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Models.Entidades;

namespace ProyectoVentas.Models
{
    public class VentasContext : DbContext
    {
        public VentasContext(DbContextOptions<VentasContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios {  get; set; }
        public DbSet<Venta> Ventas {  get; set; }
        public DbSet<DetalleVenta> DetalleVentas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Categoria>().HasIndex(c => c.Nombre).IsUnique();
            modelBuilder.Entity<Producto>().HasIndex(c => c.Nombre).IsUnique();
        }


    }
}
