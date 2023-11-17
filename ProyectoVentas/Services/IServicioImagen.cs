namespace ProyectoVentas.Services
{
    public interface IServicioImagen
    {
        Task<string> SubirImagen(Stream archivo, string nombre);
    }
}
