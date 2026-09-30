namespace Persistencia.Entidades
{
    public class AccessPoint : DispositivoRed
    {
        //SSID es el nombre de la red WI-FI
        private string ssid = string.Empty;
        public string Ssid
        {
            get => ssid;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("El SSID no puede estar vacío.");
                }
                ssid = value;
            }
        }
        //Tipo de proteccion tipo WPA2 o WPA3
        private string seguridad = string.Empty;
        public string Seguridad
        {
            get => seguridad;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(
                        "La seguridad no puede estar vacía.");

                if (value != "WPA2" && value != "WPA3")
                    throw new ArgumentException(
                        "La seguridad debe ser WPA2 o WPA3.");

                seguridad = value;
            }
        }

        //Canal de WI-FI utilizado como el 6
        private int canal;
        public int Canal
        {
            get => canal;
            set
            {
                if (value < 1 || value > 14)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),"El canal debe estar entre 1 y 14.");
                }
                canal = value;
            }
        }

        //cantidad maxima de dispsitivos conectados
        private int maximoDispositivos;

        public int MaximoDispositivos
        {
            get => maximoDispositivos;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),"La cantidad máxima de dispositivos debe ser mayor que 0.");
                }
                maximoDispositivos = value;
            }
        }


        public AccessPoint(int id, string nombre, string direccionIp, string direccionMAC, bool encendido, string ssid, string seguridad, int canal, int maximoDispositivos, int latencia)
        : base(id, nombre, direccionIp, direccionMAC, encendido, latencia)
        {
            Ssid = ssid;
            Seguridad = seguridad;
            Canal = canal;
            MaximoDispositivos = maximoDispositivos;
        }
        public override bool ProcesarPaquete(PaqueteRed paquete)
        {
            if (!Encendido)
            {
                return false;
            }
            return true;
        }
    }
}