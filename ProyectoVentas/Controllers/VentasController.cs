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
            return View(await _context.DetalleVentas
                .Include(sd => sd.Venta)                
                .ThenInclude(sd => sd.Usuario)
                .Include(sd => sd.Producto)
                .ToListAsync());
        }

        public async Task<IActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DetalleVenta detalle = await _context.DetalleVentas
                .Include(sd => sd.Venta)
                .ThenInclude(sd => sd.Usuario)
                .Include(sd => sd.Producto)                
                .FirstOrDefaultAsync(s => s.Id == id);
            if (detalle == null)
            {
                return NotFound();
            }

            return View(detalle);
        }

    }
}
