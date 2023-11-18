using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Models;
using ProyectoVentas.Models.Entidades;

namespace ProyectoVentas.Controllers
{
    public class VentasController : Controller
    {
        private readonly VentasContext _context;

        public VentasController(VentasContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Ventas
                .Include(sd => sd.DetalleVentas)                
                .ThenInclude(sd => sd.Producto)
                .Include(sd => sd.Usuario)
                .ToListAsync());
        }

        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Venta detalle = await _context.Ventas
                .Include(sd => sd.DetalleVentas)
                .ThenInclude(sd => sd.Producto)
                .Include(sd => sd.Usuario)                
                .FirstOrDefaultAsync(s => s.Id == id);
            if (detalle == null)
            {
                return NotFound();
            }

            return View(detalle);
        }

    }
}
