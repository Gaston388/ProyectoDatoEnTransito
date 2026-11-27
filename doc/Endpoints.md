# AccessPointEndpoints

## ¿Qué es AccessPointEndpoints?

`AccessPointEndpoints` es una clase de la capa API que define las rutas HTTP relacionadas con los Access Points (puntos de acceso).

Su función es recibir las solicitudes del cliente, llamar al servicio correspondiente y devolver una respuesta HTTP.

No contiene la lógica principal para crear, modificar o eliminar dispositivos. Esa responsabilidad corresponde a `AccessPointServicio`.

## Estructura de la clase

### Clase estática

`AccessPointEndpoints` es una clase `static`.

Esto significa que no necesitamos crear un objeto de esta clase para utilizar sus métodos.

### MapAccessPointEndpoints()

Es el método encargado de registrar todas las rutas relacionadas con los Access Points.

Recibe un `IEndpointRouteBuilder`, que permite definir los endpoints de la API.

El parámetro `app` representa el objeto sobre el que registramos las rutas.

La expresión `this` permite utilizar el método como una extensión de `IEndpointRouteBuilder`.

## Endpoints

### 1. GET - Listar Access Points

**Ruta:** `GET /api/access-points`

Permite obtener todos los Access Points almacenados.

Utiliza `IAccessPointServicio` para llamar al método `Listar()`.

Devuelve:

- `200 OK`: si la solicitud se procesa correctamente.
- Una lista de Access Points en formato JSON.

### 2. GET - Buscar por ID

**Ruta:** `GET /api/access-points/{id:int}`

Permite buscar un Access Point utilizando su identificador.

Recibe el ID mediante la URL y llama al método `BuscarPorId()` del servicio.

Comprueba si el dispositivo existe:

- `200 OK`: si encuentra el Access Point y devuelve sus datos.
- `404 Not Found`: si no encuentra ningún dispositivo con ese ID.

### 3. POST - Crear Access Point

**Ruta:** `POST /api/access-points`

Permite registrar un nuevo Access Point.

Recibe un `AccessPointRequestDto` en el cuerpo de la solicitud HTTP.

El servicio utiliza esos datos para crear el dispositivo y guardarlo en la base de datos.

Después devuelve:

- `201 Created`: indica que el recurso fue creado correctamente.
- La ubicación del nuevo recurso mediante su ID.
- Los datos del Access Point creado.

### 4. DELETE - Eliminar Access Point

**Ruta:** `DELETE /api/access-points/{id:int}`

Permite eliminar un Access Point utilizando su ID.

Primero busca el dispositivo mediante `BuscarPorId()`.

Si no existe, devuelve `404 Not Found`.

Si existe, llama a `Eliminar()` del servicio y devuelve:

- `204 No Content`: indica que la eliminación se realizó correctamente y no necesita devolver contenido.

### 5. PUT - Actualizar Access Point

**Ruta:** `PUT /api/access-points/{id:int}`

Permite modificar los datos de un Access Point existente.

Recibe dos elementos:

- `id`: identifica el dispositivo que se quiere modificar.
- `ActualizarAccessPointRequestDto`: contiene los nuevos datos.

Primero busca el dispositivo.

Si no existe, devuelve `404 Not Found`.

Si existe, llama a `Actualizar()` del servicio y devuelve `204 No Content`.

## Conceptos importantes

### MapGet(), MapPost(), MapPut() y MapDelete()

Son métodos de Minimal APIs que permiten registrar rutas según el tipo de solicitud HTTP:

- `MapGet()`: consultas.
- `MapPost()`: creación de recursos.
- `MapPut()`: actualización de recursos.
- `MapDelete()`: eliminación de recursos.

### FromServices

`[FromServices]` indica que el parámetro debe obtenerse desde el contenedor de inyección de dependencias.

En este caso, permite recibir una implementación de `IAccessPointServicio`.

### FromBody

`[FromBody]` indica que los datos del parámetro se reciben desde el cuerpo de la solicitud HTTP.

En este caso, se utiliza para recibir los DTO con los datos del Access Point.

### Results

`Results` permite construir respuestas HTTP.

En este archivo se utilizan:

- `Results.Ok()`: respuesta 200.
- `Results.Created()`: respuesta 201.
- `Results.NoContent()`: respuesta 204.
- `Results.NotFound()`: respuesta 404.

### WithTags()

Permite agrupar los endpoints bajo la categoría "Access Points" en Swagger.

Esto facilita encontrar y probar las rutas desde la documentación de la API.

## Relación con otras capas

El funcionamiento sigue este recorrido:

1. El cliente envía una solicitud HTTP.
2. El endpoint recibe los datos.
3. El endpoint llama a `AccessPointServicio` mediante `IAccessPointServicio`.
4. El servicio realiza la operación correspondiente.
5. El servicio utiliza el repositorio para acceder a la base de datos cuando es necesario.
6. El endpoint devuelve una respuesta HTTP al cliente.

## Resumen

`AccessPointEndpoints` define cinco rutas para administrar los Access Points:

- Consultar todos.
- Buscar por ID.
- Crear.
- Eliminar.
- Actualizar.

Su responsabilidad es recibir solicitudes HTTP, comunicarse con la capa Aplicacion y devolver las respuestas correspondientes.

La lógica de negocio está en el servicio y el acceso a los datos está en el repositorio.



# DispositivoEndpoints

## 1. ¿Qué es DispositivoEndpoints?

`DispositivoEndpoints` es una clase estática que contiene los endpoints encargados de administrar los dispositivos de la aplicación.

A diferencia de `AccessPointEndpoints`, que trabaja solamente con Access Points, esta clase permite trabajar con cuatro tipos de dispositivos:

- Router.
- Switch.
- Firewall.
- Access Point.

Su objetivo es ofrecer rutas para listar, buscar, crear, actualizar y eliminar dispositivos desde la API.

## 2. Estructura general

La clase se encuentra dentro del namespace:

```csharp
namespace API.Endpoints
```

Su método principal es:

```csharp
public static void MapDispositivoEndpoints(this IEndpointRouteBuilder app)
```

Este método registra las rutas de los dispositivos en la API.

Recibe `IEndpointRouteBuilder app`, que permite definir los endpoints utilizando Minimal APIs.

## 3. GET: Listar todos los dispositivos

Ruta:

```http
GET /api/dispositivos
```

Código:

```csharp
app.MapGet("/api/dispositivos", (
    IRouterServicio routerServicio,
    ISwitchServicio switchServicio,
    IFirewallServicio firewallServicio,
    IAccessPointServicio accessPointServicio) =>
{
    var dispositivos = new List<DispositivoDTO>();

    foreach (var d in routerServicio.Listar())
        dispositivos.Add(CrearDTO(d.Id, d.Nombre, "Router", d.Latencia));

    foreach (var d in switchServicio.Listar())
        dispositivos.Add(CrearDTO(d.Id, d.Nombre, "Switch", d.Latencia));

    foreach (var d in firewallServicio.Listar())
        dispositivos.Add(CrearDTO(d.Id, d.Nombre, "Firewall", d.Latencia));

    foreach (var d in accessPointServicio.Listar())
        dispositivos.Add(CrearDTO(d.Id, d.Nombre, "AccessPoint", d.Latencia));

    return Results.Ok(dispositivos);
})
.WithTags("Dispositivos");
```

### ¿Cómo funciona?

Primero recibe los cuatro servicios necesarios para consultar los dispositivos.

Después crea una lista vacía de tipo `DispositivoDTO`:

```csharp
var dispositivos = new List<DispositivoDTO>();
```

Esta lista permite reunir los diferentes tipos de dispositivos en una sola colección.

Luego utiliza `Listar()` para obtener los dispositivos de cada servicio.

Por cada dispositivo encontrado, utiliza `CrearDTO()` para convertirlo en un `DispositivoDTO` y agregarlo a la lista.

Finalmente:

```csharp
return Results.Ok(dispositivos);
```

Devuelve la lista completa con el código HTTP `200 OK`.

### ¿Qué es foreach?

`foreach` permite recorrer una colección elemento por elemento.

En este caso, recorre los dispositivos que devuelve cada servicio.

Por ejemplo:

```csharp
foreach (var d in routerServicio.Listar())
    dispositivos.Add(CrearDTO(d.Id, d.Nombre, "Router", d.Latencia));
```

`d` representa cada Router que se está recorriendo.

## 4. GET: Buscar un dispositivo por ID

Ruta:

```http
GET /api/dispositivos/{id}
```

Código:

```csharp
app.MapGet("/api/dispositivos/{id:int}", (
    int id,
    IRouterServicio routerServicio,
    ISwitchServicio switchServicio,
    IFirewallServicio firewallServicio,
    IAccessPointServicio accessPointServicio) =>
{
    var router = routerServicio.ObtenerPorId(id);

    if (router != null)
        return Results.Ok(CrearDTO(router.Id, router.Nombre, "Router", router.Latencia));

    var sw = switchServicio.BuscarPorId(id);

    if (sw != null)
        return Results.Ok(CrearDTO(sw.Id, sw.Nombre, "Switch", sw.Latencia));

    var firewall = firewallServicio.BuscarPorId(id);

    if (firewall != null)
        return Results.Ok(CrearDTO(firewall.Id, firewall.Nombre, "Firewall", firewall.Latencia));

    var accessPoint = accessPointServicio.BuscarPorId(id);

    if (accessPoint != null)
        return Results.Ok(CrearDTO(accessPoint.Id, accessPoint.Nombre, "AccessPoint", accessPoint.Latencia));

    return Results.NotFound();
})
.WithTags("Dispositivos");
```

### ¿Cómo funciona?

Recibe un ID desde la URL y busca el dispositivo en cada uno de los cuatro servicios.

Por ejemplo:

```http
GET /api/dispositivos/5
```

Primero intenta encontrar un Router con ID 5.

Si lo encuentra, devuelve sus datos mediante `CrearDTO()`.

Si no lo encuentra, continúa buscando en Switch, Firewall y Access Point.

Si no encuentra ningún dispositivo, devuelve:

```csharp
return Results.NotFound();
```

Esto representa el código HTTP `404 Not Found`.

## 5. DELETE: Eliminar un dispositivo

Ruta:

```http
DELETE /api/dispositivos/{id}
```

Código:

```csharp
app.MapDelete("/api/dispositivos/{id:int}", (
    int id,
    IRouterServicio routerServicio,
    ISwitchServicio switchServicio,
    IFirewallServicio firewallServicio,
    IAccessPointServicio accessPointServicio) =>
{
    try
    {
        if (routerServicio.ObtenerPorId(id) != null)
        {
            routerServicio.Eliminar(id);
            return Results.NoContent();
        }

        if (switchServicio.BuscarPorId(id) != null)
        {
            switchServicio.Eliminar(id);
            return Results.NoContent();
        }

        if (firewallServicio.BuscarPorId(id) != null)
        {
            firewallServicio.Eliminar(id);
            return Results.NoContent();
        }

        if (accessPointServicio.BuscarPorId(id) != null)
        {
            accessPointServicio.Eliminar(id);
            return Results.NoContent();
        }

        return Results.NotFound();
    }
    catch (MySqlException ex) when (ex.Number == 1217)
    {
        return Results.Conflict(new
        {
            error = "No se puede eliminar el dispositivo porque está siendo utilizado en un recorrido guardado."
        });
    }
})
.WithTags("Dispositivos");
```

### ¿Cómo funciona?

Primero busca el dispositivo en cada servicio.

Cuando encuentra uno, utiliza su método `Eliminar(id)` para eliminarlo.

Si logra eliminarlo, devuelve:

```csharp
return Results.NoContent();
```

Esto representa el código HTTP `204 No Content`.

Si no encuentra el dispositivo, devuelve `404 Not Found`.

### ¿Para qué sirve try y catch?

Se utilizan para controlar posibles errores durante la ejecución.

En este caso, el `try` contiene las operaciones de eliminación.

El `catch` captura una excepción de MySQL:

```csharp
catch (MySqlException ex) when (ex.Number == 1217)
```

El número 1217 identifica un error de MySQL relacionado con una restricción de clave foránea.

Esto permite controlar el caso en el que no se puede eliminar un dispositivo porque está relacionado con un recorrido guardado.

En esa situación, la API devuelve:

```csharp
return Results.Conflict(...);
```

Esto representa el código HTTP `409 Conflict`.

## 6. PUT: Actualizar un dispositivo

Ruta:

```http
PUT /api/dispositivos/{id}
```

Código:

```csharp
app.MapPut("/api/dispositivos/{id:int}", (
    int id,
    ActualizarDispositivoRequestDto solicitud,
    IRouterServicio routerServicio,
    ISwitchServicio switchServicio,
    IFirewallServicio firewallServicio,
    IAccessPointServicio accessPointServicio) =>
{
    string tipo = solicitud.TipoDispositivo?.ToLower() ?? "";

    if (tipo == "router")
    {
        if (routerServicio.ObtenerPorId(id) == null)
            return Results.NotFound();

        routerServicio.Actualizar(id, new ActualizarRouterRequestDto
        {
            Nombre = solicitud.Nombre,
            DireccionIp = solicitud.DireccionIp,
            DireccionMAC = solicitud.DireccionMAC,
            Encendido = solicitud.Encendido,
            PaquetesBloqueados = solicitud.PaquetesBloqueados,
            PaquetesPermitidos = solicitud.PaquetesPermitidos,
            Latencia = solicitud.Latencia
        });
    }
    else if (tipo == "switch")
    {
        if (switchServicio.BuscarPorId(id) == null)
            return Results.NotFound();

        switchServicio.Actualizar(id, new ActualizarSwitchRequestDto
        {
            Nombre = solicitud.Nombre,
            DireccionIp = solicitud.DireccionIp,
            DireccionMAC = solicitud.DireccionMAC,
            Encendido = solicitud.Encendido,
            CantidadPuertos = solicitud.CantidadPuertos,
            PuertosOcupados = solicitud.PuertosOcupados,
            VlanActiva = solicitud.VlanActiva,
            CantidadVlan = solicitud.CantidadVlan,
            Latencia = solicitud.Latencia
        });
    }
    else if (tipo == "firewall")
    {
        if (firewallServicio.BuscarPorId(id) == null)
            return Results.NotFound();

        firewallServicio.Actualizar(id, new ActualizarFirewallRequestDto
        {
            Nombre = solicitud.Nombre,
            DireccionIp = solicitud.DireccionIp,
            DireccionMAC = solicitud.DireccionMAC,
            Encendido = solicitud.Encendido,
            FiltradoActivo = solicitud.FiltradoActivo,
            PoliticaPredeterminada = solicitud.PoliticaPredeterminada,
            CantidadReglas = solicitud.CantidadReglas,
            BloqueaTraficoEntrante = solicitud.BloqueaTraficoEntrante,
            BloqueaTraficoSaliente = solicitud.BloqueaTraficoSaliente,
            Tipo = solicitud.Tipo,
            Latencia = solicitud.Latencia
        });
    }
    else if (tipo == "accesspoint")
    {
        if (accessPointServicio.BuscarPorId(id) == null)
            return Results.NotFound();

        accessPointServicio.Actualizar(id, new ActualizarAccessPointRequestDto
        {
            Nombre = solicitud.Nombre,
            DireccionIp = solicitud.DireccionIp,
            DireccionMAC = solicitud.DireccionMAC,
            Encendido = solicitud.Encendido,
            Ssid = solicitud.Ssid,
            Seguridad = solicitud.Seguridad,
            Canal = solicitud.Canal,
            MaximoDispositivos = solicitud.MaximoDispositivos,
            Latencia = solicitud.Latencia
        });
    }
    else
    {
        return Results.BadRequest("Tipo de dispositivo no válido.");
    }

    return Results.Ok(CrearDTO(
        id,
        solicitud.Nombre,
        solicitud.TipoDispositivo,
        solicitud.Latencia
    ));
})
.WithTags("Dispositivos");
```

### ¿Cómo funciona?

Primero recibe el ID del dispositivo y los datos nuevos mediante `ActualizarDispositivoRequestDto`.

Después obtiene el tipo de dispositivo:

```csharp
string tipo = solicitud.TipoDispositivo?.ToLower() ?? "";
```

Esto convierte el texto a minúsculas para facilitar las comparaciones.

Por ejemplo, `Router`, `ROUTER` y `router` se convierten en `router`.

Luego utiliza condiciones `if` y `else if` para identificar qué tipo de dispositivo se quiere actualizar.

Según el tipo, comprueba que exista y llama al servicio correspondiente.

### ¿Por qué crea diferentes DTO?

Cada tipo de dispositivo tiene propiedades particulares.

Por ejemplo:

- Router tiene paquetes permitidos y bloqueados.
- Switch tiene puertos y VLAN.
- Firewall tiene reglas y políticas.
- Access Point tiene SSID, seguridad y canal.

Por eso se crea el DTO específico que necesita cada servicio.

Al terminar, devuelve `Results.Ok()` con un DTO que representa los datos actualizados.

Si el tipo recibido no es válido, devuelve `400 Bad Request`.

## 7. POST: Crear un dispositivo

Ruta:

```http
POST /api/dispositivos
```

Código:

```csharp
app.MapPost("/api/dispositivos", (
    CrearDispositivoRequestDto solicitud,
    IRouterServicio routerServicio,
    ISwitchServicio switchServicio,
    IFirewallServicio firewallServicio,
    IAccessPointServicio accessPointServicio) =>
{
    if (string.IsNullOrWhiteSpace(solicitud.Nombre))
        return Results.BadRequest(new { error = "El nombre es obligatorio." });

    if (solicitud.LatenciaMs < 0)
        return Results.BadRequest(new { error = "La latencia no puede ser negativa." });

    string direccionIp = $"192.168.1.{Random.Shared.Next(2, 255)}";
    string direccionMAC = "02:" + Guid.NewGuid().ToString("N").Substring(0, 10);

    int id;
    string tipo;

    switch (solicitud.Tipo?.ToLower())
    {
        case "router":
            var router = routerServicio.Crear(new RouterRequestDto
            {
                Nombre = solicitud.Nombre,
                DireccionIp = direccionIp,
                DireccionMAC = direccionMAC,
                Encendido = true,
                PaquetesBloqueados = 0,
                PaquetesPermitidos = 0,
                Latencia = solicitud.LatenciaMs
            });

            id = router.Id;
            tipo = "Router";
            break;

        case "switch":
            var sw = switchServicio.Crear(new SwitchRequestDto
            {
                Nombre = solicitud.Nombre,
                DireccionIp = direccionIp,
                DireccionMAC = direccionMAC,
                Encendido = true,
                CantidadPuertos = 8,
                PuertosOcupados = 0,
                VlanActiva = false,
                CantidadVlan = 0,
                Latencia = solicitud.LatenciaMs
            });

            id = sw.Id;
            tipo = "Switch";
            break;

        case "firewall":
            var firewall = firewallServicio.Crear(new FirewallRequestDto
            {
                Nombre = solicitud.Nombre,
                DireccionIp = direccionIp,
                DireccionMAC = direccionMAC,
                Encendido = true,
                FiltradoActivo = false,
                PoliticaPredeterminada = "permitir",
                CantidadReglas = 0,
                BloqueaTraficoEntrante = false,
                BloqueaTraficoSaliente = false,
                Tipo = "Basico",
                Latencia = solicitud.LatenciaMs
            });

            id = firewall.Id;
            tipo = "Firewall";
            break;

        case "accesspoint":
            var accessPoint = accessPointServicio.Crear(new AccessPointRequestDto
            {
                Nombre = solicitud.Nombre,
                DireccionIp = direccionIp,
                DireccionMAC = direccionMAC,
                Encendido = true,
                Ssid = solicitud.Nombre,
                Seguridad = "WPA2",
                Canal = 6,
                MaximoDispositivos = 10,
                Latencia = solicitud.LatenciaMs
            });

            id = accessPoint.Id;
            tipo = "AccessPoint";
            break;

        default:
            return Results.BadRequest(new { error = "Tipo de dispositivo no válido." });
    }

    var respuesta = CrearDTO(id, solicitud.Nombre, tipo, solicitud.LatenciaMs);

    return Results.Created($"/api/dispositivos/{id}", respuesta);
})
.WithTags("Dispositivos");
```

### ¿Cómo funciona?

Primero recibe los datos enviados por el cliente mediante `CrearDispositivoRequestDto`.

Después realiza algunas validaciones:

- Comprueba que el nombre no esté vacío.
- Comprueba que la latencia no sea negativa.

Luego genera una dirección IP y una dirección MAC para el dispositivo.

### ¿Para qué sirve switch?

`switch` permite ejecutar diferentes bloques de código dependiendo del valor de una variable.

En este caso:

```csharp
switch (solicitud.Tipo?.ToLower())
```

Comprueba qué tipo de dispositivo se quiere crear.

Según el valor recibido, llama al servicio correspondiente:

- `router`: utiliza `routerServicio.Crear()`.
- `switch`: utiliza `switchServicio.Crear()`.
- `firewall`: utiliza `firewallServicio.Crear()`.
- `accesspoint`: utiliza `accessPointServicio.Crear()`.

Cada servicio recibe su propio DTO con las propiedades necesarias.

### ¿Para qué sirven las variables id y tipo?

```csharp
int id;
string tipo;
```

Se declaran antes del `switch` porque necesitamos guardar el ID y el tipo del dispositivo creado.

Por ejemplo:

```csharp
id = router.Id;
tipo = "Router";
```

El ID se obtiene del objeto que devuelve el servicio después de crear el dispositivo.

Al finalizar el `switch`, se utilizan esas variables para construir la respuesta.

### ¿Qué hace break?

`break` termina el bloque `case` que se está ejecutando y sale del `switch`.

### ¿Qué hace CrearDTO?

Después de crear el dispositivo se ejecuta:

```csharp
var respuesta = CrearDTO(id, solicitud.Nombre, tipo, solicitud.LatenciaMs);
```

Esto construye un objeto `DispositivoDTO` con los datos comunes del dispositivo.

Finalmente:

```csharp
return Results.Created($"/api/dispositivos/{id}", respuesta);
```

Devuelve el código HTTP `201 Created`, la ubicación del nuevo dispositivo y sus datos.

## 8. Método auxiliar CrearDTO

Código:

```csharp
private static DispositivoDTO CrearDTO(int id, string nombre, string tipo, int latencia)
{
    return new DispositivoDTO
    {
        Id = id,
        Nombre = nombre,
        Tipo = tipo,
        LatenciaMs = latencia
    };
}
```

Este método permite construir un `DispositivoDTO` a partir de los datos recibidos.

Se utiliza en diferentes endpoints para evitar repetir el mismo código.

### ¿Por qué es private?

Porque solamente se necesita dentro de la clase `DispositivoEndpoints`.

No hace falta que otras clases puedan utilizarlo directamente.

### ¿Por qué es static?

Porque pertenece a la clase y no necesita crear un objeto `DispositivoEndpoints` para utilizarse.

## 9. Inyección de dependencias

Los endpoints reciben los servicios mediante sus parámetros.

Por ejemplo:

```csharp
IRouterServicio routerServicio
```

ASP.NET Core obtiene las implementaciones correspondientes desde la inyección de dependencias, siempre que estén registradas en el sistema.

A diferencia del ejemplo anterior de `AccessPointEndpoints`, acá no se utiliza explícitamente `[FromServices]`.

Minimal APIs pueden reconocer los servicios registrados y obtenerlos automáticamente.

También reciben DTOs como `CrearDispositivoRequestDto` y `ActualizarDispositivoRequestDto`, cuyos datos se obtienen del cuerpo de la petición HTTP.

## 10. Relación con la arquitectura del proyecto

El flujo general es:

1. **API:** recibe la petición HTTP y ejecuta el endpoint correspondiente.
2. **Aplicación:** los servicios realizan las operaciones solicitadas.
3. **Persistencia:** los repositorios se encargan de acceder a la base de datos.

Los endpoints no realizan directamente las consultas SQL. Se comunican con los servicios para realizar las operaciones.

## 11. Resumen final

`DispositivoEndpoints` permite administrar los cuatro tipos de dispositivos desde una misma sección de la API.

Sus principales responsabilidades son:

- Listar todos los dispositivos en una colección común.
- Buscar un dispositivo por ID.
- Eliminar un dispositivo comprobando previamente su existencia.
- Controlar errores de eliminación relacionados con recorridos guardados.
- Actualizar dispositivos según su tipo.
- Crear dispositivos utilizando el servicio correspondiente.
- Construir respuestas comunes mediante `CrearDTO()`.

La clase utiliza los servicios de Aplicación para mantener separadas las responsabilidades y evitar que los endpoints accedan directamente a la base de datos.



# FirewallEndpoints

## 1. ¿Qué es FirewallEndpoints?

`FirewallEndpoints` es una clase estática que contiene los endpoints encargados de administrar los firewalls desde la API.

Permite realizar las operaciones CRUD:

- **GET:** listar todos los firewalls.
- **GET por ID:** buscar un firewall específico.
- **POST:** crear un firewall.
- **DELETE:** eliminar un firewall.
- **PUT:** actualizar los datos de un firewall.

Esta clase utiliza `IFirewallServicio` para comunicarse con la capa de Aplicación. No accede directamente a la base de datos.

## 2. Imports

```csharp
using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
```

- `Aplicacion.DTOs`: permite utilizar los DTO que reciben los datos enviados por el cliente.
- `Aplicacion.Interfaces`: permite acceder a la interfaz `IFirewallServicio`.
- `Microsoft.AspNetCore.Mvc`: proporciona atributos como `[FromBody]` y `[FromServices]`.

## 3. Clase FirewallEndpoints

```csharp
public static class FirewallEndpoints
```

Es una clase estática porque contiene métodos estáticos y no necesitamos crear objetos de esta clase para registrar sus endpoints.

## 4. Método MapFirewallEndpoints

```csharp
public static void MapFirewallEndpoints(this IEndpointRouteBuilder app)
```

Este método registra las rutas de los firewalls en la API.

- `public`: permite acceder al método desde otras clases.
- `static`: permite utilizarlo sin crear un objeto.
- `void`: indica que no devuelve ningún valor.
- `this IEndpointRouteBuilder app`: permite utilizar el método como una extensión de `app`.

## 5. Endpoints

### GET - Listar todos los firewalls

```csharp
app.MapGet("/api/firewalls", ([FromServices] IFirewallServicio servicio) =>
{
    return Results.Ok(servicio.Listar());
})
.WithTags("Firewalls");
```

- `MapGet`: registra una ruta GET.
- `/api/firewalls`: dirección para consultar todos los firewalls.
- `[FromServices]`: obtiene el servicio desde la inyección de dependencias.
- `servicio.Listar()`: solicita la lista de firewalls.
- `Results.Ok()`: devuelve HTTP 200 (OK) junto con los datos.
- `WithTags("Firewalls")`: agrupa este endpoint bajo la categoría Firewalls en Swagger.

### GET - Buscar un firewall por ID

```csharp
app.MapGet("/api/firewalls/{id:int}", (int id, [FromServices] IFirewallServicio servicio) =>
{
    var firewall = servicio.BuscarPorId(id);

    if (firewall is null)
        return Results.NotFound();

    return Results.Ok(firewall);
})
.WithTags("Firewalls");
```

- `{id:int}`: indica que la ruta recibe un parámetro llamado `id` de tipo entero.
- `BuscarPorId(id)`: busca el firewall correspondiente.
- `if (firewall is null)`: comprueba si no se encontró el firewall.
- `Results.NotFound()`: devuelve HTTP 404 (Not Found).
- `Results.Ok(firewall)`: devuelve HTTP 200 junto con el firewall encontrado.

### POST - Crear un firewall

```csharp
app.MapPost("/api/firewalls", ([FromBody] FirewallRequestDto solicitud,
    [FromServices] IFirewallServicio servicio) =>
{
    var firewall = servicio.Crear(solicitud);

    return Results.Created($"/api/firewalls/{firewall.Id}", firewall);
})
.WithTags("Firewalls");
```

- `MapPost`: registra una ruta POST.
- `[FromBody]`: recibe los datos enviados en el cuerpo de la solicitud HTTP.
- `FirewallRequestDto`: objeto que contiene los datos necesarios para crear el firewall.
- `solicitud`: variable que almacena esos datos.
- `servicio.Crear(solicitud)`: llama al servicio para crear y guardar el firewall.
- `Results.Created()`: devuelve HTTP 201 (Created), indicando que el recurso fue creado.
- `$"/api/firewalls/{firewall.Id}"`: construye la dirección del firewall creado, insertando su ID.
- `firewall`: es el objeto que se devuelve en el cuerpo de la respuesta.

### DELETE - Eliminar un firewall

```csharp
app.MapDelete("/api/firewalls/{id:int}", (int id, [FromServices] IFirewallServicio servicio) =>
{
    var firewall = servicio.BuscarPorId(id);

    if (firewall is null)
        return Results.NotFound();

    servicio.Eliminar(id);

    return Results.NoContent();
})
.WithTags("Firewalls");
```

- `MapDelete`: registra una ruta DELETE.
- `BuscarPorId(id)`: comprueba si existe el firewall.
- `if (firewall is null)`: verifica si no se encontró.
- `Results.NotFound()`: devuelve HTTP 404 si no existe.
- `servicio.Eliminar(id)`: solicita al servicio que elimine el firewall.
- `Results.NoContent()`: devuelve HTTP 204 (No Content), indicando que la operación se realizó correctamente y no hay contenido adicional para devolver.

### PUT - Actualizar un firewall

```csharp
app.MapPut("/api/firewalls/{id:int}", (
    int id,
    [FromBody] ActualizarFirewallRequestDto solicitud,
    [FromServices] IFirewallServicio servicio) =>
{
    var firewall = servicio.BuscarPorId(id);

    if (firewall is null)
        return Results.NotFound();

    servicio.Actualizar(id, solicitud);

    return Results.NoContent();
})
.WithTags("Firewalls");
```

- `MapPut`: registra una ruta PUT.
- `id`: identifica el firewall que se quiere modificar.
- `[FromBody]`: recibe los nuevos datos desde el cuerpo de la solicitud HTTP.
- `ActualizarFirewallRequestDto`: DTO que contiene los datos para actualizar el firewall.
- `BuscarPorId(id)`: comprueba que el firewall exista.
- `Results.NotFound()`: devuelve HTTP 404 si no existe.
- `servicio.Actualizar(id, solicitud)`: llama al servicio para modificar sus datos.
- `Results.NoContent()`: devuelve HTTP 204 cuando termina la operación.

## 6. Inyección de dependencias

Los endpoints reciben `IFirewallServicio` mediante la inyección de dependencias.

Esto permite que la API utilice el servicio sin tener que crear manualmente un objeto de `FirewallServicio`.

El endpoint se encarga de recibir la solicitud HTTP y devolver una respuesta. La lógica de creación, búsqueda, actualización y eliminación queda en la capa de Aplicación.

## 7. Principios SOLID

- **SRP (Responsabilidad Única):** la clase se encarga de definir las rutas HTTP relacionadas con los firewalls.
- **DIP (Inversión de Dependencias):** utiliza la abstracción `IFirewallServicio` en lugar de depender directamente de la clase concreta `FirewallServicio`.

## 8. Resumen

`FirewallEndpoints` permite administrar los firewalls mediante solicitudes HTTP.

Utiliza los métodos del servicio `IFirewallServicio` para realizar las operaciones y devuelve los códigos HTTP correspondientes según el resultado.

De esta manera, los endpoints mantienen separada la comunicación con el cliente de la lógica de negocio y del acceso a la base de datos.



# RouterEndpoints

## 1. ¿Qué es RouterEndpoints?

`RouterEndpoints` es una clase estática que contiene los endpoints encargados de administrar los routers desde la API.

Permite realizar las operaciones CRUD:

- **GET:** listar todos los routers.
- **GET por ID:** buscar un router específico.
- **POST:** crear un router.
- **DELETE:** eliminar un router.
- **PUT:** actualizar los datos de un router.

Esta clase utiliza `IRouterServicio` para comunicarse con la capa de Aplicación. No accede directamente a la base de datos.

## 2. Imports

```csharp
using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
```

- `Aplicacion.DTOs`: permite utilizar los DTO que reciben los datos enviados por el cliente.
- `Aplicacion.Interfaces`: permite acceder a la interfaz `IRouterServicio`.
- `Microsoft.AspNetCore.Mvc`: proporciona atributos como `[FromBody]` y `[FromServices]`.

## 3. Clase RouterEndpoints

```csharp
public static class RouterEndpoints
```

Es una clase estática porque contiene métodos estáticos y no necesitamos crear objetos de esta clase para registrar sus endpoints.

## 4. Método MapRouterEndpoints

```csharp
public static void MapRouterEndpoints(this IEndpointRouteBuilder app)
```

Este método registra las rutas de los routers en la API.

- `public`: permite acceder al método desde otras clases.
- `static`: permite utilizarlo sin crear un objeto.
- `void`: indica que no devuelve ningún valor.
- `this IEndpointRouteBuilder app`: permite utilizar el método como una extensión de `app`.

## 5. Endpoints

### GET - Listar todos los routers

```csharp
app.MapGet("/api/routers", ([FromServices] IRouterServicio servicio) =>
{
    return Results.Ok(servicio.Listar());
})
.WithTags("Routers");
```

- `MapGet`: registra una ruta GET.
- `/api/routers`: dirección para consultar todos los routers.
- `[FromServices]`: obtiene el servicio desde la inyección de dependencias.
- `servicio.Listar()`: solicita la lista de routers.
- `Results.Ok()`: devuelve HTTP 200 (OK) junto con los datos.
- `WithTags("Routers")`: agrupa este endpoint bajo la categoría Routers en Swagger.

### GET - Buscar un router por ID

```csharp
app.MapGet("/api/routers/{id:int}", (int id, [FromServices] IRouterServicio servicio) =>
{
    var router = servicio.ObtenerPorId(id);

    if (router is null)
        return Results.NotFound();

    return Results.Ok(router);
})
.WithTags("Routers");
```

- `{id:int}`: indica que la ruta recibe un parámetro llamado `id` de tipo entero.
- `ObtenerPorId(id)`: busca el router correspondiente.
- `if (router is null)`: comprueba si no se encontró el router.
- `Results.NotFound()`: devuelve HTTP 404 (Not Found).
- `Results.Ok(router)`: devuelve HTTP 200 junto con el router encontrado.

### POST - Crear un router

```csharp
app.MapPost("/api/routers",
    ([FromBody] RouterRequestDto solicitud,
    [FromServices] IRouterServicio servicio) =>
{
    var router = servicio.Crear(solicitud);

    return Results.Created(
        $"/api/routers/{router.Id}",
        router
    );
})
.WithTags("Routers");
```

- `MapPost`: registra una ruta POST.
- `[FromBody]`: recibe los datos enviados en el cuerpo de la solicitud HTTP.
- `RouterRequestDto`: objeto que contiene los datos necesarios para crear el router.
- `solicitud`: variable que almacena esos datos.
- `[FromServices]`: obtiene el servicio desde la inyección de dependencias.
- `servicio.Crear(solicitud)`: llama al servicio para crear y guardar el router.
- `Results.Created()`: devuelve HTTP 201 (Created), indicando que el recurso fue creado.
- `$"/api/routers/{router.Id}"`: construye la dirección del router creado, insertando su ID.
- `router`: es el objeto que se devuelve en el cuerpo de la respuesta.

### DELETE - Eliminar un router

```csharp
app.MapDelete("/api/routers/{id:int}", (int id, [FromServices] IRouterServicio servicio) =>
{
    var router = servicio.ObtenerPorId(id);

    if (router is null)
        return Results.NotFound();

    servicio.Eliminar(id);

    return Results.NoContent();
})
.WithTags("Routers");
```

- `MapDelete`: registra una ruta DELETE.
- `ObtenerPorId(id)`: comprueba si existe el router.
- `if (router is null)`: verifica si no se encontró.
- `Results.NotFound()`: devuelve HTTP 404 si no existe.
- `servicio.Eliminar(id)`: solicita al servicio que elimine el router.
- `Results.NoContent()`: devuelve HTTP 204 (No Content), indicando que la operación se realizó correctamente y no hay contenido adicional para devolver.

### PUT - Actualizar un router

```csharp
app.MapPut("/api/routers/{id:int}", (
    int id,
    [FromBody] ActualizarRouterRequestDto solicitud,
    [FromServices] IRouterServicio servicio) =>
{
    var router = servicio.ObtenerPorId(id);

    if (router is null)
        return Results.NotFound();

    servicio.Actualizar(id, solicitud);

    return Results.NoContent();
})
.WithTags("Routers");
```

- `MapPut`: registra una ruta PUT.
- `id`: identifica el router que se quiere modificar.
- `[FromBody]`: recibe los nuevos datos desde el cuerpo de la solicitud HTTP.
- `ActualizarRouterRequestDto`: DTO que contiene los datos para actualizar el router.
- `ObtenerPorId(id)`: comprueba que el router exista.
- `Results.NotFound()`: devuelve HTTP 404 si no existe.
- `servicio.Actualizar(id, solicitud)`: llama al servicio para modificar sus datos.
- `Results.NoContent()`: devuelve HTTP 204 cuando termina la operación.

## 6. Inyección de dependencias

Los endpoints reciben `IRouterServicio` mediante la inyección de dependencias.

Esto permite que la API utilice el servicio sin tener que crear manualmente un objeto de `RouterServicio`.

El endpoint se encarga de recibir la solicitud HTTP y devolver una respuesta. La lógica de creación, búsqueda, actualización y eliminación queda en la capa de Aplicación.

## 7. Principios SOLID

- **SRP (Responsabilidad Única):** la clase se encarga de definir las rutas HTTP relacionadas con los routers.
- **DIP (Inversión de Dependencias):** utiliza la abstracción `IRouterServicio` en lugar de depender directamente de la clase concreta `RouterServicio`.

## 8. Resumen

`RouterEndpoints` permite administrar los routers mediante solicitudes HTTP.

Utiliza los métodos del servicio `IRouterServicio` para realizar las operaciones y devuelve los códigos HTTP correspondientes según el resultado.

De esta manera, los endpoints mantienen separada la comunicación con el cliente de la lógica de negocio y del acceso a la base de datos.



# SimulacionEndpoints

## 1. ¿Qué es SimulacionEndpoints?

`SimulacionEndpoints` es una clase estática que contiene los endpoints encargados de administrar las simulaciones desde la API.

Permite realizar las siguientes operaciones:

- **GET:** listar todas las simulaciones.
- **GET por ID:** buscar una simulación específica.
- **POST:** crear y ejecutar una simulación.
- **DELETE:** eliminar una simulación.
- **PUT:** actualizar una simulación completa.
- **PUT de pasos:** modificar un dispositivo de un paso específico del recorrido.

Esta clase utiliza `ISimulacionServicio` para comunicarse con la capa de Aplicación. No accede directamente a la base de datos.

## 2. Imports

```csharp
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Aplicacion.DTOs;
```

- `Aplicacion.Interfaces`: permite acceder a la interfaz `ISimulacionServicio`.
- `Microsoft.AspNetCore.Mvc`: proporciona atributos como `[FromBody]` y `[FromServices]`.
- `Aplicacion.DTOs`: permite utilizar los DTO que reciben los datos enviados por el cliente.

## 3. Clase SimulacionEndpoints

```csharp
public static class SimulacionEndpoints
```

Es una clase estática porque contiene métodos estáticos y no necesitamos crear objetos de esta clase para registrar sus endpoints.

## 4. Método MapSimulacionEndpoints

```csharp
public static void MapSimulacionEndpoints(this IEndpointRouteBuilder app)
```

Este método registra las rutas de las simulaciones en la API.

- `public`: permite acceder al método desde otras clases.
- `static`: permite utilizarlo sin crear un objeto.
- `void`: indica que no devuelve ningún valor.
- `this IEndpointRouteBuilder app`: permite utilizar el método como una extensión de `app`.

## 5. Endpoints

### GET - Listar todas las simulaciones

```csharp
app.MapGet("/api/simulaciones", ([FromServices] ISimulacionServicio servicio) =>
{
    var simulaciones = servicio.Listar();

    return Results.Ok(simulaciones);
})
.WithTags("Simulaciones");
```

- `MapGet`: registra una ruta GET.
- `/api/simulaciones`: dirección para consultar todas las simulaciones.
- `[FromServices]`: obtiene el servicio desde la inyección de dependencias.
- `servicio.Listar()`: solicita la lista de simulaciones.
- `var simulaciones`: guarda los resultados obtenidos.
- `Results.Ok(simulaciones)`: devuelve HTTP 200 (OK) junto con la lista.
- `WithTags("Simulaciones")`: agrupa este endpoint bajo la categoría Simulaciones en Swagger.

### GET - Buscar una simulación por ID

```csharp
app.MapGet("/api/simulaciones/{id:int}", (int id, [FromServices] ISimulacionServicio servicio) =>
{
    var simulacion = servicio.BuscarPorId(id);

    if (simulacion is null)
        return Results.NotFound();

    return Results.Ok(simulacion);
})
.WithTags("Simulaciones");
```

- `{id:int}`: indica que la ruta recibe un parámetro llamado `id` de tipo entero.
- `BuscarPorId(id)`: busca la simulación correspondiente.
- `if (simulacion is null)`: comprueba si no se encontró la simulación.
- `Results.NotFound()`: devuelve HTTP 404 (Not Found).
- `Results.Ok(simulacion)`: devuelve HTTP 200 junto con la simulación encontrada.

### POST - Crear una simulación

```csharp
app.MapPost("/api/simulaciones",
    ([FromBody] SimulacionRequestDto solicitud,
    [FromServices] ISimulacionServicio servicio) =>
{
    var resultado = servicio.CrearSimulacion(solicitud);

    return Results.Created(
        $"/api/simulaciones/{resultado.Id}",
        resultado
    );
})
.WithTags("Simulaciones");
```

- `MapPost`: registra una ruta POST.
- `[FromBody]`: recibe los datos enviados en el cuerpo de la solicitud HTTP.
- `SimulacionRequestDto`: DTO que contiene los datos necesarios para crear la simulación.
- `solicitud`: variable que almacena esos datos.
- `[FromServices]`: obtiene el servicio desde la inyección de dependencias.
- `servicio.CrearSimulacion(solicitud)`: llama al servicio para crear y procesar la simulación.
- `resultado`: guarda el resultado devuelto por el servicio.
- `Results.Created()`: devuelve HTTP 201 (Created), indicando que el recurso fue creado.
- `$"/api/simulaciones/{resultado.Id}"`: construye la dirección de la simulación creada, insertando su ID.
- `resultado`: es el objeto que se devuelve en el cuerpo de la respuesta.

**Importante:** en este proyecto, `CrearSimulacion()` no solamente guarda una simulación. También prepara los dispositivos, crea el paquete y ejecuta el recorrido por la red.

### DELETE - Eliminar una simulación

```csharp
app.MapDelete("/api/simulaciones/{id:int}",
    (int id, [FromServices] ISimulacionServicio servicio) =>
{
    if (servicio.BuscarPorId(id) == null)
        return Results.NotFound();

    servicio.Eliminar(id);

    return Results.NoContent();
})
.WithTags("Simulaciones");
```

- `MapDelete`: registra una ruta DELETE.
- `{id:int}`: identifica la simulación que se quiere eliminar.
- `BuscarPorId(id)`: comprueba si existe la simulación.
- `== null`: verifica si la búsqueda no encontró ningún resultado.
- `Results.NotFound()`: devuelve HTTP 404 si no existe.
- `servicio.Eliminar(id)`: solicita al servicio que elimine la simulación.
- `Results.NoContent()`: devuelve HTTP 204 (No Content), indicando que la operación se realizó correctamente y no hay contenido adicional para devolver.

### PUT - Actualizar una simulación completa

```csharp
app.MapPut("/api/simulaciones/{id:int}",
    (int id,
    [FromBody] SimulacionRequestDto solicitud,
    [FromServices] ISimulacionServicio servicio) =>
{
    var resultado = servicio.ActualizarSimulacion(id, solicitud);

    return Results.Ok(resultado);
})
.WithTags("Simulaciones");
```

- `MapPut`: registra una ruta PUT.
- `{id:int}`: identifica la simulación que se quiere actualizar.
- `[FromBody]`: recibe los nuevos datos desde el cuerpo de la solicitud HTTP.
- `SimulacionRequestDto`: DTO que contiene los datos para actualizar la simulación.
- `servicio.ActualizarSimulacion(id, solicitud)`: llama al servicio para modificar la simulación y volver a procesar su recorrido.
- `resultado`: guarda la información devuelta por el servicio.
- `Results.Ok(resultado)`: devuelve HTTP 200 (OK) junto con el resultado actualizado.

A diferencia de los PUT anteriores, este endpoint no comprueba directamente si la simulación existe. Esa responsabilidad queda dentro del método `ActualizarSimulacion()` del servicio.

### PUT - Actualizar un paso del recorrido

```csharp
app.MapPut("/api/simulaciones/{id:int}/pasos/{orden:int}",
    (int id,
    int orden,
    [FromBody] ActualizarPasoRequestDto solicitud,
    [FromServices] ISimulacionServicio servicio) =>
{
    var resultado = servicio.ActualizarPaso(id, orden, solicitud);

    return Results.Ok(resultado);
})
.WithTags("Simulaciones");
```

Este endpoint permite modificar un dispositivo que participa en un paso específico del recorrido de una simulación.

- `MapPut`: registra una ruta PUT.
- `{id:int}`: identifica la simulación.
- `{orden:int}`: identifica el número de orden del paso que queremos modificar.
- `int id`: recibe el ID de la simulación.
- `int orden`: recibe el orden del paso que se quiere modificar.
- `[FromBody] ActualizarPasoRequestDto solicitud`: recibe los datos del nuevo dispositivo que se utilizará en ese paso.
- `servicio.ActualizarPaso(id, orden, solicitud)`: llama al servicio para modificar el paso y volver a procesar el recorrido.
- `resultado`: guarda la respuesta del servicio.
- `Results.Ok(resultado)`: devuelve HTTP 200 junto con el resultado actualizado.

**Ejemplo:**

Supongamos que tenemos la simulación con ID 5 y queremos modificar el segundo paso de su recorrido.

La ruta sería:

```text
PUT /api/simulaciones/5/pasos/2
```

En este caso:

- `5` es el ID de la simulación.
- `2` es el orden del paso que queremos modificar.
- El cuerpo de la solicitud contiene los datos del nuevo dispositivo.

El servicio se encarga de encontrar ese paso, reemplazar el dispositivo correspondiente y volver a procesar la simulación.

## 6. Inyección de dependencias

Los endpoints reciben `ISimulacionServicio` mediante la inyección de dependencias.

Esto permite que la API utilice el servicio sin tener que crear manualmente un objeto de `SimulacionServicio`.

El endpoint se encarga de recibir la solicitud HTTP y devolver una respuesta. La lógica para crear, actualizar, procesar y eliminar simulaciones queda en la capa de Aplicación.

## 7. Principios SOLID

- **SRP (Responsabilidad Única):** la clase se encarga de definir las rutas HTTP relacionadas con las simulaciones.
- **DIP (Inversión de Dependencias):** utiliza la abstracción `ISimulacionServicio` en lugar de depender directamente de la clase concreta `SimulacionServicio`.

## 8. Resumen

`SimulacionEndpoints` permite administrar las simulaciones mediante solicitudes HTTP.

Además de las operaciones CRUD, cuenta con dos operaciones especiales:

- Actualizar una simulación completa y volver a procesar su recorrido.
- Modificar un paso específico del recorrido y volver a procesar la simulación.

De esta manera, los endpoints mantienen separada la comunicación con el cliente de la lógica de negocio y del acceso a la base de datos.



# SwitchEndpoints

## 1. ¿Qué es SwitchEndpoints?

`SwitchEndpoints` es una clase estática que contiene los endpoints encargados de administrar los switches de red desde la API.

Permite realizar las operaciones CRUD:

- **GET:** listar todos los switches.
- **GET por ID:** buscar un switch específico.
- **POST:** crear un switch.
- **DELETE:** eliminar un switch.
- **PUT:** actualizar los datos de un switch.

Esta clase utiliza `ISwitchServicio` para comunicarse con la capa de Aplicación. No accede directamente a la base de datos.

## 2. Imports

```csharp
using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;
```

- `Aplicacion.DTOs`: permite utilizar los DTO que reciben los datos enviados por el cliente.
- `Aplicacion.Interfaces`: permite acceder a la interfaz `ISwitchServicio`.
- `Microsoft.AspNetCore.Mvc`: proporciona atributos como `[FromBody]` y `[FromServices]`.

## 3. Clase SwitchEndpoints

```csharp
public static class SwitchEndpoints
```

Es una clase estática porque contiene métodos estáticos y no necesitamos crear objetos de esta clase para registrar sus endpoints.

## 4. Método MapSwitchEndpoints

```csharp
public static void MapSwitchEndpoints(this IEndpointRouteBuilder app)
```

Este método registra las rutas de los switches en la API.

- `public`: permite acceder al método desde otras clases.
- `static`: permite utilizarlo sin crear un objeto.
- `void`: indica que no devuelve ningún valor.
- `this IEndpointRouteBuilder app`: permite utilizar el método como una extensión de `app`.

## 5. Endpoints

### GET - Listar todos los switches

```csharp
app.MapGet("/api/switches", ([FromServices] ISwitchServicio servicio) =>
{
    return Results.Ok(servicio.Listar());
})
.WithTags("Switches");
```

- `MapGet`: registra una ruta GET.
- `/api/switches`: dirección para consultar todos los switches.
- `[FromServices]`: obtiene el servicio desde la inyección de dependencias.
- `servicio.Listar()`: solicita la lista de switches.
- `Results.Ok()`: devuelve HTTP 200 (OK) junto con los datos.
- `WithTags("Switches")`: agrupa este endpoint bajo la categoría Switches en Swagger.

### GET - Buscar un switch por ID

```csharp
app.MapGet("/api/switches/{id:int}", (int id, [FromServices] ISwitchServicio servicio) =>
{
    var switchRed = servicio.BuscarPorId(id);

    if (switchRed is null)
        return Results.NotFound();

    return Results.Ok(switchRed);
})
.WithTags("Switches");
```

- `{id:int}`: indica que la ruta recibe un parámetro llamado `id` de tipo entero.
- `BuscarPorId(id)`: busca el switch correspondiente.
- `switchRed`: variable que almacena el switch encontrado.
- `if (switchRed is null)`: comprueba si no se encontró el switch.
- `Results.NotFound()`: devuelve HTTP 404 (Not Found).
- `Results.Ok(switchRed)`: devuelve HTTP 200 junto con el switch encontrado.

**¿Por qué se llama `switchRed` y no `switch`?**

Porque `switch` es una palabra reservada de C#. Se utiliza para las estructuras de selección `switch`, por lo que no podemos utilizarla como nombre de variable.

Por eso usamos `switchRed` para identificar al objeto de tipo switch de red.

### POST - Crear un switch

```csharp
app.MapPost("/api/switches", ([FromBody] SwitchRequestDto solicitud,
    [FromServices] ISwitchServicio servicio) =>
{
    var switchRed = servicio.Crear(solicitud);

    return Results.Created($"/api/switches/{switchRed.Id}", switchRed);
})
.WithTags("Switches");
```

- `MapPost`: registra una ruta POST.
- `[FromBody]`: recibe los datos enviados en el cuerpo de la solicitud HTTP.
- `SwitchRequestDto`: objeto que contiene los datos necesarios para crear el switch.
- `solicitud`: variable que almacena esos datos.
- `[FromServices]`: obtiene el servicio desde la inyección de dependencias.
- `servicio.Crear(solicitud)`: llama al servicio para crear y guardar el switch.
- `switchRed`: guarda el objeto creado.
- `Results.Created()`: devuelve HTTP 201 (Created), indicando que el recurso fue creado.
- `$"/api/switches/{switchRed.Id}"`: construye la dirección del switch creado, insertando su ID.
- `switchRed`: es el objeto que se devuelve en el cuerpo de la respuesta.

### DELETE - Eliminar un switch

```csharp
app.MapDelete("/api/switches/{id:int}", (int id, [FromServices] ISwitchServicio servicio) =>
{
    var switchRed = servicio.BuscarPorId(id);

    if (switchRed is null)
        return Results.NotFound();

    servicio.Eliminar(id);

    return Results.NoContent();
})
.WithTags("Switches");
```

- `MapDelete`: registra una ruta DELETE.
- `BuscarPorId(id)`: comprueba si existe el switch.
- `if (switchRed is null)`: verifica si no se encontró.
- `Results.NotFound()`: devuelve HTTP 404 si no existe.
- `servicio.Eliminar(id)`: solicita al servicio que elimine el switch.
- `Results.NoContent()`: devuelve HTTP 204 (No Content), indicando que la operación se realizó correctamente y no hay contenido adicional para devolver.

### PUT - Actualizar un switch

```csharp
app.MapPut("/api/switches/{id:int}", (
    int id,
    [FromBody] ActualizarSwitchRequestDto solicitud,
    [FromServices] ISwitchServicio servicio) =>
{
    var switchRed = servicio.BuscarPorId(id);

    if (switchRed is null)
        return Results.NotFound();

    servicio.Actualizar(id, solicitud);

    return Results.NoContent();
})
.WithTags("Switches");
```

- `MapPut`: registra una ruta PUT.
- `{id:int}`: identifica el switch que se quiere modificar.
- `int id`: recibe el ID del switch.
- `[FromBody]`: recibe los nuevos datos desde el cuerpo de la solicitud HTTP.
- `ActualizarSwitchRequestDto`: DTO que contiene los datos para actualizar el switch.
- `BuscarPorId(id)`: comprueba que el switch exista.
- `if (switchRed is null)`: verifica si no se encontró.
- `Results.NotFound()`: devuelve HTTP 404 si no existe.
- `servicio.Actualizar(id, solicitud)`: llama al servicio para modificar sus datos.
- `Results.NoContent()`: devuelve HTTP 204 cuando termina la operación correctamente.

## 6. Inyección de dependencias

Los endpoints reciben `ISwitchServicio` mediante la inyección de dependencias.

Esto permite que la API utilice el servicio sin tener que crear manualmente un objeto de `SwitchServicio`.

El endpoint se encarga de recibir la solicitud HTTP y devolver una respuesta. La lógica de creación, búsqueda, actualización y eliminación queda en la capa de Aplicación.

## 7. Principios SOLID

- **SRP (Responsabilidad Única):** la clase se encarga de definir las rutas HTTP relacionadas con los switches.
- **DIP (Inversión de Dependencias):** utiliza la abstracción `ISwitchServicio` en lugar de depender directamente de la clase concreta `SwitchServicio`.

## 8. Resumen

`SwitchEndpoints` permite administrar los switches mediante solicitudes HTTP.

Utiliza los métodos del servicio `ISwitchServicio` para realizar las operaciones y devuelve los códigos HTTP correspondientes según el resultado.

De esta manera, los endpoints mantienen separada la comunicación con el cliente de la lógica de negocio y del acceso a la base de datos.