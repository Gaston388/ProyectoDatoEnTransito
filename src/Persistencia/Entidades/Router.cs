namespace Persistencia.Entidades
{
    public class Router: DispositivoRed
    {
        public List<string> Reglas { get; set; }
        public int PaquetesBloqueados { get; set; }
        public int PaquetesPermitidos { get; set; }

        public Router (int id, string nombre, string direccionIp, string direccionMAC, bool encendido, int paquetesBloqueados, int paquetesPermitidos)
        : base  (id,  nombre, direccionIp,  direccionMAC,  encendido)
        {
            Reglas = new List<string>();  
            PaquetesBloqueados = paquetesBloqueados;
            PaquetesPermitidos = paquetesPermitidos;
        }
    }

}        