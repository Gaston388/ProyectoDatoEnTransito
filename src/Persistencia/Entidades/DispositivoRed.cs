namespace Persistencia.Entidades
{
    public abstract class DispositivoRed
    {
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
        private string nombre = string.Empty; 
        public string Nombre
        {
            get => nombre;
            set
            {
                if(String.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("El nombre no puede estar vacio");
                }
                nombre = value;
            }
        }
        private string direccionIp = string.Empty;
        public string DireccionIp
        {
            get => direccionIp;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("La direccion Ip no puede estar vacia");
                }           
                direccionIp = value;
            }
        }    
        public string DireccionMAC { get; set; } = string.Empty;
        public bool Encendido { get; set; }

        public DispositivoRed (int id, string nombre, string direccionIp, string direccionMAC, bool encendido)
        {
            Id = id;
            Nombre = nombre;
            DireccionIp = direccionIp;
            DireccionMAC = direccionMAC;
            Encendido = encendido;
        }
        public abstract bool ProcesarPaquete(PaqueteRed paquete);
    }
}
