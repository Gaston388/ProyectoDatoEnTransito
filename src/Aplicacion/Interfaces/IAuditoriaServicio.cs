using Persistencia.Entidades;

namespace Aplicacion.Interfaces
{
    public interface IAuditoriaServicio
    {
        List<Auditoria> ConsultarAuditorias();
        //El LIst es el tipo dedato que devuelve el metodo
    }
} 