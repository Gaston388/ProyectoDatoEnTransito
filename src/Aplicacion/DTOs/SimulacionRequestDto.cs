namespace Aplicacion.DTOs
{
    public class SimulacionRequestDto
    {
        public string Origen { get; set; } = string.Empty;
        public string Destino { get; set; } = string.Empty;
        public int TamanioBytes { get; set; }
        public int Ttl { get; set; }
        public List<DispositivoSimulacionDto> Dispositivos { get; set; } = new();    }
}