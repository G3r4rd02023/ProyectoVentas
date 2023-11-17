using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Models;

namespace ProyectoVentas.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly VentasContext _context;

        public CatalogoController(VentasContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Productos.ToListAsync());
        }

        public async Task<IActionResult> Detalles(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

    }
}
