namespace SRC;
    public class DispositivoRed
    {
        public int Id {get; set;}
        private readonly string nombre ; 
        public string Nombre
    {
        get => nombre;
        set
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new Exception
            }
        }
    }
    private string direccionIp;
    public string DirrecionIp
    get => dirrecionIp
         public string direccionMAC { get; set; }
        public bool encendido { get; set; }

        public DispositivoRed (int id, string nombre, string direccionIp, string direccionMAC, bool encendido)
        {
            Id = id;
            this.nombre= nombre;
            this.direccionIp= direccionIp;
            this.direccionMAC= direccionMAC;
            this.encendido= encendido;

        }

        
    }
