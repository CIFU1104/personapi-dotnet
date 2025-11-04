-- DDL: persona_db (SQL Server)
IF DB_ID('persona_db') IS NULL
  CREATE DATABASE persona_db;
GO
USE persona_db;
GO

-- Tabla persona
IF OBJECT_ID('dbo.persona') IS NOT NULL DROP TABLE dbo.persona;
CREATE TABLE dbo.persona(
  cc        INT          NOT NULL PRIMARY KEY,
  nombre    NVARCHAR(45) NOT NULL,
  apellido  NVARCHAR(45) NOT NULL,
  genero    CHAR(1)      NOT NULL CHECK (genero IN ('M','F')),
  edad      INT          NULL
);

-- Tabla profesion
IF OBJECT_ID('dbo.profesion') IS NOT NULL DROP TABLE dbo.profesion;
CREATE TABLE dbo.profesion(
  id   INT          NOT NULL PRIMARY KEY,
  nom  NVARCHAR(90) NOT NULL,
  des  NVARCHAR(MAX) NULL
);

-- Tabla estudios (N..N persona-profesion)
IF OBJECT_ID('dbo.estudios') IS NOT NULL DROP TABLE dbo.estudios;
CREATE TABLE dbo.estudios(
  id_prof INT NOT NULL,
  cc_per  INT NOT NULL,
  fecha   DATE NULL,
  univer  NVARCHAR(50) NULL,
  CONSTRAINT PK_estudios PRIMARY KEY (id_prof, cc_per),
  CONSTRAINT FK_estudios_profesion FOREIGN KEY (id_prof) REFERENCES dbo.profesion(id),
  CONSTRAINT FK_estudios_persona   FOREIGN KEY (cc_per)  REFERENCES dbo.persona(cc)
);

-- Tabla telefono
IF OBJECT_ID('dbo.telefono') IS NOT NULL DROP TABLE dbo.telefono;
CREATE TABLE dbo.telefono(
  num    NVARCHAR(15) NOT NULL PRIMARY KEY,
  oper   NVARCHAR(45) NOT NULL,
  duenio INT          NOT NULL,
  CONSTRAINT FK_telefono_persona FOREIGN KEY (duenio) REFERENCES dbo.persona(cc)
);
GO

-- DML de ejemplo
INSERT INTO dbo.persona(cc,nombre,apellido,genero,edad) VALUES
(1001,'Ana','Pérez','F',23),
(1002,'Luis','Gómez','M',30);

INSERT INTO dbo.profesion(id,nom,des) VALUES
(1,'Ingeniería de Sistemas','Desarrollo de software'),
(2,'Medicina','General');

INSERT INTO dbo.estudios(id_prof,cc_per,fecha,univer) VALUES
(1,1001,'2022-12-15','PUJ'),
(2,1002,'2020-06-30','UNAL');

INSERT INTO dbo.telefono(num,oper,duenio) VALUES
('3001112233','Claro',1001),
('3204445566','Tigo',1002);
