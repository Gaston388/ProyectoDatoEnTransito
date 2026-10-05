namespace Persistencia.Entidades;
    public class Simulacion
    {
        public DateTime FechaInicio{get;set;}
        public DateTime FechaFin{get;set;}
        public bool Finalizado {get;set;}
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
            Finalizado = false;
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
        public bool ProcesarPaquete(PaqueteRed paquete)
        {
            foreach (DispositivoRed dispositivo in Dispositivos)
            {
                if (paquete.Ttl <= 0)
                {
                    paquete.Procesado = false ;
                    Finalizar(false);
                    return false;
                }
                bool permitido = dispositivo.ProcesarPaquete(paquete);

                paquete.LatenciaAcumulada += dispositivo.Latencia;
                paquete.Ttl--;
                if (!permitido)
                {
                    paquete.Procesado = false;
                    Finalizar(false);
                    return false;
                }
            }
            paquete.Procesado = true;
            Finalizar(true);
            return true;
        }
        public void Finalizar(bool resultado)
        {
            FechaFin = DateTime.Now;
            Finalizado = resultado;
        }
    }


