using ProyectoVentas.Models.Entidades;

namespace ProyectoVentas.Services
{
    public interface IServicioUsuario
    {
        Task<Usuario> AutenticarUsuario(string correo, string clave);
        Task<Usuario> CrearUsuario(Usuario usuario);
        Task<Usuario> ObtenerUsuario(string nombreUsuario);


    }
}
