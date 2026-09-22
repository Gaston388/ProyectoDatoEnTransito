namespace SRC;
    public class Switch : DispositivoRed
    {
        //Cantidad de puertos disponibles en el switch
        public int CantidadPuertos {get; set;}
        
        //Cantidad de puertos que actualmente estan ocupados
        public int PuertosOcupados {get; set;}

        //Indica si el switch esta administrado/Configurardo
        public bool Administrado {get;set;}

        public Switch (int id, string  nombre, string direccionIp, string direccionMAC, bool encendido, int cantidadPuertos, int puertosOcupados, bool administrado)
        : base(id, nombre, direccionIp, direccionMAC, encendido)
        {
            CantidadPuertos = cantidadPuertos;
            PuertosOcupados = puertosOcupados;
            Administrable = administrado;
        }
    }
