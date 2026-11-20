using Dapper;
using MySqlConnector;
using Persistencia.Conexiones;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;

namespace Persistencia.Repositorios
{
    public class RouterRepositorio : IRouterRepositorio
    {
        private readonly ConexionDesarrollador conexion;

        public RouterRepositorio(ConexionDesarrollador conexion)
        {
            this.conexion = conexion;
        }
        public Router? ObtenerPorId(int id)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                SELECT
                    d.id AS Id,
                    d.nombre AS Nombre,
                    d.direccion_ip AS DireccionIp,
                    d.direccion_mac AS DireccionMAC,
                    d.encendido AS Encendido,
                    r.paquetes_bloqueados AS PaquetesBloqueados,
                    r.paquetes_permitidos AS PaquetesPermitidos,
                    d.latencia AS Latencia
                FROM routers r
                INNER JOIN dispositivos d ON d.id = r.dispositivo_id
                WHERE d.id = @id;
            ";

            return connection.QueryFirstOrDefault<Router>(sql, new { id });
        }

        public List<Router> Listar()
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sql = @"
                SELECT
                    d.id AS Id,
                    d.nombre AS Nombre,
                    d.direccion_ip AS DireccionIp,
                    d.direccion_mac AS DireccionMAC,
                    d.encendido AS Encendido,
                    r.paquetes_bloqueados AS PaquetesBloqueados,
                    r.paquetes_permitidos AS PaquetesPermitidos,
                    d.latencia AS Latencia
                FROM routers r
                INNER JOIN dispositivos d ON d.id = r.dispositivo_id;
            ";

            return connection.Query<Router>(sql).ToList();
        }        
        public void Eliminar(int id)
        {
            using MySqlConnection connection = conexion.CrearConexion();
            connection.Open();

            using MySqlTransaction transaccion = connection.BeginTransaction();

            try
            {
                string sqlRouter = @"
                    DELETE FROM routers
                    WHERE dispositivo_id = @id;
                ";

                connection.Execute(sqlRouter, new { id }, transaccion);

                string sqlDispositivo = @"
                    DELETE FROM dispositivos
                    WHERE id = @id;
                ";

                connection.Execute(sqlDispositivo, new { id }, transaccion);

                transaccion.Commit();
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }       
         public int Agregar(Router router)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            connection.Open();

            using MySqlTransaction transaccion = connection.BeginTransaction();

            try
            {
                string sqlDispositivo = @"
                    INSERT INTO dispositivos
                    (
                        nombre,
                        direccion_ip,
                        direccion_mac,
                        encendido,
                        latencia
                    )
                    VALUES
                    (
                        @Nombre,
                        @DireccionIp,
                        @DireccionMAC,
                        @Encendido,
                        @Latencia
                    );

                    SELECT LAST_INSERT_ID();
                ";

                int dispositivoId = connection.ExecuteScalar<int>(
                    sqlDispositivo,
                    router,
                    transaccion
                );

                string sqlRouter = @"
                    INSERT INTO routers
                    (
                        dispositivo_id,
                        paquetes_bloqueados,
                        paquetes_permitidos
                    )
                    VALUES
                    (
                        @DispositivoId,
                        @PaquetesBloqueados,
                        @PaquetesPermitidos
                    );
                ";

                connection.Execute(
                    sqlRouter,
                    new
                    {
                        DispositivoId = dispositivoId,
                        router.PaquetesBloqueados,
                        router.PaquetesPermitidos
                    },
                    transaccion
                );

                transaccion.Commit();

                router.Id = dispositivoId;

                return dispositivoId;
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }
        public void Actualizar(Router router)
        {
            using MySqlConnection connection = conexion.CrearConexion();

            string sqlDispositivo = @"
                UPDATE dispositivos
                SET nombre = @Nombre,
                    direccion_ip = @DireccionIp,
                    direccion_mac = @DireccionMAC,
                    encendido = @Encendido,
                    latencia = @Latencia
                WHERE id = @Id;
            ";

            connection.Execute(sqlDispositivo, router);

            string sqlRouter = @"
                UPDATE routers
                SET paquetes_bloqueados = @PaquetesBloqueados,
                    paquetes_permitidos = @PaquetesPermitidos
                WHERE dispositivo_id = @Id;
            ";

            connection.Execute(sqlRouter, router);
        }
        }
    }
