# Lógica de la Base de Datos - Datos En Tránsito

## 1. Introducción

La base de datos de Datos En Tránsito está diseñada para almacenar los dispositivos de red, las simulaciones, los paquetes y los recorridos que realizan los paquetes dentro de la red.

Su objetivo principal es permitir que el sistema pueda crear simulaciones, procesar paquetes, registrar los dispositivos por los que pasan y guardar los resultados obtenidos.

La base de datos se puede dividir en cuatro partes:

- **Dispositivos de red:** almacenan los equipos que pueden participar en una simulación.
- **Simulaciones y paquetes:** guardan las simulaciones realizadas y los paquetes que se procesan.
- **Recorridos:** registran por qué dispositivos pasó un paquete y qué ocurrió en cada paso.
- **Auditorías:** almacenan registros de acciones realizadas en el sistema.

---

# 2. Tablas de dispositivos de red

Primero tenemos una tabla principal llamada `dispositivos` y cuatro tablas que guardan información específica de cada tipo de dispositivo.

## Tabla dispositivos

Es la tabla que almacena los datos comunes de todos los dispositivos de red.

| Columna | ¿Para qué sirve? |
|---|---|
| `id` | Identificador único del dispositivo. |
| `nombre` | Nombre del dispositivo. |
| `direccion_ip` | Dirección IP del dispositivo. |
| `direccion_mac` | Dirección MAC del dispositivo. |
| `encendido` | Indica si está encendido o apagado. |
| `latencia` | Tiempo de demora que agrega al procesar un paquete. |

### ¿Por qué existe esta tabla?

Porque todos los dispositivos comparten algunas características.

Por ejemplo, un router, un switch y un firewall tienen nombre, dirección IP, dirección MAC, estado de encendido y latencia.

En lugar de repetir esos datos en cuatro tablas, los guardamos en una tabla común.

## Tabla routers

Guarda las características específicas de los routers.

| Columna | ¿Para qué sirve? |
|---|---|
| `id` | Identificador del registro del router. |
| `dispositivo_id` | Relaciona el router con la tabla `dispositivos`. |
| `paquetes_bloqueados` | Cantidad de paquetes bloqueados. |
| `paquetes_permitidos` | Cantidad de paquetes permitidos. |

Por ejemplo, si tenemos un router llamado Router Principal, sus datos generales están en `dispositivos` y sus características específicas están en `routers`.

## Tabla switches

Guarda las características específicas de los switches.

| Columna | ¿Para qué sirve? |
|---|---|
| `id` | Identificador del registro del switch. |
| `dispositivo_id` | Relaciona el switch con `dispositivos`. |
| `cantidad_puertos` | Cantidad total de puertos. |
| `puertos_ocupados` | Cantidad de puertos que están ocupados. |
| `vlan_activa` | Indica si tiene VLAN activada. |
| `cantidad_vlan` | Cantidad de VLAN configuradas. |

Por ejemplo, podemos tener un switch de 8 puertos, de los cuales 4 están ocupados.

## Tabla firewalls

Guarda las características específicas de los firewalls.

| Columna | ¿Para qué sirve? |
|---|---|
| `id` | Identificador del registro del firewall. |
| `dispositivo_id` | Relaciona el firewall con `dispositivos`. |
| `filtrado_activo` | Indica si está activo el filtrado. |
| `politica_predeterminada` | Define si la política predeterminada es permitir o bloquear. |
| `cantidad_reglas` | Cantidad de reglas configuradas. |
| `bloquea_trafico_entrante` | Indica si bloquea el tráfico entrante. |
| `bloquea_trafico_saliente` | Indica si bloquea el tráfico saliente. |
| `tipo` | Tipo de firewall. |

## Tabla access_points

Guarda las características específicas de los puntos de acceso inalámbricos.

| Columna | ¿Para qué sirve? |
|---|---|
| `id` | Identificador del registro del Access Point. |
| `dispositivo_id` | Relaciona el Access Point con `dispositivos`. |
| `ssid` | Nombre de la red inalámbrica. |
| `seguridad` | Tipo de seguridad, por ejemplo WPA2 o WPA3. |
| `canal` | Canal de transmisión inalámbrica. |
| `maximo_dispositivos` | Cantidad máxima de dispositivos que puede conectar. |

## ¿Cómo se relacionan estas cinco tablas?

La relación funciona de esta manera:

- `dispositivos` guarda los datos generales de cada equipo.
- `routers`, `switches`, `firewalls` y `access_points` guardan sus características particulares.
- Cada tabla específica tiene una columna `dispositivo_id` que funciona como clave foránea hacia `dispositivos.id`.

**¿Por qué usamos `dispositivo_id`?**

Porque permite relacionar los datos generales del dispositivo con sus características particulares.

Además, `UNIQUE(dispositivo_id)` impide que el mismo dispositivo aparezca dos veces en una misma tabla especializada.

---

# 3. Tablas de simulaciones y paquetes

Ahora tenemos las tablas que permiten guardar las simulaciones y los paquetes de red.

## Tabla simulaciones

Guarda las simulaciones que se realizan en el sistema.

| Columna | ¿Para qué sirve? |
|---|---|
| `id` | Identificador único de la simulación. |
| `fecha_inicio` | Fecha y hora en que comenzó. |
| `fecha_fin` | Fecha y hora en que terminó. |
| `finalizado` | Indica si la simulación terminó. |
| `exitoso` | Indica si la simulación terminó correctamente. |

**Un detalle importante:** `finalizado` y `exitoso` no significan lo mismo.

Por ejemplo:

- `finalizado = TRUE`: la simulación terminó.
- `exitoso = FALSE`: terminó, pero el paquete no pudo completar correctamente el recorrido.

Esto permite diferenciar una simulación terminada correctamente de una que terminó con errores.

## Tabla paquetes

Guarda la información de los paquetes que se procesan durante las simulaciones.

| Columna | ¿Para qué sirve? |
|---|---|
| `id` | Identificador único del paquete. |
| `ip_origen` | Dirección IP desde donde sale el paquete. |
| `ip_destino` | Dirección IP a donde se dirige. |
| `mac_origen` | Dirección MAC de origen. |
| `mac_destino` | Dirección MAC de destino. |
| `tamano` | Tamaño del paquete. |
| `protocolo` | Protocolo utilizado: TCP, UDP o HTTP. |
| `datos` | Información que transporta el paquete. |
| `hora_creacion` | Fecha y hora de creación. |
| `procesado` | Indica si el paquete fue procesado. |
| `latencia_acumulada` | Suma de las latencias de los dispositivos recorridos. |
| `ttl_inicial` | TTL con el que comenzó el paquete. |
| `ttl_final` | TTL que quedó al terminar el procesamiento. |

### ¿Qué es TTL?

TTL significa *Time To Live*. En tu proyecto representa la cantidad de saltos disponibles que tiene el paquete para recorrer dispositivos.

Cada vez que pasa por un dispositivo, el TTL disminuye.

Por ejemplo:

- El paquete comienza con TTL 5.
- Pasa por un router y queda en 4.
- Pasa por un switch y queda en 3.
- Continúa recorriendo la red hasta llegar al destino o quedarse sin TTL.

La columna `ttl_inicial` conserva el valor original y `ttl_final` permite guardar el valor restante.

## Tabla simulacion_paquetes

Esta tabla relaciona las simulaciones con los paquetes.

| Columna | ¿Para qué sirve? |
|---|---|
| `id` | Identificador del registro. |
| `simulacion_id` | Identifica la simulación relacionada. |
| `paquete_id` | Identifica el paquete relacionado. |

### ¿Por qué necesitamos esta tabla?

Porque permite relacionar dos tablas diferentes mediante una relación de muchos a muchos (N:M).

- Una simulación puede estar relacionada con uno o varios paquetes.
- Un paquete puede estar relacionado con una o varias simulaciones.

La tabla intermedia registra esas asociaciones.

Por ejemplo:

| simulacion_id | paquete_id |
|---|---|
| 1 | 3 |
| 2 | 3 |
| 3 | 5 |

En este ejemplo, el paquete 3 está relacionado con las simulaciones 1 y 2.

La restricción `UNIQUE(simulacion_id, paquete_id)` evita repetir la misma asociación.

---

# 4. Tabla de recorridos

## Tabla recorridos

Esta tabla es fundamental para el proyecto porque permite guardar por qué dispositivos pasó un paquete durante una simulación.

| Columna | ¿Para qué sirve? |
|---|---|
| `id` | Identificador único del recorrido. |
| `simulacion_id` | Identifica a qué simulación pertenece el paso. |
| `dispositivo_id` | Identifica qué dispositivo participó. |
| `latencia` | Latencia que agregó ese dispositivo. |
| `ttl_final` | TTL que quedó después de ese paso. |
| `orden` | Posición del dispositivo dentro del recorrido. |

### ¿Cómo funciona?

Supongamos que tenemos una simulación donde un paquete pasa por tres dispositivos:

1. Router.
2. Switch.
3. Firewall.

La tabla podría guardar algo parecido a esto:

| simulacion_id | dispositivo_id | latencia | ttl_final | orden |
|---|---|---|---|---|
| 1 | 2 | 10 | 4 | 1 |
| 1 | 4 | 5 | 3 | 2 |
| 1 | 6 | 15 | 2 | 3 |

Esto nos permite reconstruir el recorrido.

- El primer dispositivo fue el número 2.
- El segundo fue el número 4.
- El tercero fue el número 6.

La columna `orden` indica en qué posición participó cada dispositivo.

Además, tenemos esta restricción:

```sql
UNIQUE (simulacion_id, orden)
```

Esto impide que una misma simulación tenga dos pasos con el mismo número de orden.

### ¿Por qué es importante esta tabla?

Porque permite conocer qué dispositivos participaron, cuánto aportó cada uno a la latencia y cómo fue disminuyendo el TTL del paquete.

También sirve para obtener estadísticas de los dispositivos y de las simulaciones.

---

# 5. Tabla de auditorías

## Tabla auditorias

Esta tabla permite guardar registros de acciones realizadas en el sistema.

| Columna | ¿Para qué sirve? |
|---|---|
| `id` | Identificador único de la auditoría. |
| `accion` | Nombre de la acción realizada. |
| `descripcion` | Explicación de lo ocurrido. |
| `fecha` | Fecha y hora en que ocurrió. |

Por ejemplo, podría guardar registros como:

| accion | descripcion |
|---|---|
| CREAR | Se creó un router. |
| ELIMINAR | Se eliminó un firewall. |
| SIMULAR | Se ejecutó una simulación. |

**Importante:** en tu `DDL.sql`, esta tabla no tiene claves foráneas que la relacionen con las demás. Es una tabla independiente.

Su función es almacenar información de auditoría, aunque para saber qué acciones registra realmente habría que revisar dónde se utiliza en el código.

---

# 6. ¿Cómo se relacionan todas las tablas?

Las relaciones principales son:

| Relación | Tipo |
|---|---|
| `dispositivos` → tablas de tipos | Uno a cero o uno por cada tipo. |
| `simulaciones` → `simulacion_paquetes` | Uno a muchos. |
| `paquetes` → `simulacion_paquetes` | Uno a muchos. |
| `simulaciones` → `recorridos` | Uno a muchos. |
| `dispositivos` → `recorridos` | Uno a muchos. |

La estructura general se puede entender de esta manera:

```text
                    DISPOSITIVOS
                         |
           +-------------+-------------+
           |             |             |
        ROUTERS       SWITCHES      FIREWALLS
                         |
                    ACCESS_POINTS


       SIMULACIONES              PAQUETES
             |                       |
             +----------+------------+
                        |
                SIMULACION_PAQUETES


       SIMULACIONES              DISPOSITIVOS
             |                       |
             +----------+------------+
                        |
                    RECORRIDOS


                    AUDITORIAS
                 (Tabla independiente)
```

---

# 7. Ejemplo completo: ¿qué sucede cuando creamos una simulación?

Ahora vamos a unir todo lo que vimos.

Supongamos que queremos simular el recorrido de un paquete por un router, un switch y un firewall.

### Paso 1: Se crea la simulación

Se registra una nueva fila en `simulaciones`.

Se guarda su fecha de inicio y se establece que todavía no está finalizada.

### Paso 2: Se obtienen los dispositivos

El sistema busca los dispositivos que participarán en la simulación.

Sus datos generales se encuentran en `dispositivos` y sus características particulares en sus respectivas tablas.

### Paso 3: Se crea el paquete

Se registra el paquete en `paquetes`.

Se guardan sus direcciones IP, direcciones MAC, tamaño, protocolo, datos y TTL inicial.

### Paso 4: Se relaciona el paquete con la simulación

Se registra la relación en `simulacion_paquetes`.

De esta manera, la base de datos sabe qué paquete pertenece a cada simulación.

### Paso 5: Se procesa el recorrido

El paquete comienza a pasar por los dispositivos seleccionados.

Por cada dispositivo se calcula su procesamiento, se acumula la latencia y disminuye el TTL.

### Paso 6: Se guardan los pasos

Cada paso se registra en `recorridos`.

Se almacena:

- Qué simulación se está ejecutando.
- Qué dispositivo participó.
- Cuánta latencia agregó.
- Cuánto TTL quedó.
- En qué orden participó.

### Paso 7: Se actualiza el resultado

Cuando termina el procesamiento, se actualiza la información del paquete y de la simulación.

Se guarda si el paquete fue procesado, su latencia acumulada, su TTL final y si la simulación terminó exitosamente.

---

# 8. Restricciones de la base de datos

Además de las relaciones, el esquema utiliza restricciones para mantener la integridad de los datos.

### PRIMARY KEY

Identifica de manera única cada registro de una tabla.

Por ejemplo:

```sql
id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY
```

- `UNSIGNED`: permite almacenar valores enteros no negativos.
- `AUTO_INCREMENT`: genera automáticamente el siguiente identificador.
- `PRIMARY KEY`: establece que ese campo identifica de manera única cada registro.

### FOREIGN KEY

Relaciona una tabla con otra y evita referencias a registros inexistentes.

Por ejemplo:

```sql
FOREIGN KEY (dispositivo_id)
REFERENCES dispositivos(id)
```

Esto significa que el dispositivo relacionado debe existir previamente en la tabla `dispositivos`.

### UNIQUE

Impide que se repitan determinados valores.

Por ejemplo:

```sql
UNIQUE (simulacion_id, orden)
```

Evita que una misma simulación tenga dos pasos con el mismo orden.

### NOT NULL

Obliga a que una columna tenga un valor.

Por ejemplo:

```sql
nombre VARCHAR(100) NOT NULL
```

Esto significa que el nombre del dispositivo es obligatorio.

### CHECK

Permite establecer condiciones que deben cumplir los datos.

Por ejemplo:

```sql
CHECK (protocolo IN ('TCP', 'UDP', 'HTTP'))
```

Esto limita los protocolos permitidos a TCP, UDP y HTTP.

---

# 9. Resumen final

La base de datos de Datos En Tránsito está organizada para separar la información según su responsabilidad.

- `dispositivos`: guarda los datos generales de los equipos.
- `routers`: guarda las características particulares de los routers.
- `switches`: guarda las características particulares de los switches.
- `firewalls`: guarda las características particulares de los firewalls.
- `access_points`: guarda las características particulares de los puntos de acceso.
- `simulaciones`: registra las simulaciones realizadas.
- `paquetes`: almacena la información de los paquetes de red.
- `simulacion_paquetes`: relaciona las simulaciones con los paquetes.
- `recorridos`: registra los dispositivos por los que pasa cada paquete y los resultados de cada paso.
- `auditorias`: almacena registros de acciones realizadas en el sistema.

La lógica principal consiste en crear una simulación, asociarle un paquete, procesarlo a través de distintos dispositivos y registrar cada paso del recorrido.

De esta manera, el sistema puede recuperar los resultados de las simulaciones, consultar los recorridos realizados y obtener estadísticas sobre el funcionamiento de los dispositivos.