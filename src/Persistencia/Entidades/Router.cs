using System.Runtime.CompilerServices;

namespace SRC;
    public class Router: DispositivoRed
    {
        public List<string> reglas { get; set; }
        public int paquetesBloqueados { get; set; }
        public int paquetesPermitidos { get; set; }

        public Router (int id, string nombre, string direccionIp, string direccionMAC, bool encendido, int paquetesBloqueados, int paquetesPermitidos) : base  (id,  nombre, direccionIp,  direccionMAC,  encendido)
        {
            this.reglas =new List<string>();  
            this.paquetesBloqueados = paquetesBloqueados;
            this.paquetesPermitidos = paquetesPermitidos;
        }
    }
        