using System.ComponentModel.DataAnnotations;

namespace ProyectoVentas.Models.Entidades
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50,ErrorMessage ="El campo {0} no puede tener más de {1} carácteres")]
        public string Nombre { get; set; }

        public DateTime FechaCreacion { get; set;}


    }
}
