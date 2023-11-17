using System.ComponentModel.DataAnnotations;

namespace ProyectoVentas.Models.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(1000, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        public string Clave { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        public string Rol { get; set; }

        [Display(Name = "Imagen")]
        public string URLFotoPerfil { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
