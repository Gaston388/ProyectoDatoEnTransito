namespace Persistencia.Entidades
{
    public class PaqueteRed
    {
        //sirve para identificar el problema
        public int Id { get; set; }
        
        //indica desde que direccion ip sale el pquete
        public string IpOrigen { get; set; } = string.Empty;
        //indica a que direccion ip se dirige el paquete
        public string IpDestino { get; set; } = string.Empty;
        //Representa la direccion MAc del dispositivo que origina el paquete
        public string MacOrigen { get; set; } = string.Empty;
        //representa la direccion MAC del destino 
        public string MacDestino { get; set; } = string.Empty;
        //indica cuanto ocupa el paquete
        public int Tamaño { get; set; }
        //indica que protocolo esta utilizando el paquete 
        //"TCP", "HTTP" , "UDP"
        public string Protocolo { get; set; } = string.Empty;
        //es el contenido que transporta el paquete
        public string Datos { get; set; } = string.Empty;
        //indica cuando se creo o registro el paquete
        public DateTime HoraCreacion { get; set; }
        public PaqueteRed (int id, string ipOrigen, string ipDestino, string macOrigen, string macDestino, int tamaño, string protocolo, string datos, DateTime horaCreacion)
        {
            Id = id;
            IpOrigen = ipOrigen;
            IpDestino = ipDestino;
            MacOrigen = macOrigen;
            MacDestino = macDestino;
            Tamaño = tamaño;
            Protocolo = protocolo;
            Datos = datos;
            HoraCreacion = horaCreacion;
        }
    }
}