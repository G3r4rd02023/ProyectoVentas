using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Models;

namespace ProyectoVentas.Services
{
    public class ServicioLista : IServicioLista
    {
        private readonly VentasContext _context;

        public ServicioLista(VentasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetListaCategorias()
        {
            List<SelectListItem> list = await _context.Categorias.Select(x => new SelectListItem
            {
                Text = x.Nombre,
                Value = $"{x.Id}"
            })
               .OrderBy(x => x.Text)
               .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una categoría...]",
                Value = "0"
            });

            return list;
        }
    }
    
}
