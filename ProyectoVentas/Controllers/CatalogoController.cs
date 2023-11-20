using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Models;
using ProyectoVentas.Models.Entidades;
using ProyectoVentas.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Drawing.Printing;

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

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "NameDesc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "PriceDesc" : "Price";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            IQueryable<Producto> query = _context.Productos
                .Include(p => p.Categoria)
                .Where(p => p.Cantidad > 0);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query
                    .Where(p => (p.Nombre.ToLower().Contains(searchString.ToLower()) ||
                                p.Categoria.Nombre.ToLower().Contains(searchString.ToLower())));
            }

            switch (sortOrder)
            {
                case "NameDesc":
                    query = query.OrderByDescending(p => p.Nombre);
                    break;
                default:
                    query = query.OrderBy(p => p.Nombre);
                    break;
            }

            int pageSize = 8;

            CatalogoViewModel model = new()
            {
                Productos = await PaginatedList<Producto>.CreateAsync(query, pageNumber ?? 1, pageSize),
                Categorias = await _context.Categorias.ToListAsync(),
            };

            return View(model);
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

            // Obtener o crear el carrito del usuario
            Venta venta = await _context.Ventas
                .Include(c => c.DetalleVentas)
                .FirstOrDefaultAsync(c => c.Usuario.Id == usuario.Id && c.FechaCreacion == DateTime.Today);

            if (venta == null)
            {
                venta = new Venta
                {
                    FechaCreacion = DateTime.Now,
                    DetalleVentas = new List<DetalleVenta>(),
                    Total = 0,
                    Usuario = usuario,
                };
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
                DetalleVentas = venta.DetalleVentas.ToList(),
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

                // Obtener o crear el carrito del usuario
                Venta venta = await _context.Ventas
                    .Include(c => c.DetalleVentas)
                    .ThenInclude(c => c.Producto)
                    .FirstOrDefaultAsync(c => c.Usuario.Id == usuario.Id && c.FechaCreacion == DateTime.Today);

                if (venta == null)
                {
                    venta = new Venta
                    {
                        FechaCreacion = DateTime.Now,
                        DetalleVentas = new List<DetalleVenta>(),
                        Total = 0,
                        Usuario = usuario,
                    };
                }

                try
                {
                    venta.DetalleVentas.Add(new DetalleVenta
                    {
                        Producto = producto,
                        Cantidad = model.Cantidad,
                        Total = model.Cantidad * (int)producto.PrecioOferta,
                    });

                    // Calcular el total sumando los subtotales de cada detalle
                    venta.Total = venta.DetalleVentas.Sum(d => d.Cantidad * (int)d.Producto.PrecioOferta);

                    if (venta.Id == 0)
                    {
                        // Si el carrito no existe en la base de datos, agregarlo
                        _context.Add(venta);
                    }

                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Producto agregado al carrito!!!";
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
