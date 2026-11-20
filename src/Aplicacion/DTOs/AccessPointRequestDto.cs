namespace Aplicacion.DTOs
{
    public class AccessPointRequestDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string DireccionIp { get; set; } = string.Empty;
        public string DireccionMAC { get; set; } = string.Empty;
        public bool Encendido { get; set; }

        public string Ssid { get; set; } = string.Empty;
        public string Seguridad { get; set; } = string.Empty;
        public int Canal { get; set; }
        public int MaximoDispositivos { get; set; }

        public int Latencia { get; set; }
    }
}