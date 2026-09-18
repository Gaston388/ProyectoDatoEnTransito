namespace Persistencia.Entidades;
    public class Simulacion
    {
        public List<DispositivoRed> Dispositivos {get;set;}
        public List<PaqueteRed> Paquetes {get;set;}
        public Simulacion()
        {
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
    }


