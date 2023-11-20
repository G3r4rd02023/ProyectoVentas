using ProyectoVentas.Models.Entidades;
using ProyectoVentas.Services;

namespace ProyectoVentas.Models
{
    public class CatalogoViewModel
    {
        public int Cantidad { get; set; }

        public PaginatedList<Producto> Productos { get; set; }

        public ICollection<Categoria> Categorias { get; set; }
    }
}
