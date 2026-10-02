CREATE DATABASE IF NOT EXISTS datos_transito;

USE datos_transito;

-- =========================================
-- AUDITORIAS
-- =========================================

CREATE TABLE auditorias
(
    id INT AUTO_INCREMENT PRIMARY KEY,
    accion VARCHAR(100) NOT NULL,
    descripcion VARCHAR(500) NOT NULL,
    fecha DATETIME NOT NULL
);

-- =========================================
-- PAQUETES
-- =========================================

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
    procesado BOOLEAN NOT NULL,
    latencia_acumulada INT NOT NULL,
    ttl_inicial INT NOT NULL,
);


-- =========================================
-- DISPOSITIVOS
-- =========================================

CREATE TABLE dispositivos
(
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    direccion_ip VARCHAR(45) NOT NULL,
    direccion_mac VARCHAR(50) NOT NULL,
    encendido BOOLEAN NOT NULL,
    latencia INT NOT NULL
);

-- =========================================
-- SIMULACIONES
-- =========================================

CREATE TABLE simulaciones
(
    id INT AUTO_INCREMENT PRIMARY KEY
);

-- =========================================
-- ACCESS POINT
-- =========================================

CREATE TABLE access_points
(
    id INT AUTO_INCREMENT PRIMARY KEY,
    dispositivo_id INT NOT NULL,
    ssid VARCHAR(100) NOT NULL,
    seguridad VARCHAR(20) NOT NULL,
    canal INT NOT NULL,
    maximo_dispositivos INT NOT NULL,

    FOREIGN KEY (dispositivo_id)
        REFERENCES dispositivos(id)
);


-- =========================================
-- FIREWALL
-- =========================================

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

    FOREIGN KEY (dispositivo_id)
        REFERENCES dispositivos(id)
);


-- =========================================
-- ROUTER
-- =========================================

CREATE TABLE routers
(
    id INT AUTO_INCREMENT PRIMARY KEY,
    dispositivo_id INT NOT NULL,
    paquetes_bloqueados INT NOT NULL,
    paquetes_permitidos INT NOT NULL,

    FOREIGN KEY (dispositivo_id)
        REFERENCES dispositivos(id)
);


-- =========================================
-- SWITCH
-- =========================================

CREATE TABLE switches
(
    id INT AUTO_INCREMENT PRIMARY KEY,
    dispositivo_id INT NOT NULL,
    cantidad_puertos INT NOT NULL,
    puertos_ocupados INT NOT NULL,
    vlan_activa BOOLEAN NOT NULL,
    cantidad_vlan INT NOT NULL,

    FOREIGN KEY (dispositivo_id)
        REFERENCES dispositivos(id)
);

-- =========================================
-- RELACION SIMULACION - PAQUETE
-- =========================================

CREATE TABLE simulacion_paquetes
(
    id INT AUTO_INCREMENT PRIMARY KEY,
    simulacion_id INT NOT NULL,
    paquete_id INT NOT NULL,

    FOREIGN KEY (simulacion_id)
        REFERENCES simulaciones(id),

    FOREIGN KEY (paquete_id)
        REFERENCES paquetes(id)
);


-- =========================================
-- RECORRIDOS
-- =========================================

CREATE TABLE recorridos
(
    id INT AUTO_INCREMENT PRIMARY KEY,
    simulacion_id INT NOT NULL,
    dispositivo_id INT NOT NULL,
    latencia INT NOT NULL,
    ttl_final INT NOT NULL,

    FOREIGN KEY (simulacion_id)
        REFERENCES simulaciones(id),

    FOREIGN KEY (dispositivo_id)
        REFERENCES dispositivos(id)
);


