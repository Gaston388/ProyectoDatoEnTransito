namespace SRC;
    public class DispositivoRed
    {
        public int id {get; set;}
        private string nombre; 
        public string Nombre
        {
            get{return nombre;}
            set
            {
                if
                {
                    
                }
            }
        }
        public string direccionIp {get; set;}
         public string direccionMAC { get; set; }
        public bool encendido { get; set; }

        public DispositivoRed (int id, string nombre, string direccionIp, string direccionMAC, bool encendido)
        {
            this.id = id;
            this.nombre= nombre;
            this.direccionIp= direccionIp;
            this.direccionMAC= direccionMAC;
            this.encendido= encendido;

        }

        
    }
