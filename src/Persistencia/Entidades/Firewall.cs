using System.Runtime.InteropServices;

namespace Persistencia.Entidades
{
    public class Firewall : DispositivoRed
    {   
        //indica si las reglas de Firewall estan activas mmm
        //true = esta filtarndo trafico
        //false = no esta filtrando
        public bool FiltradoActivo {get;set;}
        
        //La politica Predeterminada indica que hacer con el trafico que no coincide con ninguna regla 
        // opciones = "permitir" or "bloquear"
        private string politicaPredeterminada = string.Empty;
        public string PoliticaPredeterminada
        {
            get => politicaPredeterminada;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("La política predeterminada no puede estar vacía.");
                }
                value = value.ToLower().Trim();
                if (value != "permitir" && value != "bloquear")
                {
                    throw new ArgumentException("La política predeterminada debe ser 'permitir' o 'bloquear'.");
                }
                politicaPredeterminada = value;
            }
        }        
        //se explica solo
        private int cantidadReglas;
        public int CantidadReglas
        {
            get => cantidadReglas;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),"La cantidad de reglas no puede ser negativa.");
                }
                cantidadReglas = value;
            }
        }
        //indice si el fierwall bloquea trafico que intenta entrar a la red
        public bool BloqueaTraficoEntrante {get;set;}

        //indica si bloquea trafico que sale de la red
        public bool BloqueaTraficoSaliente {get;set;}
        //indica el tipo de firewall que representa 
        private string tipo = string.Empty;
        public string Tipo
        {
            get => tipo;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {   
                    throw new ArgumentException("El tipo de firewall no puede estar vacío.");
                }
                tipo = value.Trim();
            }
        }        
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
        public override bool ProcesarPaquete(PaqueteRed paquete)
        {
            if (!Encendido)
            {
                return false;
            }

            if (!FiltradoActivo)
            {
                return true;
            }

            if (BloqueaTraficoEntrante)
            {
                return false;
            }
            if(PoliticaPredeterminada.ToLower() == "bloquear")
            {
                return false;
            }
            return true;
        }
    }
}
