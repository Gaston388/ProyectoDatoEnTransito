namespace Persistencia.Entidades
{
    public class Router: DispositivoRed
    {
        public List<string> Reglas { get; set; }
        private int paquetesBloqueados;
        public int PaquetesBloqueados
        {
            get => paquetesBloqueados;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),"La cantidad de paquetes bloqueados no puede ser negativa.");
                }
                paquetesBloqueados = value;
            }
        }
        private int paquetesPermitidos;
        public int PaquetesPermitidos
        {
            get => paquetesPermitidos;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),"La cantidad de paquetes permitidos no puede ser negativa.");
                }
                paquetesPermitidos = value;
            }
        }
        public Router (int id, string nombre, string direccionIp, string direccionMAC, bool encendido, int paquetesBloqueados, int paquetesPermitidos)
        : base  (id,  nombre, direccionIp,  direccionMAC,  encendido)
        {
            Reglas = new List<string>();  
            PaquetesBloqueados = paquetesBloqueados;
            PaquetesPermitidos = paquetesPermitidos;
        }
        public void AgregarRegla(string regla)
        {
            if (string.IsNullOrWhiteSpace(regla))
            {
                throw new ArgumentException("La regla no puede estar vacía.");
            }
            Reglas.Add(regla);
        }
        public override bool ProcesarPaquete(PaqueteRed paquete)
        {
            if (!Encendido)
            {
                return false;
            }
            // Por ahora el router permite el paquete.
            PaquetesPermitidos++;
            return true;
        }
    }
}        