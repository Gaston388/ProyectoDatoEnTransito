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