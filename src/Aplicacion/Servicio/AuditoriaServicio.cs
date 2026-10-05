using Aplicacion.Interfaces; //para poder usar el contrato IAuditoriaServidcio
using Persistencia.Entidades;//Para los datos de auditoria 
using Persistencia.Repositorios.InterfazRepositorios; //hmmm para traer la interfaz IauditoriaRepo el contrato de acceso ala base de dato

namespace Aplicacion.Servicios //mi capa de negocio del proyecto
{
    public class AuditoriaServicio : IAuditoriaServicio//se implementa el contrato de la interfaz para usar sus metodos obligatoriamente
    {
        private readonly IAuditoriaRepositorios _auditoriaRepositorio;
        //solo se puede usar en la misma clase y la variable solo se puede asignar una sola vez 
        //_auditoriaRepo es la variable que guardara la refe
        public AuditoriaServicio(IAuditoriaRepositorios auditoriaRepositorio)//en este metodo se inyecta la dependencia
        {
            _auditoriaRepositorio = auditoriaRepositorio;
        }

        public List<Auditoria> ConsultarAuditorias()//logica de negocio
        {
            return _auditoriaRepositorio.Listar();//delega el work a un repo
        }
    }
}