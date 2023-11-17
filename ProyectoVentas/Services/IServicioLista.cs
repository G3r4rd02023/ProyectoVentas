using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProyectoVentas.Services
{
    public interface IServicioLista
    {
        Task<IEnumerable<SelectListItem>> GetListaCategorias();

    }
}
