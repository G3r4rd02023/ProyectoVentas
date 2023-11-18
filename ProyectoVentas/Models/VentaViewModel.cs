using ProyectoVentas.Models.Entidades;
using System.ComponentModel.DataAnnotations;

namespace ProyectoVentas.Models
{
    public class VentaViewModel
    {
        public ICollection<DetalleVenta> DetalleVentas { get; set; }
        public Producto Producto { get; set; }
        public int  ProductoId { get; set; }
        public decimal Precio { get; set; }
        public string URLImagen { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Cantidad { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
        public Usuario Usuario { get; set; }
    }
}
