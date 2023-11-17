using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoVentas.Models.Entidades
{
    public class DetalleVenta
    {
        public int Id { get; set; }

        public Venta Venta { get; set;}

        public Producto Producto { get; set;}

        public int Cantidad { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public int Total { get; set; }
    }
}
