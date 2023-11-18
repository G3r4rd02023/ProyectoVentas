using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Models;
using ProyectoVentas.Models.Entidades;
using ProyectoVentas.Services;

namespace ProyectoVentas.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly VentasContext _context;
        private readonly IServicioUsuario _servicioUsuario;

        public CatalogoController(VentasContext context, IServicioUsuario servicioUsuario)
        {
            _context = context;
            _servicioUsuario = servicioUsuario;
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

        public async Task<IActionResult> Vender(int id)
        {

            Usuario usuario = await _servicioUsuario.ObtenerUsuario(User.Identity.Name);
            if (usuario == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            VentaViewModel model = new()
            {
                Producto = producto,
                ProductoId = id,                
                Precio = producto.Precio,
                URLImagen = producto.URLImagen,
                Usuario = usuario,
                DetalleVentas = new List<DetalleVenta>()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Vender(VentaViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = await _servicioUsuario.ObtenerUsuario(User.Identity.Name);
                if (usuario == null)
                {
                    return NotFound();
                }

                Producto producto = await _context.Productos.FindAsync(model.ProductoId);
                if (producto == null)
                {
                    return NotFound();
                }
                try
                {
                    var nuevaVenta = new Venta
                    {
                        FechaCreacion = DateTime.Today,
                        DetalleVentas = new List<DetalleVenta>(),
                        Total = 0,
                        Usuario = usuario,
                    };

                    nuevaVenta.DetalleVentas.Add(new DetalleVenta
                    {
                        Producto = producto,
                        Cantidad = model.Cantidad,
                        Total = model.Cantidad * (int)producto.PrecioOferta,
                    });

                    // Calcular el total sumando los subtotales de cada detalle
                    nuevaVenta.Total = nuevaVenta.DetalleVentas.Sum(d => d.Cantidad * (int)d.Producto.PrecioOferta);


                    _context.Add(nuevaVenta);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Gracias por tu compra!!!";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(ex.Message, "Ocurrio un error");
                };
                return RedirectToAction("Index");
            }
            return View(model);
        }

    } 
}
