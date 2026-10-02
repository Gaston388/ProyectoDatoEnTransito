DROP TRIGGER IF EXISTS validar_ttl;

DELIMITER //

CREATE TRIGGER validar_ttl
BEFORE INSERT ON paquetes
FOR EACH ROW
BEGIN
    IF NEW.ttl_inicial <= 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El TTL debe ser mayor que 0';
    END IF;
END//

DELIMITER ;