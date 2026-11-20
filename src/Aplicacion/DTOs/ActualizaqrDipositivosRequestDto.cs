
namespace Aplicacion.DTOs
{
    public class ActualizarDispositivoRequestDto
    {
        public string TipoDispositivo { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;
        public string DireccionIp { get; set; } = string.Empty;
        public string DireccionMAC { get; set; } = string.Empty;
        public bool Encendido { get; set; }
        public int Latencia { get; set; }

        // Router
        public int PaquetesBloqueados { get; set; }
        public int PaquetesPermitidos { get; set; }

        // Switch
        public int CantidadPuertos { get; set; }
        public int PuertosOcupados { get; set; }
        public bool VlanActiva { get; set; }
        public int CantidadVlan { get; set; }

        // Firewall
        public bool FiltradoActivo { get; set; }
        public string PoliticaPredeterminada { get; set; } = string.Empty;
        public int CantidadReglas { get; set; }
        public bool BloqueaTraficoEntrante { get; set; }
        public bool BloqueaTraficoSaliente { get; set; }
        public string Tipo { get; set; } = string.Empty;

        // Access Point
        public string Ssid { get; set; } = string.Empty;
        public string Seguridad { get; set; } = string.Empty;
        public int Canal { get; set; }
        public int MaximoDispositivos { get; set; }
    }
}
