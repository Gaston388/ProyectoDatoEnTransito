namespace Aplicacion.DTOs
{
    public class ActualizarFirewallRequestDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string DireccionIp { get; set; } = string.Empty;
        public string DireccionMAC { get; set; } = string.Empty;
        public bool Encendido { get; set; }
        public bool FiltradoActivo { get; set; }
        public string PoliticaPredeterminada { get; set; } = string.Empty;
        public int CantidadReglas { get; set; }
        public bool BloqueaTraficoEntrante { get; set; }
        public bool BloqueaTraficoSaliente { get; set; }
        public string Tipo { get; set; } = string.Empty;

        public int Latencia { get; set; }
    }
}