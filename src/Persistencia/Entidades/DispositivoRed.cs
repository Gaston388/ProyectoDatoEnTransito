namespace Persistencia.Entidades
{
    public class DispositivoRed
    {
        public int Id {get; set;}
        private string nombre = string.Empty; 
        public string Nombre
        {
            get => nombre;
            set
            {
                if(String.IsNullOrEmpty(value))
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
        public string DireccionMAC { get; set; }
        public bool Encendido { get; set; }

        public DispositivoRed (int id, string nombre, string direccionIp, string direccionMAC, bool encendido)
        {
            Id = id;
            Nombre = nombre;
            DireccionIp = direccionIp;
            DireccionMAC = direccionMAC;
            Encendido = encendido;
        }
    }
}
