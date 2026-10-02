USE datos_transito;

DROP TRIGGER IF EXISTS validar_latencia;

DELIMITER //

CREATE TRIGGER validar_latencia
BEFORE INSERT ON dispositivos
FOR EACH ROW
BEGIN
    IF NEW.latencia < 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'La latencia no puede ser negativa';
    END IF;
END//

DELIMITER ;