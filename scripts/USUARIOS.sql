CREATE USER IF NOT EXISTS 'administrador'@'localhost'
IDENTIFIED BY 'Admin1234!';

CREATE USER IF NOT EXISTS '5to_agbd'@'localhost'
IDENTIFIED BY 'Trigg3rs!';

GRANT ALL PRIVILEGES
ON *.*
TO 'administrador'@'localhost';

GRANT SELECT, INSERT, UPDATE, DELETE, EXECUTE
ON datos_transito.*
TO '5to_agbd'@'localhost';

FLUSH PRIVILEGES;