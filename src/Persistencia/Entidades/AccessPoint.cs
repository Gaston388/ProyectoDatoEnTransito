namespace SRC;
   public class AccessPoint : DispositivoRed
{
    public string ssid { get; set; }
    public string seguridad { get; set; }
    public string canal { get; set; }
    public int maximoDispositivos { get; set; }
    public List<DispositivoRed> dispositivosConectados { get; set; }

    public AccessPoint(int id, string nombre, string direccionIp, string direccionMAC, bool encendido, string ssid, string seguridad, string canal, int maximoDispositivos) : base(id, nombre, direccionIp, direccionMAC, encendido)
    {
        this.ssid = ssid;
        this.seguridad = seguridad;
        this.canal = canal;
        this.maximoDispositivos = maximoDispositivos;
        this.dispositivosConectados = new List<DispositivoRed>();
    }
}
