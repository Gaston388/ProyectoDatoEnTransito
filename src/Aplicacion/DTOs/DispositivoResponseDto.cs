
namespace Aplicacion.DTOs
{
    public class DispositivoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public int LatenciaMs { get; set; }
    }
}
