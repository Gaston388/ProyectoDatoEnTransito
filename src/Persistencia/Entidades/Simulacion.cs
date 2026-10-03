namespace Persistencia.Entidades;
    public class Simulacion
    {
        private int id;
        public int Id
        {
            get => id;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("El ID de la simulación debe ser mayor que 0");
                }
                id = value;
            } 
        }
        public List<DispositivoRed> Dispositivos {get;set;}
        public List<PaqueteRed> Paquetes {get;set;}
        public Simulacion(int id)
        {
            Id = id;
            Dispositivos = new List<DispositivoRed>();
            Paquetes = new List<PaqueteRed>();
        }
        public void AgregarDispositivo(DispositivoRed dispositivo)
        {
            Dispositivos.Add(dispositivo);    
        }
        public void AgregarPaquete(PaqueteRed paquete)
        {
            Paquetes.Add(paquete);
        }
        public void ProcesarPaquete(PaqueteRed paquete)
        {
            foreach (DispositivoRed dispositivo in Dispositivos)
            {
                if (paquete.Ttl <= 0)
                {
                    paquete.Procesado = false ;
                    return;
                }
                bool permitido = dispositivo.ProcesarPaquete(paquete);

                paquete.LatenciaAcumulada += dispositivo.Latencia;
                paquete.Ttl--;
                if (!permitido)
                {
                    paquete.Procesado = false;
                    return;
                }
            }
            paquete.Procesado = true;
        }
    }


