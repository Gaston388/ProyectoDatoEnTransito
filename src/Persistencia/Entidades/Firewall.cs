namespace Persistencia.Entidades;
    public class Firewall : DispositivoRed
    {   
        //indica si las reglas de Firewall estan activas mmm
        //true = esta filtarndo trafico
        //false = no esta filtrando
        public bool FiltradoActivo {get;set;}
        
        //La politica Predeterminada indica que hacer con el trafico que no coincide con ninguna regla 
        // opciones = "permitir" or "bloquear"
        public string PoliticaPredeterminada {get;set;} = string.Empty;
        
        //se explica solo
        public int CantidadReglas {get;set;}

        //indice si el fierwall bloquea trafico que intenta entrar a la red
        public bool BloqueaTraficoEntrante {get;set;}

        //indica si bloquea trafico que sale de la red
        public bool BloqueaTraficoSaliente {get;set;}

        //indica el tipo de firewall que representa 
        public string Tipo {get;set;} = string.Empty;
        public Firewall(int id, string nombre, string direccionIp, string direccionMAC, bool encendido, bool filtradoActivo, string politicaPredeterminada, int cantidadReglas, bool bloqueaTraficoEntrante, bool bloqueaTraficoSaliente, string tipo)
        : base (id, nombre, direccionIp, direccionMAC, encendido)
        {
            FiltradoActivo = filtradoActivo;
            PoliticaPredeterminada = politicaPredeterminada;
            CantidadReglas = cantidadReglas;
            BloqueaTraficoEntrante = bloqueaTraficoEntrante;
            BloqueaTraficoSaliente = bloqueaTraficoSaliente;
            Tipo = tipo;
        }
    }
