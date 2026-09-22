namespace Persistencia.Entidades
{
    public class AccessPoint : DispositivoRed
    {
        //SSID es el nombre de la red WI-FI
        private string ssid = string.Empty;
        public string Ssid
        {
            get{
                return value;
                }
            set{
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El SSID no puede estar vacio");
                }
                ssid = value;
                }
        }
        
        //Tipo de proteccion tipo WPA2 o WPA3
        private string Seguridad = string.Empty;
        public string Seguridad
        {
            get
            {
                return value ;
            }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("La seguridad no puede estar vacia");
                }
                Seguridad = value;
            }
        }
        //Canal de WI-FI utilizado como el 6
        private int canal;
        public int Canal
        {
            get
            {
                return value;
            }
            set
            {
                if(canal<=0)
                {
                    throw new ArgumentException("El canal debe ser mayor que cero");
                }
                canal = value;
            }
        }


        //cantidad maxima de dispsitivos conectados
        private int maximoDispositivos;
        public int MaximoDispositivos
        {
            get
            {
                return Value;
            }
            set
            {
                if(maximoDispositivos<=0)
                {
                    throw new ArgumentException("El maximo de dispositivos debe ser mayor a cero");
                }
            }
        }

        public AccessPoint(int id, string nombre, string direccionIp, string direccionMAC, bool encendido, string ssid, string seguridad, int canal, int maximoDispositivos)
        : base(id, nombre, direccionIp, direccionMAC, encendido)
        {
            Ssid = ssid;
            Seguridad = seguridad;
            Canal = canal;
            MaximoDispositivos = maximoDispositivos;
        }
    }
}