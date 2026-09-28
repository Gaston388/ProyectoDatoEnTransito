namespace Persistencia.Entidades;
    public class Switch : DispositivoRed
    {
        private int cantidadPuertos;
        public int CantidadPuertos
        {
            get => cantidadPuertos;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),"La cantidad de puertos debe ser mayor que 0.");
                }
                cantidadPuertos = value;
            }
        }
        private int puertosOcupados;
        public int PuertosOcupados
        {
            get => puertosOcupados;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),"Los puertos ocupados no pueden ser negativos.");
                }
                if (value > CantidadPuertos)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),"Los puertos ocupados no pueden superar la cantidad total de puertos.");
                }
                puertosOcupados = value;
            }
        }
        public bool VlanActiva { get; set; }
        private int cantidadVlan;
        public int CantidadVlan
        {
            get => cantidadVlan;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),"La cantidad de VLAN no puede ser negativa.");
                }
                if (!VlanActiva && value > 0)
                {
                    throw new ArgumentException("No puede haber VLAN configuradas si VLAN está desactivada.");
                }
                cantidadVlan = value;
            }
        }
        public Switch(int id, string nombre, string direccionIp, string direccionMAC, bool encendido, int cantidadPuertos, int puertosOcupados, bool vlanActiva, int cantidadVlan)
        : base(id, nombre, direccionIp, direccionMAC, encendido)
        {
            CantidadPuertos = cantidadPuertos;
            PuertosOcupados = puertosOcupados;
            VlanActiva = vlanActiva;
            CantidadVlan = cantidadVlan;
        }
        public override bool ProcesarPaquete(PaqueteRed paquete)
        {
            if (!Encendido)
            {
                return false;
            }
            // El switch permite continuar el paquete.
            return true;
        }
}

