namespace Aplicacion.DTOs
{
    public class ActualizarSwitchRequestDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string DireccionIp { get; set; } = string.Empty;
        public string DireccionMAC { get; set; } = string.Empty;
        public bool Encendido { get; set; }
        public int CantidadPuertos { get; set; }
        public int PuertosOcupados { get; set; }
        public bool VlanActiva { get; set; }
        public int CantidadVlan { get; set; }

        public int Latencia { get; set; }
    }
}