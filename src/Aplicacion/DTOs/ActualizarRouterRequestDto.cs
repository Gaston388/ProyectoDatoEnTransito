namespace Aplicacion.DTOs
{
    public class ActualizarRouterRequestDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string DireccionIp { get; set; } = string.Empty;
        public string DireccionMAC { get; set; } = string.Empty;
        public bool Encendido { get; set; }

        public int PaquetesBloqueados { get; set; }
        public int PaquetesPermitidos { get; set; }

        public int Latencia { get; set; }
    }
}