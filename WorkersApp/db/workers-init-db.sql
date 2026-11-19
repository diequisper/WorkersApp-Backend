USE master;
GO
IF DB_ID('workers') IS NOT NULL
BEGIN
    ALTER DATABASE workers SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE workers;
END
GO

--CREACION DE BASE DE DATOS
CREATE DATABASE workers
GO

USE workers
GO

--CREACION DE TABLAS
CREATE TABLE usuario(
	id INT IDENTITY(1,1) PRIMARY KEY,
	username VARCHAR(40) NOT NULL UNIQUE,
	clave VARCHAR(500) NOT NULL
)
GO

CREATE TABLE trabajador(
    id INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50) NOT NULL,
    dni CHAR(8) NOT NULL UNIQUE,
    edad INT NOT NULL
)
GO

--PROCEDIMIENTOS ALMACENADOS

CREATE PROCEDURE sp_get_usuario_by_username
    @username VARCHAR(40)
AS
BEGIN
    SELECT id, username, clave
    FROM usuario
    WHERE username = @username;
END;
GO

CREATE PROCEDURE sp_get_all_trabajador
AS
BEGIN
    SELECT * FROM trabajador;
END;
GO

CREATE PROCEDURE sp_insert_trabajador
    @nombre VARCHAR(50),
    @apellido VARCHAR(50),
    @dni CHAR(8),
    @edad INT
AS
BEGIN
    INSERT INTO trabajador VALUES (@nombre, @apellido, @dni, @edad);
END;
GO

CREATE PROCEDURE sp_find_trabajador_by_dni
    @dni VARCHAR(8) = NULL
AS
BEGIN
    SELECT id, nombre, apellido, dni, edad
    FROM trabajador
    WHERE @dni IS NULL
       OR dni LIKE '%' + @dni + '%';
END;
GO

--INSERTS DE PRUEBA
INSERT INTO usuario VALUES ('demo', 'AQAAAAIAAYagAAAAEPXrFI0BNu7CsUWymkXvyXtoHLTr33nJSsisUvCIaPU5PDSkqaw4ePPevHe2M92hIg==')
GO

INSERT INTO trabajador (nombre, apellido, dni, edad) VALUES
('Diego', 'Quispe', '74839216', 25),
('Carlos', 'Ramirez', '61582743', 31),
('Lucia', 'Gonzales', '90471652', 28),
('Andres', 'Torres', '58394127', 24),
('Valeria', 'Flores', '72615384', 35),
('Miguel', 'Castillo', '49182763', 29),
('Camila', 'Mendoza', '83746195', 42),
('Jorge', 'Vargas', '65821479', 33),
('Fernanda', 'Rojas', '91263847', 27),
('Ricardo', 'Salazar', '57419286', 38),
('Daniel', 'Paredes', '78152639', 30),
('Sofia', 'Herrera', '43671825', 26),
('Marco', 'Navarro', '86924157', 34),
('Andrea', 'Medina', '52386491', 23),
('Roberto', 'Campos', '69431728', 41),
('Gabriela', 'Vega', '81573942', 32),
('Oscar', 'Cabrera', '36792518', 37),
('Alejandro', 'Fuentes', '95841326', 29),
('Mariana', 'Ortega', '64178593', 36);
GO

USE master;
GO
IF SUSER_ID('invitado') IS NULL
    CREATE LOGIN invitado WITH PASSWORD = '$(API_DB_PASSWORD)';
ELSE
    ALTER LOGIN invitado WITH PASSWORD = '$(API_DB_PASSWORD)';
GO

USE workers;
GO
CREATE USER invitado FOR LOGIN invitado;
GRANT EXECUTE ON SCHEMA::dbo TO invitado;
GO