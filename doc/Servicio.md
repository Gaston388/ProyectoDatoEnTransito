# AccessPoint – Entidad

## 1. ¿Qué es la clase AccessPoint?

`AccessPoint` es una clase que pertenece a la capa de Persistencia, dentro de la carpeta de Entidades del proyecto Datos En Tránsito.

Representa un punto de acceso inalámbrico (Access Point), que es un dispositivo de red que permite conectar otros dispositivos mediante una red Wi-Fi.

Esta clase contiene los atributos y comportamientos propios de un punto de acceso.

**Ubicación:** `src/Persistencia/Entidades/AccessPoint.cs`

## 2. ¿Para qué sirve?

La clase `AccessPoint` sirve para representar los puntos de acceso que forman parte de la red simulada.

Almacena información general del dispositivo, como su nombre, dirección IP, dirección MAC y estado de encendido.

También contiene información específica de un Access Point:

- **SSID:** nombre de la red Wi-Fi.
- **Seguridad:** tipo de protección utilizada por la red.
- **Canal:** canal de Wi-Fi utilizado.
- **MaximoDispositivos:** cantidad máxima de dispositivos que pueden conectarse.

Además, permite determinar si el punto de acceso puede procesar un paquete durante una simulación.

## 3. Herencia

```csharp
public class AccessPoint : DispositivoRed
```

La clase `AccessPoint` hereda de `DispositivoRed`.

Esto significa que recibe los atributos y comportamientos generales definidos en la clase padre.

Por ejemplo:

- `Id`
- `Nombre`
- `DireccionIp`
- `DireccionMAC`
- `Encendido`
- `Latencia`

De esta manera, no es necesario volver a declarar esos atributos dentro de `AccessPoint`.

La clase solamente agrega las propiedades específicas de un punto de acceso.

## 4. Atributos y propiedades

### 4.1. SSID

```csharp
private string ssid = string.Empty;

public string Ssid
{
    get => ssid;
    set
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El SSID no puede estar vacío.");
        }
        ssid = value;
    }
}
```

El SSID representa el nombre de la red Wi-Fi.

Por ejemplo: `Red_Oficina`.

- `private string ssid`: almacena internamente el nombre de la red.
- `get`: permite obtener el valor almacenado.
- `set`: permite asignar un nuevo valor.
- `string.IsNullOrWhiteSpace(value)`: comprueba si el valor está vacío o contiene solamente espacios.
- `throw new ArgumentException`: genera una excepción si el nombre no es válido.

De esta manera, se evita que un Access Point tenga un SSID vacío.

### 4.2. Seguridad

```csharp
private string seguridad = string.Empty;

public string Seguridad
{
    get => seguridad;
    set
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException(
                "La seguridad no puede estar vacía.");

        if (value != "WPA2" && value != "WPA3")
            throw new ArgumentException(
                "La seguridad debe ser WPA2 o WPA3.");

        seguridad = value;
    }
}
```

Representa el tipo de protección utilizado por la red Wi-Fi.

En nuestro proyecto se permiten dos valores:

- `WPA2`
- `WPA3`

El método `set` realiza dos validaciones:

1. Comprueba que el valor no esté vacío.
2. Comprueba que el valor sea WPA2 o WPA3.

Si no cumple alguna de estas condiciones, genera una excepción.

### 4.3. Canal

```csharp
private int canal;

public int Canal
{
    get => canal;
    set
    {
        if (value < 1 || value > 14)
        {
            throw new ArgumentOutOfRangeException(
                nameof(value),
                "El canal debe estar entre 1 y 14.");
        }
        canal = value;
    }
}
```

Representa el canal de Wi-Fi utilizado por el punto de acceso.

En esta clase se establece que el canal debe estar entre 1 y 14.

- `private int canal`: almacena el número del canal.
- `get`: permite consultar el canal.
- `set`: permite modificarlo.
- `value < 1 || value > 14`: comprueba que el valor esté dentro del rango permitido.
- `ArgumentOutOfRangeException`: genera una excepción cuando el valor está fuera del rango.

### 4.4. MaximoDispositivos

```csharp
private int maximoDispositivos;

public int MaximoDispositivos
{
    get => maximoDispositivos;
    set
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(value),
                "La cantidad máxima de dispositivos debe ser mayor que 0.");
        }
        maximoDispositivos = value;
    }
}
```

Representa la cantidad máxima de dispositivos que pueden conectarse al Access Point.

La validación comprueba que el valor sea mayor que cero.

Si se intenta asignar cero o un número negativo, se genera una excepción.

## 5. Constructores

La clase contiene tres constructores diferentes.

### 5.1. Constructor con todos los atributos, incluido el ID

```csharp
public AccessPoint(
    int id,
    string nombre,
    string direccionIp,
    string direccionMAC,
    bool encendido,
    string ssid,
    string seguridad,
    int canal,
    int maximoDispositivos,
    int latencia)
    : base(id, nombre, direccionIp, direccionMAC, encendido, latencia)
{
    Ssid = ssid;
    Seguridad = seguridad;
    Canal = canal;
    MaximoDispositivos = maximoDispositivos;
}
```

Este constructor permite crear un Access Point indicando todos sus datos, incluido su identificador.

Utiliza `: base(...)` para enviar los atributos generales al constructor de la clase padre `DispositivoRed`.

Después asigna los atributos específicos del Access Point.

### 5.2. Constructor vacío

```csharp
public AccessPoint()
    : base()
{
}
```

Permite crear un objeto `AccessPoint` sin pasarle argumentos.

Llama al constructor vacío de `DispositivoRed` mediante `base()`.

Puede resultar útil cuando se necesita crear un objeto y completar sus propiedades posteriormente.

### 5.3. Constructor sin ID

```csharp
public AccessPoint(
    string nombre,
    string direccionIp,
    string direccionMAC,
    bool encendido,
    string ssid,
    string seguridad,
    int canal,
    int maximoDispositivos,
    int latencia)
    : base(nombre, direccionIp, direccionMAC, encendido, latencia)
{
    Ssid = ssid;
    Seguridad = seguridad;
    Canal = canal;
    MaximoDispositivos = maximoDispositivos;
}
```

Este constructor permite crear un Access Point sin indicar su identificador.

Los datos generales se envían al constructor de `DispositivoRed` y los datos específicos se asignan dentro de la clase.

En nuestro proyecto, este constructor se utiliza cuando se crea un nuevo Access Point desde el servicio.

## 6. Método ProcesarPaquete()

```csharp
public override bool ProcesarPaquete(PaqueteRed paquete)
{
    if (!Encendido)
    {
        return false;
    }
    return true;
}
```

Este método determina si el Access Point puede procesar un paquete durante una simulación.

Utiliza `override` porque está sobrescribiendo el método virtual definido en la clase padre `DispositivoRed`.

**Funcionamiento:**

1. Comprueba si el Access Point está encendido.
2. Si está apagado, devuelve `false`, indicando que no puede procesar el paquete.
3. Si está encendido, devuelve `true`, indicando que puede procesarlo.

El tipo `bool` significa que el método devuelve un valor verdadero o falso.

En esta implementación, el Access Point solamente comprueba si está encendido. No realiza otras validaciones sobre el paquete.

## 7. Conceptos de programación utilizados

### Encapsulamiento

Se utiliza para proteger los atributos internos de la clase.

Por ejemplo, `ssid` es privado y solamente se puede modificar mediante la propiedad pública `Ssid`, que realiza una validación antes de guardar el valor.

### Herencia

`AccessPoint` hereda de `DispositivoRed`, por lo que reutiliza los atributos y comportamientos generales de un dispositivo de red.

### Polimorfismo

Se aplica mediante el método `ProcesarPaquete()`.

La clase sobrescribe el comportamiento definido en `DispositivoRed` para establecer cómo procesa paquetes un Access Point.

Esto permite que diferentes tipos de dispositivos tengan su propia implementación del procesamiento de paquetes.

### Validaciones

Las propiedades `Ssid`, `Seguridad`, `Canal` y `MaximoDispositivos` contienen validaciones para evitar asignar valores incorrectos.

## 8. Relación con otras clases

La clase `AccessPoint` se relaciona principalmente con:

- **DispositivoRed:** es su clase padre y proporciona los atributos y comportamientos generales de los dispositivos de red.
- **PaqueteRed:** es el objeto que recibe el método `ProcesarPaquete()` durante una simulación.
- **AccessPointServicio:** utiliza la entidad `AccessPoint` para crear, buscar, actualizar, listar y eliminar puntos de acceso.
- **AccessPointRepositorio:** se encarga de realizar las operaciones de persistencia relacionadas con los Access Point.

## 9. Resumen: ¿Para qué sirve AccessPoint?

`AccessPoint` es la clase que representa un punto de acceso inalámbrico dentro del proyecto Datos En Tránsito.

Hereda de `DispositivoRed`, por lo que reutiliza los atributos generales de un dispositivo y agrega características propias de una red Wi-Fi, como el SSID, la seguridad, el canal y la cantidad máxima de dispositivos conectados.

También contiene validaciones para evitar datos incorrectos y sobrescribe el método `ProcesarPaquete()` para determinar si puede procesar paquetes según su estado de encendido.

En conclusión, esta clase representa y define el comportamiento de los puntos de acceso que participan en las simulaciones de red.



# AuditoriaServicio – Capa de Aplicación

## 1. ¿Qué es AuditoriaServicio?

`AuditoriaServicio` es una clase de la capa de aplicación encargada de gestionar las consultas de los registros de auditoría del sistema.

Su función principal es comunicarse con el repositorio de auditoría para obtener los registros almacenados en la base de datos.

Esta clase implementa la interfaz `IAuditoriaServicio`, que establece las operaciones que debe ofrecer el servicio.

## 2. Estructura de la clase

### Interfaces utilizadas

- `IAuditoriaServicio`: define el contrato que debe cumplir el servicio.
- `IAuditoriaRepositorios`: permite comunicarse con el repositorio encargado de acceder a los datos de auditoría.

### Entidad utilizada

- `Auditoria`: representa los registros de auditoría que maneja el sistema.

## 3. Atributo de la clase

```csharp
private readonly IAuditoriaRepositorios _auditoriaRepositorio;



# DispositivoServicio – Capa de Aplicación

## 1. ¿Qué es DispositivoServicio?

`DispositivoServicio` es una clase de la capa de aplicación que se encarga de administrar los dispositivos de red que forman parte de una simulación.

Permite realizar tres operaciones principales:

- Registrar un dispositivo en una simulación.
- Buscar un dispositivo por su ID.
- Eliminar un dispositivo de una simulación.

A diferencia de otros servicios del proyecto, esta clase no utiliza un repositorio porque trabaja directamente con los objetos que recibe como parámetros.

## 2. ¿Qué interfaces y clases utiliza?

### IDispositivoServicio

Es la interfaz que implementa `DispositivoServicio`.

Define el contrato que debe cumplir la clase, es decir, los métodos que debe ofrecer.

### Simulacion

Representa una simulación de red.

Esta clase contiene una colección llamada `Dispositivos`, donde se almacenan los dispositivos que participan en la simulación.

### DispositivoRed

Es la clase base de los dispositivos de red.

Representa un dispositivo que puede participar en una simulación, como un Router, Switch, Firewall o AccessPoint.

## 3. Código de la clase

```csharp
using Aplicacion.Interfaces;
using Persistencia.Entidades;

namespace Aplicacion.Servicios
{
    public class DispositivoServicio : IDispositivoServicio
    {
        public void RegistrarDispositivo(Simulacion simulacion, DispositivoRed dispositivo)
        {
            if (simulacion == null)
            {
                throw new ArgumentNullException(nameof(simulacion));
            }

            if (dispositivo == null)
            {
                throw new ArgumentNullException(nameof(dispositivo));
            }

            simulacion.AgregarDispositivo(dispositivo);
        }

        public DispositivoRed ObtenerDispositivoPorId(Simulacion simulacion, int id)
        {
            if (simulacion == null)
            {
                throw new ArgumentNullException(nameof(simulacion));
            }

            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "El Id debe ser mayor que 0.");
            }

            foreach (var dispositivo in simulacion.Dispositivos)
            {
                if (dispositivo.Id == id)
                {
                    return dispositivo;
                }
            }

            throw new Exception("No se encontró el dispositivo");
        }

        public void EliminarDispositivo(Simulacion simulacion, DispositivoRed dispositivo)
        {
            if (simulacion == null)
            {
                throw new ArgumentNullException(nameof(simulacion));
            }

            if (dispositivo == null)
            {
                throw new ArgumentNullException(nameof(dispositivo));
            }

            simulacion.Dispositivos.Remove(dispositivo);
        }
    }
}
```

## 4. Explicación de sus métodos

### RegistrarDispositivo()

Recibe una simulación y un dispositivo de red.

Primero comprueba que ninguno de los dos parámetros sea `null`.

Si alguno es nulo, lanza una excepción `ArgumentNullException`.

Si ambos son válidos, llama al método `AgregarDispositivo()` de la clase `Simulacion`.

De esta manera, el dispositivo queda incorporado a la colección de dispositivos de esa simulación.

### ObtenerDispositivoPorId()

Permite buscar un dispositivo dentro de una simulación utilizando su ID.

Primero realiza dos validaciones:

- Comprueba que la simulación no sea nula.
- Comprueba que el ID sea mayor que cero.

Después utiliza un `foreach` para recorrer la colección `Dispositivos`.

En cada vuelta compara el ID del dispositivo actual con el ID recibido.

Si encuentra una coincidencia, devuelve ese dispositivo.

Si termina de recorrer la colección y no encuentra ninguno, lanza una excepción indicando que el dispositivo no existe.

### EliminarDispositivo()

Recibe una simulación y un dispositivo.

Primero comprueba que ambos parámetros no sean nulos.

Después utiliza `Remove()` para quitar el dispositivo de la colección `Dispositivos` de esa simulación.

Este método elimina el objeto de la colección en memoria.

## 5. ¿Por qué no utiliza un repositorio?

Esta clase no necesita un repositorio porque sus operaciones trabajan directamente sobre una simulación que ya recibió.

Por ejemplo:

- Para registrar un dispositivo, utiliza `simulacion.AgregarDispositivo()`.
- Para buscarlo, recorre `simulacion.Dispositivos`.
- Para eliminarlo, utiliza `simulacion.Dispositivos.Remove()`.

Por lo tanto, esta clase no realiza consultas ni operaciones directas sobre la base de datos.

Esto la diferencia de servicios como `AccessPointServicio`, que utiliza un repositorio para guardar, consultar, actualizar y eliminar información persistida.

Es importante aclarar que esto no significa que los dispositivos nunca se guarden en la base de datos. Significa que `DispositivoServicio`, por sí mismo, trabaja con los objetos de la simulación en memoria.

## 6. Relación con los principios SOLID

### SRP – Principio de Responsabilidad Única

La clase tiene una responsabilidad concreta: administrar los dispositivos que pertenecen a una simulación.

No se encarga de realizar consultas SQL ni de administrar directamente la conexión con la base de datos.

### DIP – Principio de Inversión de Dependencias

La clase implementa la interfaz `IDispositivoServicio`.

De esta manera, ofrece sus operaciones mediante un contrato definido por una interfaz.

Sin embargo, no utiliza inyección de dependencias en su constructor porque no necesita recibir otras clases para realizar sus operaciones.

## 7. Resumen

`DispositivoServicio` administra los dispositivos que participan en una simulación.

Sus tres métodos principales permiten registrar, buscar y eliminar dispositivos.

No utiliza un repositorio porque trabaja directamente con la colección `Dispositivos` de la clase `Simulacion`.

Su objetivo es mantener separadas las operaciones relacionadas con la administración de dispositivos de otras responsabilidades del sistema.



# EstadisticaServicio – Capa de Aplicación

## 1. ¿Qué es EstadisticaServicio?

`EstadisticaServicio` es una clase de la capa de aplicación que se encarga de consultar y mostrar estadísticas relacionadas con las simulaciones y los dispositivos de red.

Su objetivo es obtener información que permita conocer el comportamiento del sistema, como la cantidad de recorridos, las latencias y el total de simulaciones realizadas.

Para obtener las estadísticas almacenadas en la base de datos, utiliza un repositorio llamado `IEstadisticaRepositorio`.

## 2. ¿Qué interfaces y clases utiliza?

### IEstadisticaServicio

Es la interfaz que implementa `EstadisticaServicio`.

Define el contrato que debe cumplir la clase, es decir, los métodos que debe ofrecer.

### IEstadisticaRepositorio

Es la interfaz del repositorio que utiliza el servicio.

Define las operaciones necesarias para consultar las estadísticas almacenadas en la base de datos.

### Simulacion

Es la entidad que representa una simulación de red.

El servicio utiliza sus propiedades para mostrar información como el ID, la cantidad de dispositivos y la cantidad de paquetes.

## 3. Inyección de dependencias

La clase recibe `IEstadisticaRepositorio` mediante su constructor.

```csharp
private readonly IEstadisticaRepositorio _estadisticaRepositorio;

public EstadisticaServicio(IEstadisticaRepositorio estadisticaRepositorio)
{
    _estadisticaRepositorio = estadisticaRepositorio;
}
```

Esto se conoce como inyección de dependencias.

En lugar de crear directamente un repositorio dentro del servicio, recibe una implementación que cumple con la interfaz `IEstadisticaRepositorio`.

Esto permite separar las responsabilidades y facilita cambiar la implementación del repositorio sin modificar la lógica del servicio.

## 4. Código de la clase

```csharp
using Aplicacion.Interfaces;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;

namespace Aplicacion.Servicios
{
    public class EstadisticaServicio : IEstadisticaServicio
    {
        private readonly IEstadisticaRepositorio _estadisticaRepositorio;

        public EstadisticaServicio(IEstadisticaRepositorio estadisticaRepositorio)
        {
            _estadisticaRepositorio = estadisticaRepositorio;
        }

        public void ConsultarEstadisticas(Simulacion simulacion)
        {
            if (simulacion == null)
            {
                throw new ArgumentNullException(nameof(simulacion));
            }

            Console.WriteLine($"===== Estadísticas de la simulación {simulacion.Id} =====");
            Console.WriteLine($"Dispositivos: {simulacion.Dispositivos.Count}");
            Console.WriteLine($"Paquetes: {simulacion.Paquetes.Count}");
        }

        // Método nuevo: estadísticas por dispositivo (usa el SP)
        public void MostrarEstadisticasDispositivos()
        {
            var resultados = _estadisticaRepositorio.ObtenerEstadisticasDispositivos();

            Console.WriteLine("===== Estadísticas por Dispositivo =====");

            foreach (var item in resultados)
            {
                Console.WriteLine($"Dispositivo ID: {item.dispositivo_id}");
                Console.WriteLine($"Cantidad de recorridos: {item.cantidad_recorridos}");
                Console.WriteLine($"Latencia promedio: {item.latencia_promedio}");
                Console.WriteLine($"Latencia máxima: {item.latencia_maxima}");
                Console.WriteLine("--------------------------------------");
            }
        }

        // Método nuevo: estadísticas generales de simulaciones (usa el SP)
        public void MostrarEstadisticasSimulaciones()
        {
            var resultados = _estadisticaRepositorio.ObtenerEstadisticasSimulaciones();

            Console.WriteLine("===== Estadísticas Generales de Simulaciones =====");

            foreach (var item in resultados)
            {
                Console.WriteLine($"Total simulaciones: {item.total_simulaciones}");
                Console.WriteLine($"Latencia promedio: {item.latencia_promedio}");
                Console.WriteLine($"Latencia máxima: {item.latencia_maxima}");
            }
        }
    }
}
```

## 5. Explicación de sus métodos

### ConsultarEstadisticas()

Recibe una simulación como parámetro y muestra información básica de ella.

Primero comprueba que la simulación no sea `null`.

Si es nula, lanza una excepción `ArgumentNullException` para indicar que se recibió un argumento vacío.

Si la simulación es válida, muestra:

- El ID de la simulación.
- La cantidad de dispositivos que participan.
- La cantidad de paquetes que contiene.

Para contar los elementos utiliza la propiedad `Count` de las colecciones.

Este método trabaja con el objeto `Simulacion` que recibe. No necesita consultar el repositorio.

### MostrarEstadisticasDispositivos()

Obtiene estadísticas relacionadas con los dispositivos de red.

Primero llama al método:

`_estadisticaRepositorio.ObtenerEstadisticasDispositivos()`

Este método consulta los resultados mediante el repositorio.

Los datos se obtienen utilizando un procedimiento almacenado de MySQL (Stored Procedure o SP).

Después utiliza un `foreach` para recorrer los resultados y mostrar:

- ID del dispositivo.
- Cantidad de recorridos.
- Latencia promedio.
- Latencia máxima.

La variable `item` representa cada resultado individual del recorrido.

### MostrarEstadisticasSimulaciones()

Obtiene estadísticas generales de las simulaciones.

Primero llama al método:

`_estadisticaRepositorio.ObtenerEstadisticasSimulaciones()`

El repositorio obtiene los resultados mediante un procedimiento almacenado de MySQL.

Después recorre los resultados con un `foreach` y muestra:

- Total de simulaciones.
- Latencia promedio.
- Latencia máxima.

## 6. ¿Qué es un Stored Procedure (SP)?

Un Stored Procedure o procedimiento almacenado es un conjunto de instrucciones SQL que se guarda en la base de datos para poder ejecutarse cuando sea necesario.

En este proyecto se utilizan para realizar consultas estadísticas.

Por ejemplo, en lugar de escribir toda la consulta SQL dentro del servicio, el servicio solicita al repositorio que obtenga los resultados del procedimiento almacenado.

El flujo es:

1. `EstadisticaServicio` solicita las estadísticas.
2. `IEstadisticaRepositorio` define la operación que debe realizarse.
3. La implementación del repositorio ejecuta el procedimiento almacenado en MySQL.
4. El repositorio devuelve los resultados.
5. El servicio recorre y muestra esos resultados.

## 7. ¿Por qué utiliza un repositorio?

A diferencia de `DispositivoServicio`, esta clase necesita consultar información almacenada en la base de datos.

Por eso utiliza `IEstadisticaRepositorio`.

El servicio no necesita conocer cómo se ejecutan las consultas SQL ni cómo se conecta a MySQL. Esa responsabilidad corresponde al repositorio.

Esto permite mantener separadas las responsabilidades:

- Servicio: coordina la consulta y presenta los resultados.
- Repositorio: obtiene los datos de la base de datos.

## 8. Relación con los principios SOLID

### SRP – Principio de Responsabilidad Única

La clase tiene como responsabilidad trabajar con las estadísticas del sistema.

No se encarga de establecer conexiones con MySQL ni de ejecutar directamente las consultas SQL.

### DIP – Principio de Inversión de Dependencias

La clase depende de la abstracción `IEstadisticaRepositorio` en lugar de depender directamente de una implementación concreta.

Esto permite que el servicio trabaje con cualquier repositorio que cumpla con ese contrato.

## 9. Resumen

`EstadisticaServicio` se encarga de consultar y mostrar estadísticas de las simulaciones y los dispositivos.

Cuenta con tres métodos principales:

- `ConsultarEstadisticas()`: muestra información básica de una simulación.
- `MostrarEstadisticasDispositivos()`: muestra estadísticas de los dispositivos.
- `MostrarEstadisticasSimulaciones()`: muestra estadísticas generales de las simulaciones.

Utiliza inyección de dependencias para recibir `IEstadisticaRepositorio` y obtener información de la base de datos mediante procedimientos almacenados.

De esta manera, mantiene separada la lógica del servicio de las operaciones de persistencia.



# FirewallServicio

## 1. ¿Qué es FirewallServicio?

`FirewallServicio` es una clase perteneciente a la capa **Aplicación**.

Su responsabilidad es administrar los firewalls del sistema. Para realizar estas operaciones utiliza `IFirewallRepositorio`, que permite comunicarse con la capa de Persistencia.

Esta clase contiene las operaciones CRUD:

- Crear firewalls.
- Listar firewalls.
- Buscar firewalls por ID.
- Actualizar firewalls.
- Eliminar firewalls.

## 2. Dependencias

```csharp
using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;
```

- `Aplicacion.DTOs`: permite utilizar los DTO que reciben los datos de las solicitudes.
- `Aplicacion.Interfaces`: contiene las interfaces de los servicios.
- `Persistencia.Entidades`: contiene la entidad Firewall.
- `Persistencia.Repositorios.InterfazRepositorios`: permite utilizar la interfaz del repositorio.

## 3. Atributo y constructor

```csharp
private readonly IFirewallRepositorio repositorio;

public FirewallServicio(IFirewallRepositorio repositorio)
{
    this.repositorio = repositorio;
}
```

- `repositorio`: guarda una referencia al repositorio que permite realizar operaciones sobre los firewalls.
- `readonly`: evita que se cambie la referencia después de inicializarla.
- Constructor: recibe el repositorio mediante inyección de dependencias y lo guarda en el atributo de la clase.

## 4. Métodos

### Crear()

```csharp
public Firewall Crear(FirewallRequestDto solicitud)
{
    Firewall firewall = new Firewall(
        solicitud.Nombre,
        solicitud.DireccionIp,
        solicitud.DireccionMAC,
        solicitud.Encendido,
        solicitud.FiltradoActivo,
        solicitud.PoliticaPredeterminada,
        solicitud.CantidadReglas,
        solicitud.BloqueaTraficoEntrante,
        solicitud.BloqueaTraficoSaliente,
        solicitud.Tipo,
        solicitud.Latencia
    );

    repositorio.Agregar(firewall);

    return firewall;
}
```

Recibe un `FirewallRequestDto` con los datos necesarios para crear un firewall.

Primero construye un objeto `Firewall` utilizando los datos recibidos. Después, utiliza `repositorio.Agregar()` para guardarlo y finalmente devuelve el firewall creado.

### Listar()

```csharp
public List<Firewall> Listar()
{
    return repositorio.Listar();
}
```

Solicita al repositorio todos los firewalls registrados y devuelve una lista de objetos `Firewall`.

### BuscarPorId()

```csharp
public Firewall? BuscarPorId(int id)
{
    return repositorio.ObtenerPorId(id);
}
```

Busca un firewall utilizando su identificador.

El signo `?` indica que el método puede devolver un objeto `Firewall` o `null` si no encuentra uno.

### Eliminar()

```csharp
public void Eliminar(int id)
{
    repositorio.Eliminar(id);
}
```

Solicita al repositorio que elimine el firewall correspondiente al ID recibido.

No devuelve ningún valor porque utiliza `void`.

### Actualizar()

```csharp
public void Actualizar(int id, ActualizarFirewallRequestDto solicitud)
{
    Firewall? firewall = repositorio.ObtenerPorId(id);

    if (firewall is null)
    {
        return;
    }

    firewall.Nombre = solicitud.Nombre;
    firewall.DireccionIp = solicitud.DireccionIp;
    firewall.DireccionMAC = solicitud.DireccionMAC;
    firewall.Encendido = solicitud.Encendido;
    firewall.FiltradoActivo = solicitud.FiltradoActivo;
    firewall.PoliticaPredeterminada = solicitud.PoliticaPredeterminada;
    firewall.CantidadReglas = solicitud.CantidadReglas;
    firewall.BloqueaTraficoEntrante = solicitud.BloqueaTraficoEntrante;
    firewall.BloqueaTraficoSaliente = solicitud.BloqueaTraficoSaliente;
    firewall.Tipo = solicitud.Tipo;
    firewall.Latencia = solicitud.Latencia;

    repositorio.Actualizar(firewall);
}
```

Actualiza los datos de un firewall existente.

1. Busca el firewall mediante su ID.
2. Comprueba si existe.
3. Si no existe, finaliza el método con `return`.
4. Si existe, modifica sus propiedades utilizando los datos del DTO.
5. Finalmente, utiliza `repositorio.Actualizar()` para guardar los cambios.

## 5. Relación con SOLID

- **SRP (Responsabilidad Única):** la clase se encarga de administrar las operaciones de los firewalls.
- **DIP (Inversión de Dependencias):** depende de la abstracción `IFirewallRepositorio` y no directamente de una implementación concreta.

## 6. Resumen

`FirewallServicio` funciona como intermediario entre la API y el repositorio.

Recibe los datos, organiza las operaciones necesarias y solicita al repositorio que consulte, cree, actualice o elimine los firewalls.

De esta manera, la lógica de aplicación queda separada del acceso a la base de datos.



# PaqueteServicio

## 1. ¿Qué es PaqueteServicio?

`PaqueteServicio` es una clase de la capa **Aplicación**.

Su responsabilidad es administrar los paquetes de red que se utilizan dentro de las simulaciones.

Para realizar estas operaciones utiliza dos repositorios:

- `IPaqueteRedRepositorio`: permite guardar, consultar y actualizar paquetes.
- `ISimulacionRepositorio`: permite consultar las simulaciones existentes.

## 2. Dependencias

```csharp
using Aplicacion.Interfaces;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;
```

- `Aplicacion.Interfaces`: permite implementar la interfaz del servicio.
- `Persistencia.Entidades`: permite utilizar las entidades `Simulacion` y `PaqueteRed`.
- `Persistencia.Repositorios.InterfazRepositorios`: permite utilizar las interfaces de los repositorios.

## 3. Atributos y constructor

```csharp
private readonly IPaqueteRedRepositorio _paqueteRepositorio;
private readonly ISimulacionRepositorio _simulacionRepositorio;
```

La clase tiene dos dependencias:

- `_paqueteRepositorio`: se encarga de las operaciones relacionadas con los paquetes.
- `_simulacionRepositorio`: permite consultar las simulaciones para comprobar que existen y que no estén finalizadas.

Ambos atributos son `readonly`, por lo que sus referencias no pueden cambiar después de inicializarse.

### Constructor

```csharp
public PaqueteServicio(
    IPaqueteRedRepositorio paqueteRepositorio,
    ISimulacionRepositorio simulacionRepositorio)
{
    _paqueteRepositorio = paqueteRepositorio
        ?? throw new ArgumentNullException(nameof(paqueteRepositorio));

    _simulacionRepositorio = simulacionRepositorio
        ?? throw new ArgumentNullException(nameof(simulacionRepositorio));
}
```

Recibe los dos repositorios mediante inyección de dependencias.

El operador `??` comprueba si el repositorio recibido es `null`. Si lo es, lanza una excepción para evitar que el servicio funcione sin sus dependencias.

## 4. Métodos

### CrearPaquete()

```csharp
public void CrearPaquete(Simulacion simulacion, PaqueteRed paquete)
{
    if (simulacion == null)
    {
        throw new ArgumentNullException(nameof(simulacion), "La simulación no puede ser nula.");
    }

    if (paquete == null)
    {
        throw new ArgumentNullException(nameof(paquete), "El paquete no puede ser nulo.");
    }

    if (simulacion.Id <= 0)
    {
        throw new ArgumentException("La simulación debe tener un ID válido.", nameof(simulacion));
    }

    Simulacion? simulacionExistente = _simulacionRepositorio.BuscarPorId(simulacion.Id);

    if (simulacionExistente == null)
    {
        throw new InvalidOperationException("No existe esa simulacion");
    }

    if (simulacionExistente.Finalizado)
    {
        throw new InvalidOperationException("No se puede agregar un paquete a una simulación finalizada.");
    }

    int paqueteId = _paqueteRepositorio.Agregar(paquete, simulacion.Id);

    paquete.Id = paqueteId;
    simulacion.AgregarPaquete(paquete);
}
```

Este método crea un paquete y lo relaciona con una simulación.

Su funcionamiento es:

1. Comprueba que la simulación y el paquete no sean nulos.
2. Comprueba que la simulación tenga un ID válido.
3. Busca la simulación en la base de datos.
4. Comprueba que la simulación exista.
5. Comprueba que no esté finalizada.
6. Guarda el paquete mediante el repositorio y obtiene el ID generado.
7. Asigna ese ID al objeto `PaqueteRed`.
8. Agrega el paquete a la colección de paquetes de la simulación.

**Importante:** el paquete se guarda en la base de datos y también se agrega a la colección de la simulación en memoria.

### ActualizarResultado()

```csharp
public void ActualizarResultado(PaqueteRed paquete)
{
    if (paquete == null)
    {
        throw new ArgumentNullException(nameof(paquete), "El paquete no puede ser nulo.");
    }

    if (paquete.Id <= 0)
    {
        throw new ArgumentException("El paquete debe tener un ID válido.", nameof(paquete));
    }

    PaqueteRed? paqueteExistente = _paqueteRepositorio.ObtenerPorId(paquete.Id);

    if (paqueteExistente == null)
    {
        throw new InvalidOperationException("No existe ese paquete");
    }

    paquete.TtlFinal = paquete.Ttl;

    _paqueteRepositorio.ActualizarResultado(paquete);
}
```

Actualiza el resultado de un paquete después de su procesamiento.

Su funcionamiento es:

1. Comprueba que el paquete no sea nulo.
2. Comprueba que tenga un ID válido.
3. Busca el paquete en la base de datos.
4. Comprueba que exista.
5. Asigna el valor actual de `Ttl` a `TtlFinal`.
6. Solicita al repositorio que actualice el resultado.

**¿Qué significa TTL?**

TTL significa *Time To Live*. En este proyecto representa la cantidad de saltos que le quedan al paquete para recorrer la red.

`TtlFinal` guarda el valor que tiene el TTL cuando termina el procesamiento del paquete.

### ObtenerPorSimulacionId()

```csharp
public PaqueteRed? ObtenerPorSimulacionId(int simulacionId)
{
    if (simulacionId <= 0)
        throw new ArgumentOutOfRangeException(nameof(simulacionId));

    return _paqueteRepositorio.ObtenerPorSimulacionId(simulacionId);
}
```

Obtiene el paquete asociado a una simulación utilizando su ID.

Primero comprueba que el ID sea válido y después solicita al repositorio que realice la búsqueda.

El signo `?` indica que puede devolver un `PaqueteRed` o `null` si no encuentra uno.

### ActualizarDatos()

```csharp
public void ActualizarDatos(PaqueteRed paquete)
{
    if (paquete == null)
        throw new ArgumentNullException(nameof(paquete));

    if (paquete.Id <= 0)
        throw new ArgumentException("El paquete debe tener un ID válido.");

    _paqueteRepositorio.ActualizarDatos(paquete);
}
```

Actualiza los datos generales de un paquete.

Primero comprueba que el objeto no sea nulo y que tenga un ID válido. Después solicita al repositorio que guarde los cambios.

## 5. Relación con SOLID

- **SRP (Responsabilidad Única):** el servicio se encarga de administrar las operaciones relacionadas con los paquetes de red.
- **DIP (Inversión de Dependencias):** utiliza interfaces de repositorios en lugar de depender directamente de sus implementaciones.

## 6. Resumen

`PaqueteServicio` administra los paquetes de red utilizados en las simulaciones.

Se encarga de validar los datos, comprobar que las simulaciones existan y no estén finalizadas, guardar paquetes y actualizar sus resultados.

También mantiene relacionada la información del paquete con la simulación correspondiente.



# RouterServicio

## 1. ¿Qué es RouterServicio?

`RouterServicio` es una clase perteneciente a la capa **Aplicación**.

Su responsabilidad es administrar los routers del sistema mediante las operaciones CRUD:

- Crear routers.
- Obtener un router por su ID.
- Listar todos los routers.
- Actualizar routers.
- Eliminar routers.

Para realizar estas operaciones utiliza `IRouterRepositorio`.

## 2. Dependencias

```csharp
using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Persistencia.Entidades;
using Persistencia.Repositorios.InterfazRepositorios;
```

- `Aplicacion.DTOs`: permite utilizar los DTO que reciben los datos de las solicitudes.
- `Aplicacion.Interfaces`: contiene las interfaces de los servicios.
- `Persistencia.Entidades`: permite utilizar la entidad `Router`.
- `Persistencia.Repositorios.InterfazRepositorios`: permite utilizar la interfaz del repositorio.

## 3. Atributo y constructor

```csharp
private readonly IRouterRepositorio repositorio;

public RouterServicio(IRouterRepositorio repositorio)
{
    this.repositorio = repositorio;
}
```

El atributo `repositorio` guarda una referencia al repositorio encargado de realizar las operaciones sobre los routers.

El constructor recibe `IRouterRepositorio` mediante inyección de dependencias.

`readonly` evita que se cambie la referencia después de inicializarla.

## 4. Métodos

### Crear()

```csharp
public Router Crear(RouterRequestDto solicitud)
{
    Router router = new Router(
        solicitud.Nombre,
        solicitud.DireccionIp,
        solicitud.DireccionMAC,
        solicitud.Encendido,
        solicitud.PaquetesBloqueados,
        solicitud.PaquetesPermitidos,
        solicitud.Latencia
    );

    repositorio.Agregar(router);

    return router;
}
```

Recibe un `RouterRequestDto` con los datos necesarios para crear un router.

Su funcionamiento es:

1. Recibe los datos del DTO.
2. Construye un objeto `Router` utilizando esos datos.
3. Utiliza `repositorio.Agregar()` para guardarlo.
4. Devuelve el router creado.

### ObtenerPorId()

```csharp
public Router? ObtenerPorId(int id)
{
    return repositorio.ObtenerPorId(id);
}
```

Busca un router utilizando su identificador.

El signo `?` indica que puede devolver un objeto `Router` o `null` si no encuentra uno.

### Listar()

```csharp
public List<Router> Listar()
{
    return repositorio.Listar();
}
```

Solicita al repositorio todos los routers registrados y devuelve una lista de objetos `Router`.

### Eliminar()

```csharp
public void Eliminar(int id)
{
    repositorio.Eliminar(id);
}
```

Solicita al repositorio que elimine el router correspondiente al ID recibido.

Utiliza `void` porque no devuelve ningún valor.

### Actualizar()

```csharp
public void Actualizar(int id, ActualizarRouterRequestDto solicitud)
{
    Router? router = repositorio.ObtenerPorId(id);

    if (router is null)
    {
        return;
    }

    router.Nombre = solicitud.Nombre;
    router.DireccionIp = solicitud.DireccionIp;
    router.DireccionMAC = solicitud.DireccionMAC;
    router.Encendido = solicitud.Encendido;
    router.PaquetesBloqueados = solicitud.PaquetesBloqueados;
    router.PaquetesPermitidos = solicitud.PaquetesPermitidos;
    router.Latencia = solicitud.Latencia;

    repositorio.Actualizar(router);
}
```

Actualiza los datos de un router existente.

Su funcionamiento es:

1. Busca el router mediante su ID.
2. Comprueba si existe.
3. Si no existe, finaliza el método con `return`.
4. Si existe, modifica sus propiedades utilizando los datos del DTO.
5. Finalmente, solicita al repositorio que guarde los cambios.

## 5. Relación con SOLID

- **SRP (Responsabilidad Única):** la clase se encarga de administrar las operaciones relacionadas con los routers.
- **DIP (Inversión de Dependencias):** depende de la interfaz `IRouterRepositorio` y no directamente de una implementación concreta.

## 6. Resumen

`RouterServicio` funciona como intermediario entre la API y el repositorio.

Recibe los datos, organiza las operaciones necesarias y solicita al repositorio que consulte, cree, actualice o elimine routers.

De esta manera, la lógica de aplicación queda separada del acceso a la base de datos.



# SimulacionServicio

## ¿Qué es SimulacionServicio?

`SimulacionServicio` es una clase de la capa Aplicacion que se encarga de coordinar las operaciones relacionadas con las simulaciones de red.

Su responsabilidad principal es organizar el proceso de una simulación: obtener los dispositivos, crear el paquete, procesarlo a través del recorrido, guardar los resultados y finalizar la simulación.

Es uno de los servicios principales del proyecto porque conecta distintas partes del sistema.

## Dependencias

La clase utiliza seis dependencias:

- `ISimulacionRepositorio`: permite guardar, buscar, listar y eliminar simulaciones, además de administrar los pasos del recorrido.
- `IRouterRepositorio`: permite buscar routers.
- `ISwitchRepositorio`: permite buscar switches.
- `IFirewallRepositorio`: permite buscar firewalls.
- `IAccessPointRepositorio`: permite buscar puntos de acceso.
- `IPaqueteServicio`: permite crear, consultar y actualizar paquetes.

Estas dependencias se reciben mediante el constructor principal. Esto permite que el servicio utilice otras clases sin tener que crearlas directamente.

## Constructores

### Constructor principal

Recibe los repositorios y servicios que necesita la clase para realizar las operaciones completas de una simulación.

Las dependencias se reciben mediante inyección de dependencias y se guardan en atributos privados.

### Constructor vacío

No recibe dependencias y establece todos los atributos en `null`.

Se conserva para permitir operaciones que trabajan únicamente en memoria y no necesitan acceder a la base de datos.

Es importante tener en cuenta que las operaciones que necesitan repositorios no pueden funcionar correctamente utilizando este constructor.

## Métodos que trabajan en memoria

Estos métodos trabajan directamente con los objetos de la simulación. No realizan consultas ni modificaciones en la base de datos.

### AgregarDispositivo()

Recibe una simulación y un dispositivo de red.

Primero comprueba que ninguno de los dos valores sea `null`. Después llama al método `AgregarDispositivo()` de la entidad `Simulacion`.

Su objetivo es incorporar un dispositivo a la colección de dispositivos de una simulación.

### AgregarPaquete()

Recibe una simulación y un paquete de red.

Comprueba que ambos objetos existan y después llama al método `AgregarPaquete()` de la entidad `Simulacion`.

Su objetivo es incorporar el paquete a la colección de paquetes de la simulación.

### ProcesarPaquete()

Recibe una simulación y un paquete.

Primero valida que ambos objetos no sean `null`. Después delega el procesamiento al método `ProcesarPaquete()` de la entidad `Simulacion`.

Devuelve un valor booleano (`true` o `false`) que representa el resultado de ese procesamiento.

La lógica concreta del procesamiento se encuentra en la entidad `Simulacion`, no en este método del servicio.

## Diferencia entre estos métodos

| Método | Función |
|---|---|
| `AgregarDispositivo()` | Incorpora un dispositivo a la simulación. |
| `AgregarPaquete()` | Incorpora un paquete a la simulación. |
| `ProcesarPaquete()` | Delega el procesamiento del paquete a la entidad `Simulacion`. |

## Relación con la base de datos

Estos tres métodos no guardan información en la base de datos. Trabajan con los objetos que están en memoria.

La persistencia se realiza mediante otros métodos de `SimulacionServicio` y mediante las dependencias que recibe, como `ISimulacionRepositorio` e `IPaqueteServicio`.

## Principios SOLID

### SRP - Responsabilidad única

El servicio coordina las operaciones relacionadas con las simulaciones. En estos tres métodos, valida los datos recibidos y delega las acciones correspondientes a la entidad `Simulacion`.

### DIP - Inversión de dependencias

El constructor principal recibe interfaces como `ISimulacionRepositorio` e `IRouterRepositorio` en lugar de depender directamente de las clases concretas.

Esto permite trabajar con abstracciones y facilita el mantenimiento y las pruebas del sistema.

## Resumen

`SimulacionServicio` coordina las operaciones del sistema relacionadas con las simulaciones.

En esta primera parte vimos sus constructores y tres métodos básicos que trabajan en memoria:

- Agregar dispositivos.
- Agregar paquetes.
- Procesar paquetes.

Estos métodos validan los objetos recibidos y delegan las operaciones a la entidad `Simulacion`. No guardan información directamente en la base de datos.



