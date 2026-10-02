USE datos_transito;

DROP PROCEDURE IF EXISTS sp_registrar_simulacion;

DELIMITER //

CREATE PROCEDURE sp_registrar_simulacion(
    IN p_paquete_id INT
)
BEGIN
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
    ROLLBACK;
    END;
    
    START TRANSACTION;

    INSERT INTO simulaciones
    VALUES ();

    INSERT INTO simulacion_paquetes
    (
        simulacion_id,
        paquete_id
    )
    VALUES
    (
        LAST_INSERT_ID(),
        p_paquete_id
    );

    COMMIT;

END//

DELIMITER ;