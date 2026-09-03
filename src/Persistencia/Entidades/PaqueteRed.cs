namespace SRC;
    public class PaqueteRed
    {
        public int id { get; set; }
        public string ipOrigen { get; set; }
        public string ipDestino { get; set; }
        public string macOrigen { get; set; }
        public string macDestino { get; set; }
        public int tamaño { get; set; }
        public string protocolo { get; set; }
        public string datos { get; set; }
        public DateTime horaCreacion { get; set; }

        public PaqueteRed (int id, string ipOrigen, string ipDestino, string macOrigen, string macDestino, int tamaño, string protocolo, string datos, DateTime horaCreacion)
        {
            this.id = id;
            this.ipOrigen = ipOrigen;
            this.ipDestino = ipDestino;
            this.macOrigen = macOrigen;
            this.macDestino = macDestino;
            this.tamaño = tamaño;
            this.protocolo = protocolo;
            this.datos = datos;
            this.horaCreacion = horaCreacion;
        }
    }