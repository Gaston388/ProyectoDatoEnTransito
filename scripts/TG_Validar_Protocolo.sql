DROP TRIGGER IF EXISTS validar_protocolo;

DELIMITER //

CREATE TRIGGER validar_protocolo
BEFORE INSERT ON paquetes
FOR EACH ROW
BEGIN
    IF UPPER(NEW.protocolo) NOT IN ('TCP', 'UDP', 'HTTP') THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El protocolo debe ser TCP, UDP o HTTP';
    END IF;
END//

DELIMITER ;