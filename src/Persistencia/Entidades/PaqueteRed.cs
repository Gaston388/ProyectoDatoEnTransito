namespace Persistencia.Entidades;
    public class PaqueteRed
    {
        public int Id { get; set; }
        public string IpOrigen { get; set; }
        public string IpDestino { get; set; }
        public string MacOrigen { get; set; }
        public string MacDestino { get; set; }
        public int Tamaño { get; set; }
        public string Protocolo { get; set; }
        public string Datos { get; set; }
        public DateTime HoraCreacion { get; set; }

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