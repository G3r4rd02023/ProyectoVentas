using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoVentas.Models.Entidades
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(1000, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Column(TypeName = "decimal(10,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Precio { get; set;}

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Column(TypeName = "decimal(10,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal PrecioOferta { get; set; }

        public int Cantidad { get; set; }

        [Display(Name = "Imagen")]
        public string URLImagen { get; set; }

        public DateTime FechaCreacion { get; set; }


    }
}
