# Datos En Tránsito

## Descripción

Datos En Tránsito es un sistema que permite simular el recorrido de paquetes de datos a través de distintos dispositivos de red, como routers, switches, firewalls y access points.

La aplicación permite administrar dispositivos, crear simulaciones y consultar los resultados de los recorridos realizados por los paquetes.

El proyecto está desarrollado en **C# con ASP.NET Core Web API** y utiliza **MySQL** para almacenar la información.

## Tecnologías utilizadas

- C#
- .NET / ASP.NET Core Web API
- MySQL
- Dapper
- MySqlConnector
- Swagger / OpenAPI
- Git y GitHub

## Estructura del proyecto

La solución está organizada en cuatro proyectos:

- **API:** recibe las solicitudes HTTP y expone los endpoints.
- **Aplicacion:** contiene los servicios, las interfaces y la lógica de negocio.
- **Persistencia:** contiene las entidades, los repositorios y la conexión con MySQL.
- **Tests:** contiene las pruebas del proyecto.

## Requisitos

Para ejecutar el proyecto se necesita:

- .NET SDK.
- MySQL Server.
- MySQL Command Line Client.
- Visual Studio Code o un entorno compatible con C#.

## Configuración de la base de datos

La base de datos utilizada por el proyecto se llama `datos_transito`.

Los scripts SQL se encuentran en la carpeta `scripts`.

### Orden de ejecución

El orden correcto es:

1. `DDL.sql`: crea la base de datos y sus tablas.
2. `USUARIOS.sql`: crea los usuarios y asigna los permisos.
3. `SP_Registrar.sql`: crea el procedimiento almacenado de registro.
4. `SP_Estadisticas_Dispositivos.sql`: crea el procedimiento de estadísticas de dispositivos.
5. `SP_Estadisticas_Simulaciones.sql`: crea el procedimiento de estadísticas de simulaciones.

También se encuentra disponible el archivo `EJECUTARTODO.sql`, que ejecuta los cinco scripts anteriores en ese orden.

### Ejecutar todos los scripts

Desde la carpeta raíz del repositorio, abrir MySQL Command Line Client:

```bash
mysql -u root -p
```

Una vez dentro de MySQL, ejecutar:

```sql
SOURCE C:/ruta/ProyectoDatoEnTransito/scripts/EJECUTARTODO.sql;
```

Reemplazar `C:/ruta/ProyectoDatoEnTransito` por la ubicación real del repositorio.

### Ejecutar los scripts individualmente

Si se necesita ejecutar cada archivo por separado:

```sql
SOURCE C:/ruta/ProyectoDatoEnTransito/scripts/DDL.sql;
SOURCE C:/ruta/ProyectoDatoEnTransito/scripts/USUARIOS.sql;
SOURCE C:/ruta/ProyectoDatoEnTransito/scripts/SP_Registrar.sql;
SOURCE C:/ruta/ProyectoDatoEnTransito/scripts/SP_Estadisticas_Dispositivos.sql;
SOURCE C:/ruta/ProyectoDatoEnTransito/scripts/SP_Estadisticas_Simulaciones.sql;
```

## Restauración del backup

Para restaurar un backup de la base de datos:

1. Abrir MySQL Command Line Client.
2. Iniciar sesión con un usuario que tenga permisos suficientes.
3. Ejecutar el archivo de respaldo mediante `SOURCE`.

Ejemplo:

```sql
SOURCE C:/ruta/del/backup/datos_transito_backup.sql;
```

El archivo debe contener el respaldo de la base de datos `datos_transito`.

**Importante:** el repositorio revisado no contiene un archivo de backup identificado. Para utilizar este procedimiento es necesario disponer previamente de uno.

## Configuración de la conexión

La aplicación utiliza MySQL mediante `MySqlConnector` y Dapper.

Los datos de conexión utilizados por el código revisado son:

| Parámetro | Valor |
|---|---|
| Servidor | localhost |
| Puerto | 3306 |
| Base de datos | datos_transito |
| Usuario utilizado por la aplicación | desarrollador |

La contraseña debe configurarse de manera privada y no debe publicarse en GitHub.

### Variables de entorno recomendadas

Para evitar guardar credenciales directamente en el código, se recomienda utilizar estas variables:

```text
DB_HOST=localhost
DB_PORT=3306
DB_NAME=datos_transito
DB_USER=desarrollador
DB_PASSWORD=tu_contraseña
```

**Nota:** las clases de conexión actuales deben adaptarse para leer estas variables. Su uso no debe considerarse activo hasta realizar esa modificación.

### Conexiones utilizadas por las operaciones

El proyecto utiliza dos mecanismos de acceso a MySQL:

- **Consultas y operaciones CRUD:** los repositorios utilizan `ConexionDesarrollador` y Dapper para ejecutar consultas SQL directamente, como `SELECT`, `INSERT`, `UPDATE` y `DELETE`.
- **Estadísticas:** `EstadisticaRepositorio` utiliza `DbConnectionFactory` para ejecutar los procedimientos almacenados de estadísticas.

Ambos mecanismos se conectan a la base de datos `datos_transito`.

## Compilación e inicio de la API

Desde la carpeta raíz del repositorio, ejecutar:

```bash
dotnet restore
```

Para compilar la solución:

```bash
dotnet build
```

Para iniciar la API:

```bash
dotnet run --project API/API.csproj
```

La API utiliza el puerto local `5003` en la configuración de desarrollo revisada.

### Swagger

Una vez iniciada la API, acceder a:

http://localhost:5003/swagger/index.html

Desde Swagger se pueden consultar y probar los endpoints disponibles.

## Ejemplo de creación de una simulación

Para crear una simulación se utiliza el siguiente endpoint:

```http
POST /api/simulaciones
```

Ejemplo de solicitud JSON:

```json
{
  "origen": "192.168.1.10",
  "destino": "192.168.1.20",
  "tamanioBytes": 1024,
  "ttl": 10,
  "dispositivos": [
    {
      "id": 1,
      "tipo": "Router"
    },
    {
      "id": 2,
      "tipo": "Switch"
    }
  ]
}
```

Los identificadores deben corresponder a dispositivos que existan en la base de datos y los tipos deben coincidir con los dispositivos registrados.

La solicitud permite indicar:

- `origen`: dirección IP de origen del paquete.
- `destino`: dirección IP de destino.
- `tamanioBytes`: tamaño del paquete.
- `ttl`: tiempo de vida inicial del paquete.
- `dispositivos`: lista ordenada de dispositivos que participan en el recorrido.
- `id`: identificador del dispositivo.
- `tipo`: tipo de dispositivo.

## Funcionalidades principales

- Registrar dispositivos de red.
- Consultar dispositivos registrados.
- Consultar dispositivos por identificador.
- Actualizar información de dispositivos.
- Eliminar dispositivos.
- Crear simulaciones de recorrido de paquetes.
- Consultar simulaciones.
- Consultar los pasos de un recorrido.
- Actualizar simulaciones y pasos del recorrido.
- Eliminar simulaciones.

## Simulaciones

Cada simulación trabaja con un paquete de datos y una lista ordenada de dispositivos por los que debe pasar.

Durante el recorrido se tienen en cuenta:

- El TTL del paquete.
- La latencia acumulada.
- Los dispositivos que participan.
- El resultado del procesamiento.

Al finalizar, la simulación almacena su resultado y la información del recorrido realizado.

## Arquitectura

El proyecto utiliza una separación por responsabilidades:

1. La **API** recibe las solicitudes HTTP y devuelve las respuestas.
2. La capa de **Aplicacion** ejecuta la lógica de negocio mediante servicios.
3. La capa de **Persistencia** consulta y modifica la base de datos.
4. La capa de **Tests** permite verificar el funcionamiento del sistema.

Esta organización busca mantener el código ordenado, facilitar su mantenimiento y separar la lógica de negocio del acceso a los datos.

## Verificación desde una base limpia

Para comprobar la instalación desde cero:

1. Crear una base de datos limpia o eliminar la base anterior si se desea reconstruirla.
2. Ejecutar `EJECUTARTODO.sql`.
3. Verificar que se hayan creado las tablas y los procedimientos almacenados.
4. Comprobar que los usuarios de MySQL tengan los permisos necesarios.
5. Configurar correctamente las conexiones de la aplicación.
6. Ejecutar `dotnet restore` y `dotnet build`.
7. Iniciar la API con `dotnet run --project API/API.csproj`.
8. Abrir Swagger y probar los endpoints.

La instalación debe considerarse verificada cuando las operaciones de la API pueden consultar y modificar la base de datos correctamente.

---

**Proyecto académico - Datos En Tránsito**
