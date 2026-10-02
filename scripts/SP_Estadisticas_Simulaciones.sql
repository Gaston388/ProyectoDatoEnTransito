USE datos_transito;

DROP PROCEDURE IF EXISTS sp_estadisticas_simulaciones;

DELIMITER //

CREATE PROCEDURE sp_estadisticas_simulaciones()
BEGIN

    SELECT
        COUNT(*) AS total_simulaciones,
        AVG(latencia) AS latencia_promedio,
        MAX(latencia) AS latencia_maxima
    FROM recorridos;

END//

DELIMITER ;