namespace Aplicacion.DTOs
{
    public class CrearDispositivoRequestDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public int LatenciaMs { get; set; }
    }
}