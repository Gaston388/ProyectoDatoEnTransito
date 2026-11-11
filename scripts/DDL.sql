CREATE DATABASE IF NOT EXISTS datos_transito;

USE datos_transito;


CREATE TABLE auditorias
(
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    accion VARCHAR(100) NOT NULL,
    descripcion VARCHAR(500) NOT NULL,
    fecha DATETIME NOT NULL
);



CREATE TABLE paquetes
(
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,

    ip_origen VARCHAR(45) NOT NULL,
    ip_destino VARCHAR(45) NOT NULL,

    mac_origen VARCHAR(50) NOT NULL,
    mac_destino VARCHAR(50) NOT NULL,

    tamano INT UNSIGNED NOT NULL,
    protocolo VARCHAR(10) NOT NULL,
    datos VARCHAR(500) NOT NULL,

    hora_creacion DATETIME NOT NULL,

    procesado BOOLEAN NOT NULL DEFAULT FALSE,

    latencia_acumulada INT UNSIGNED NOT NULL DEFAULT 0,

    ttl_inicial INT UNSIGNED NOT NULL,

    CHECK (protocolo IN ('TCP', 'UDP', 'HTTP'))
);



CREATE TABLE dispositivos
(
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,

    nombre VARCHAR(100) NOT NULL,
    direccion_ip VARCHAR(45) NOT NULL,
    direccion_mac VARCHAR(50) NOT NULL,

    encendido BOOLEAN NOT NULL,

    latencia INT UNSIGNED NOT NULL DEFAULT 0
);



CREATE TABLE simulaciones
(
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    fecha_inicio DATETIME NOT NULL,
    fecha_fin DATETIME NULL,
    finalizado BOOLEAN NOT NULL DEFAULT FALSE

);



CREATE TABLE access_points
(
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    dispositivo_id INT UNSIGNED NOT NULL,
    ssid VARCHAR(100) NOT NULL,
    seguridad VARCHAR(20) NOT NULL,
    canal INT UNSIGNED NOT NULL,
    maximo_dispositivos INT UNSIGNED NOT NULL,

    CONSTRAINT uq_access_point_dispositivo
        UNIQUE (dispositivo_id),

    CONSTRAINT fk_access_point_dispositivo
        FOREIGN KEY (dispositivo_id)
        REFERENCES dispositivos(id),

    CHECK (seguridad IN ('WPA2', 'WPA3')),
    CHECK (canal BETWEEN 1 AND 14)
);


CREATE TABLE firewalls
(
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    dispositivo_id INT UNSIGNED NOT NULL,
    filtrado_activo BOOLEAN NOT NULL,
    politica_predeterminada VARCHAR(20) NOT NULL,
    cantidad_reglas INT UNSIGNED NOT NULL,
    bloquea_trafico_entrante BOOLEAN NOT NULL,
    bloquea_trafico_saliente BOOLEAN NOT NULL,
    tipo VARCHAR(50) NOT NULL,

    CONSTRAINT uq_firewall_dispositivo
        UNIQUE (dispositivo_id),

    CONSTRAINT fk_firewall_dispositivo
        FOREIGN KEY (dispositivo_id)
        REFERENCES dispositivos(id),

    CHECK (politica_predeterminada IN ('permitir', 'bloquear'))
);


CREATE TABLE routers
(
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    dispositivo_id INT UNSIGNED NOT NULL,
    paquetes_bloqueados INT UNSIGNED NOT NULL DEFAULT 0,
    paquetes_permitidos INT UNSIGNED NOT NULL DEFAULT 0,

    CONSTRAINT uq_router_dispositivo
        UNIQUE (dispositivo_id),

    CONSTRAINT fk_router_dispositivo
        FOREIGN KEY (dispositivo_id)
        REFERENCES dispositivos(id)
);


CREATE TABLE switches
(
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    dispositivo_id INT UNSIGNED NOT NULL,
    cantidad_puertos INT UNSIGNED NOT NULL,
    puertos_ocupados INT UNSIGNED NOT NULL,
    vlan_activa BOOLEAN NOT NULL,
    cantidad_vlan INT UNSIGNED NOT NULL,

    CONSTRAINT uq_switch_dispositivo
        UNIQUE (dispositivo_id),

    CONSTRAINT fk_switch_dispositivo
        FOREIGN KEY (dispositivo_id)
        REFERENCES dispositivos(id),

    CHECK (puertos_ocupados <= cantidad_puertos),
    CHECK (cantidad_vlan >= 0),

    CHECK (
        vlan_activa = TRUE
        OR cantidad_vlan = 0
    )
);


CREATE TABLE simulacion_paquetes
(
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    simulacion_id INT UNSIGNED NOT NULL,
    paquete_id INT UNSIGNED NOT NULL,

    CONSTRAINT fk_simulacion_paquete_simulacion
        FOREIGN KEY (simulacion_id)
        REFERENCES simulaciones(id),

    CONSTRAINT fk_simulacion_paquete_paquete
        FOREIGN KEY (paquete_id)
        REFERENCES paquetes(id),

    CONSTRAINT uq_simulacion_paquete
        UNIQUE (simulacion_id, paquete_id)
);



CREATE TABLE recorridos
(
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    simulacion_id INT UNSIGNED NOT NULL,
    dispositivo_id INT UNSIGNED NOT NULL,
    latencia INT UNSIGNED NOT NULL,
    ttl_final INT UNSIGNED NOT NULL,

    CONSTRAINT fk_recorrido_simulacion
        FOREIGN KEY (simulacion_id)
        REFERENCES simulaciones(id),

    CONSTRAINT fk_recorrido_dispositivo
        FOREIGN KEY (dispositivo_id)
        REFERENCES dispositivos(id)
);