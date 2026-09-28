namespace Persistencia.Entidades
{
    public class PaqueteRed
    {
        //sirve para identificar el problema
        private int id;
        public int Id
        {
            get => id;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),"El Id debe ser mayor que 0.");
                }
                id = value;
            }
        }        
        //indica desde que direccion ip sale el pquete
       private string ipOrigen = string.Empty;

        public string IpOrigen
        {
            get => ipOrigen;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("La IP de origen no puede estar vacía.");
                }
                ipOrigen = value;
            }
        }
        //indica a que direccion ip se dirige el paquete
       private string ipDestino = string.Empty;

        public string IpDestino
        {
            get => ipDestino;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {    
                    throw new ArgumentException("La IP de destino no puede estar vacía.");
                }
                ipDestino = value;
            }
        }
        //Representa la direccion MAc del dispositivo que origina el paquete
        public string MacOrigen { get; set; } = string.Empty;
        //representa la direccion MAC del destino 
        public string MacDestino { get; set; } = string.Empty;
        //indica cuanto ocupa el paquete
        private int tamaño;
        public int Tamaño
        {
            get => tamaño;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),"El tamaño del paquete debe ser mayor que 0.");
                }
                tamaño = value;
            }
        }        
        //indica que protocolo esta utilizando el paquete 
        //"TCP", "HTTP" , "UDP"
        private string protocolo = string.Empty;
        public string Protocolo
        {
            get => protocolo;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {   
                throw new ArgumentException("El protocolo no puede estar vacío.");
                }
                string protocoloNormalizado = value.ToUpper().Trim();
                if (protocoloNormalizado != "TCP" && protocoloNormalizado != "UDP" && protocoloNormalizado != "HTTP")
                {
                    throw new ArgumentException("El protocolo debe ser TCP, UDP o HTTP.");
                }
                protocolo = value;
            }
        }
        //es el contenido que transporta el paquete
        private string datos = string.Empty;
        public string Datos
        {
            get => datos;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {    
                    throw new ArgumentException("Los datos del paquete no pueden estar vacíos.");
                }
                datos = value;
            }
        }
        //indica cuando se creo o registro el paquete
        private DateTime horaCreacion;
        public DateTime HoraCreacion
        {
            get => horaCreacion;
            set
            {
                if (value > DateTime.Now)
                {
                    throw new ArgumentException("La hora de creación no puede estar en el futuro.");
                }
                horaCreacion = value;
            }
        }        
        public bool Procesado {get;set;}
        private int latenciaAcumulada;
        public int LatenciaAcumulada
        {
            get => latenciaAcumulada;
            set
            {
                if (value < 0)
                {
                throw new ArgumentOutOfRangeException(nameof(value),"La latencia acumulada no puede ser negativa.");
                }
                latenciaAcumulada = value;
            }
        }        
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