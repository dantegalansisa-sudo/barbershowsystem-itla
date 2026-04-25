-- =========================================
-- SCRIPT SQL - BARBERIA APP
-- Base de datos para el sistema de gestion
-- de una barberia
-- =========================================

-- Crear la base de datos
CREATE DATABASE BarberiaDB;
GO

-- Usar la base de datos creada
USE BarberiaDB;
GO

-- =========================================
-- TABLA: Clientes
-- Guarda la informacion de los clientes
-- =========================================
CREATE TABLE Clientes
(
    Id          INT IDENTITY(1,1) PRIMARY KEY,   -- Id autoincremental
    Nombre      NVARCHAR(100)     NOT NULL,       -- Nombre completo del cliente
    Telefono    NVARCHAR(20)      NOT NULL,       -- Numero de telefono
    Email       NVARCHAR(100)     NOT NULL        -- Correo electronico
);
GO

-- =========================================
-- TABLA: Servicios
-- Guarda los servicios que ofrece la barberia
-- =========================================
CREATE TABLE Servicios
(
    Id          INT IDENTITY(1,1) PRIMARY KEY,   -- Id autoincremental
    Nombre      NVARCHAR(100)     NOT NULL,       -- Nombre del servicio
    Precio      DECIMAL(10,2)     NOT NULL        -- Precio del servicio
);
GO

-- =========================================
-- TABLA: Visitas
-- Guarda el historial de visitas de los clientes
-- Relaciona un cliente con un servicio en una fecha
-- =========================================
CREATE TABLE Visitas
(
    Id          INT IDENTITY(1,1) PRIMARY KEY,   -- Id autoincremental
    ClienteId   INT               NOT NULL,       -- Referencia al cliente
    ServicioId  INT               NOT NULL,       -- Referencia al servicio
    Fecha       DATETIME          NOT NULL,       -- Fecha y hora de la visita

    -- Llaves foraneas para mantener la integridad
    CONSTRAINT FK_Visitas_Clientes FOREIGN KEY (ClienteId) REFERENCES Clientes(Id),
    CONSTRAINT FK_Visitas_Servicios FOREIGN KEY (ServicioId) REFERENCES Servicios(Id)
);
GO

-- =========================================
-- DATOS INICIALES
-- Insertamos algunos servicios de ejemplo
-- =========================================
INSERT INTO Servicios (Nombre, Precio) VALUES ('Corte clasico', 150.00);
INSERT INTO Servicios (Nombre, Precio) VALUES ('Corte con degradado', 200.00);
INSERT INTO Servicios (Nombre, Precio) VALUES ('Afeitado con navaja', 100.00);
INSERT INTO Servicios (Nombre, Precio) VALUES ('Corte y barba', 250.00);
INSERT INTO Servicios (Nombre, Precio) VALUES ('Lavado de cabello', 80.00);
INSERT INTO Servicios (Nombre, Precio) VALUES ('Tinte de cabello', 350.00);
GO

-- =========================================
-- DATOS DE PRUEBA (OPCIONALES)
-- Algunos clientes de ejemplo para probar
-- =========================================
INSERT INTO Clientes (Nombre, Telefono, Email) VALUES ('Juan Perez', '555-1234', 'juan.perez@email.com');
INSERT INTO Clientes (Nombre, Telefono, Email) VALUES ('Carlos Lopez', '555-5678', 'carlos.lopez@email.com');
INSERT INTO Clientes (Nombre, Telefono, Email) VALUES ('Miguel Torres', '555-9012', 'miguel.torres@email.com');
GO

-- Algunas visitas de ejemplo
INSERT INTO Visitas (ClienteId, ServicioId, Fecha) VALUES (1, 1, GETDATE());
INSERT INTO Visitas (ClienteId, ServicioId, Fecha) VALUES (2, 4, GETDATE());
INSERT INTO Visitas (ClienteId, ServicioId, Fecha) VALUES (3, 2, GETDATE());
GO

-- Verificar que todo se creo correctamente
SELECT 'Clientes' AS Tabla, COUNT(*) AS Total FROM Clientes
UNION ALL
SELECT 'Servicios', COUNT(*) FROM Servicios
UNION ALL
SELECT 'Visitas', COUNT(*) FROM Visitas;
GO
