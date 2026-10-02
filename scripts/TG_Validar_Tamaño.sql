DROP TRIGGER IF EXISTS validar_tamano;

DELIMITER //

CREATE TRIGGER validar_tamano
BEFORE INSERT ON paquetes
FOR EACH ROW
BEGIN
    IF NEW.tamano <= 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'El tamaño debe ser mayor que 0';
    END IF;
END//

DELIMITER ;