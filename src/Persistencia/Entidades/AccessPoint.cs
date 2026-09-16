namespace SRC;
   public class AccessPoint : DispositivoRed
{
    //SSID es el nombre de la red WI-FI
    private string ssid;
    public string Ssid
    {
        get
            { 
            return ssid ; 
            }
        set
            {
                
            }
    }
    //Tipo de proteccion tipo WPA2 o WPA3
    public string seguridad { get; set; }

    //Canal de WI-FI utilizado como el 6
    public string canal { get; set; }

    
    public int maximoDispositivos { get; set; }
    public List<DispositivoRed> dispositivosConectados { get; set; }

    public AccessPoint(int id, string nombre, string direccionIp, string direccionMAC, bool encendido, string ssid, string seguridad, string canal, int maximoDispositivos) : base(id, nombre, direccionIp, direccionMAC, encendido)
    {
        Ssid = ssid;
        this.seguridad = seguridad;
        this.canal = canal;
        this.maximoDispositivos = maximoDispositivos;
        this.dispositivosConectados = new List<DispositivoRed>();
    }
}
