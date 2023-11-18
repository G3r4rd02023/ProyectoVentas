using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoVentas.Models.Entidades
{
    public class Venta
    {
        public int Id { get; set; }

        public Usuario Usuario { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public int Total { get; set; }

        public DateTime FechaCreacion { get; set; }

        public ICollection<DetalleVenta> DetalleVentas { get; set; }
    }
}
