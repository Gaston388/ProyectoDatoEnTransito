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
- Swagger / OpenAPI
- Git y GitHub

## Estructura del proyecto

La solución está organizada en cuatro proyectos:

- **API:** recibe las solicitudes HTTP y expone los endpoints de la aplicación.
- **Aplicacion:** contiene los servicios, las interfaces y la lógica de negocio.
- **Persistencia:** contiene las entidades, los repositorios y la conexión con la base de datos MySQL.
- **Tests:** contiene las pruebas del proyecto.

## Requisitos

Para ejecutar el proyecto se necesita:

- .NET SDK.
- MySQL Server.
- Visual Studio Code o un entorno compatible con C#.

## Configuración de la base de datos

1. Crear la base de datos MySQL.
2. Ejecutar los scripts SQL ubicados en la carpeta `scripts`, respetando el orden de ejecución correspondiente.
3. Configurar la cadena de conexión para que la aplicación pueda acceder a la base de datos.

La cadena de conexión debe configurarse mediante la configuración correspondiente del proyecto, utilizando User Secrets o el mecanismo de configuración definido en la aplicación.

## Ejecución del proyecto

Desde la carpeta raíz de la solución, ejecutar:

```bash
dotnet build
```

Para iniciar la API:

```bash
dotnet run --project API/API.csproj
```

Una vez iniciada, se puede acceder a la documentación de los endpoints mediante Swagger, utilizando la dirección local que indique la consola.

## Funcionalidades principales

- Registrar dispositivos de red.
- Consultar los dispositivos registrados.
- Consultar dispositivos por identificador.
- Actualizar información de dispositivos.
- Eliminar dispositivos.
- Crear simulaciones de recorrido de paquetes.
- Consultar simulaciones.
- Consultar los pasos de un recorrido.
- Actualizar y eliminar simulaciones.

## Simulaciones

Cada simulación trabaja con un paquete de datos y una lista ordenada de dispositivos por los que debe pasar.

Durante el recorrido se tienen en cuenta aspectos como:

- El TTL del paquete.
- La latencia acumulada.
- Los dispositivos que participan.
- El resultado del procesamiento.

Al finalizar, la simulación almacena su resultado y la información del recorrido realizado.

## Arquitectura

El proyecto utiliza una separación por responsabilidades:

1. La **API** recibe las solicitudes y devuelve las respuestas.
2. La capa de **Aplicacion** ejecuta la lógica de negocio mediante servicios.
3. La capa de **Persistencia** se encarga de consultar y modificar la base de datos.
4. La capa de **Tests** permite verificar el funcionamiento del sistema.

Esta organización busca mantener el código ordenado, facilitar su mantenimiento y separar la lógica de negocio del acceso a los datos.

---

**Proyecto académico - Datos En Tránsito**
