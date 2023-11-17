using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Models;
using ProyectoVentas.Models.Entidades;

namespace ProyectoVentas.Services
{
    public class ServicioUsuario : IServicioUsuario
    {
        private readonly VentasContext _context;

        public ServicioUsuario(VentasContext context)
        {
            _context = context;
        }

        public async Task<Usuario> AutenticarUsuario(string correo, string clave)
        {
            Usuario usuario = await _context.Usuarios.Where(u => u.Correo == correo && u.Clave == clave)
               .FirstOrDefaultAsync();

            return usuario;
        }

        public async Task<Usuario> CrearUsuario(Usuario usuario)
        {
            usuario.FechaCreacion = DateTime.Today;
            usuario.Rol = "Cliente";
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        
        public async Task<Usuario> ObtenerUsuario(string nombreUsuario)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreCompleto == nombreUsuario);
        }
    }
}
