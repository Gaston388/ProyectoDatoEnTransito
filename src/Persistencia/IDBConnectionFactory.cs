using System.Data;

namespace Persistencia.Repositorios
{
    public interface IDbConnectionFactory
    {
        IDbConnection CrearConnection();
    }
}