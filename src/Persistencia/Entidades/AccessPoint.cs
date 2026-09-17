namespace SRC;
   public class AccessPoint : DispositivoRed
{
    //SSID es el nombre de la red WI-FI
    public string Ssid { get; set; } = string.Empty;
    //Tipo de proteccion tipo WPA2 o WPA3
    public string Seguridad { get; set; } = string.Empty;

    //Canal de WI-FI utilizado como el 6
    public int Canal { get; set; }

    //cantidad maxima de dispsitivos conectados
    public int MaximoDispositivos { get; set; }

    public AccessPoint(int id, string nombre, string direccionIp, string direccionMAC, bool encendido, string ssid, string seguridad, int canal, int maximoDispositivos)
     : base(id, nombre, direccionIp, direccionMAC, encendido)
    {
        Ssid = ssid;
        Seguridad = seguridad;
        Canal = canal;
        MaximoDispositivos = maximoDispositivos;
    }
}