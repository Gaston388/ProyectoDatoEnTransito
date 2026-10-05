CREATE DATABASE IF NOT EXISTS datos_transito;

USE datos_transito;


CREATE TABLE auditorias
(
    id INT AUTO_INCREMENT PRIMARY KEY,
    accion VARCHAR(100) NOT NULL,
    descripcion VARCHAR(500) NOT NULL,
    fecha DATETIME NOT NULL
);



CREATE TABLE paquetes
(
    id INT AUTO_INCREMENT PRIMARY KEY,

    ip_origen VARCHAR(45) NOT NULL,
    ip_destino VARCHAR(45) NOT NULL,

    mac_origen VARCHAR(50) NOT NULL,
    mac_destino VARCHAR(50) NOT NULL,

    tamano INT NOT NULL,
    protocolo VARCHAR(10) NOT NULL,
    datos VARCHAR(500) NOT NULL,

    hora_creacion DATETIME NOT NULL,

    procesado BOOLEAN NOT NULL DEFAULT FALSE,

    latencia_acumulada INT NOT NULL DEFAULT 0,

    ttl_inicial INT NOT NULL,

    CHECK (tamano > 0),
    CHECK (latencia_acumulada >= 0),
    CHECK (ttl_inicial > 0),

    CHECK (protocolo IN ('TCP', 'UDP', 'HTTP'))
);



CREATE TABLE dispositivos
(
    id INT AUTO_INCREMENT PRIMARY KEY,

    nombre VARCHAR(100) NOT NULL,
    direccion_ip VARCHAR(45) NOT NULL,
    direccion_mac VARCHAR(50) NOT NULL,

    encendido BOOLEAN NOT NULL,

    latencia INT NOT NULL DEFAULT 0,

    CHECK (latencia >= 0)
);



CREATE TABLE simulaciones
(
    id INT AUTO_INCREMENT PRIMARY KEY
);



CREATE TABLE access_points
(
    id INT AUTO_INCREMENT PRIMARY KEY,

    dispositivo_id INT NOT NULL,

    ssid VARCHAR(100) NOT NULL,
    seguridad VARCHAR(20) NOT NULL,
    canal INT NOT NULL,
    maximo_dispositivos INT NOT NULL,

    CONSTRAINT uq_access_point_dispositivo
        UNIQUE (dispositivo_id),

    CONSTRAINT fk_access_point_dispositivo
        FOREIGN KEY (dispositivo_id)
        REFERENCES dispositivos(id),

    CHECK (seguridad IN ('WPA2', 'WPA3')),
    CHECK (canal BETWEEN 1 AND 14),
    CHECK (maximo_dispositivos > 0)
);


CREATE TABLE firewalls
(
    id INT AUTO_INCREMENT PRIMARY KEY,

    dispositivo_id INT NOT NULL,

    filtrado_activo BOOLEAN NOT NULL,

    politica_predeterminada VARCHAR(20) NOT NULL,

    cantidad_reglas INT NOT NULL,

    bloquea_trafico_entrante BOOLEAN NOT NULL,
    bloquea_trafico_saliente BOOLEAN NOT NULL,

    tipo VARCHAR(50) NOT NULL,

    CONSTRAINT uq_firewall_dispositivo
        UNIQUE (dispositivo_id),

    CONSTRAINT fk_firewall_dispositivo
        FOREIGN KEY (dispositivo_id)
        REFERENCES dispositivos(id),

    CHECK (politica_predeterminada IN ('permitir', 'bloquear')),
    CHECK (cantidad_reglas >= 0)
);


CREATE TABLE routers
(
    id INT AUTO_INCREMENT PRIMARY KEY,

    dispositivo_id INT NOT NULL,

    paquetes_bloqueados INT NOT NULL DEFAULT 0,
    paquetes_permitidos INT NOT NULL DEFAULT 0,

    CONSTRAINT uq_router_dispositivo
        UNIQUE (dispositivo_id),

    CONSTRAINT fk_router_dispositivo
        FOREIGN KEY (dispositivo_id)
        REFERENCES dispositivos(id),

    CHECK (paquetes_bloqueados >= 0),
    CHECK (paquetes_permitidos >= 0)
);


CREATE TABLE switches
(
    id INT AUTO_INCREMENT PRIMARY KEY,

    dispositivo_id INT NOT NULL,

    cantidad_puertos INT NOT NULL,
    puertos_ocupados INT NOT NULL,

    vlan_activa BOOLEAN NOT NULL,
    cantidad_vlan INT NOT NULL,

    CONSTRAINT uq_switch_dispositivo
        UNIQUE (dispositivo_id),

    CONSTRAINT fk_switch_dispositivo
        FOREIGN KEY (dispositivo_id)
        REFERENCES dispositivos(id),

    CHECK (cantidad_puertos > 0),
    CHECK (puertos_ocupados >= 0),
    CHECK (puertos_ocupados <= cantidad_puertos),
    CHECK (cantidad_vlan >= 0),

    CHECK (
        vlan_activa = TRUE
        OR cantidad_vlan = 0
    )
);


CREATE TABLE simulacion_paquetes
(
    id INT AUTO_INCREMENT PRIMARY KEY,

    simulacion_id INT NOT NULL,
    paquete_id INT NOT NULL,

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
    id INT AUTO_INCREMENT PRIMARY KEY,

    simulacion_id INT NOT NULL,
    dispositivo_id INT NOT NULL,

    latencia INT NOT NULL,
    ttl_final INT NOT NULL,

    CONSTRAINT fk_recorrido_simulacion
        FOREIGN KEY (simulacion_id)
        REFERENCES simulaciones(id),

    CONSTRAINT fk_recorrido_dispositivo
        FOREIGN KEY (dispositivo_id)
        REFERENCES dispositivos(id),

    CHECK (latencia >= 0),
    CHECK (ttl_final >= 0)
);