USE datos_transito;

DROP PROCEDURE IF EXISTS sp_estadisticas_dispositivos;

DELIMITER //

CREATE PROCEDURE sp_estadisticas_dispositivos()
BEGIN

    SELECT
        dispositivo_id,
        COUNT(*) AS cantidad_recorridos,
        AVG(latencia) AS latencia_promedio,
        MAX(latencia) AS latencia_maxima
    FROM recorridos
    GROUP BY dispositivo_id;

END//

DELIMITER ;