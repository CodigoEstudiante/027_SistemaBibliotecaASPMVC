
create database DB_BIBLIOTECA

GO

USE DB_BIBLIOTECA

GO

CREATE TABLE  CATEGORIA(
IdCategoria int primary key identity,
Descripcion varchar(50),
Estado bit default 1,
FechaCreacion datetime default getdate()
)

go

CREATE TABLE  EDITORIAL(
IdEditorial int primary key identity,
Descripcion varchar(50),
Estado bit default 1,
FechaCreacion datetime default getdate()
)

go


CREATE TABLE  AUTOR(
IdAutor int primary key identity,
Descripcion varchar(50),
Estado bit default 1,
FechaCreacion datetime default getdate()
)

go

CREATE TABLE LIBRO(
IdLibro int primary key identity,
Titulo varchar(100),
RutaPortada varchar(100),
NombrePortada varchar(100),
IdAutor int references AUTOR(IdAutor),
IdCategoria int references CATEGORIA(IdCategoria),
IdEditorial int references EDITORIAL(IdEditorial),
Ubicacion varchar(50),
Ejemplares int,
Estado bit default 1,
FechaCreacion datetime default getdate()
)

GO

CREATE TABLE TIPO_PERSONA(
IdTipoPersona  int primary key,
Descripcion varchar(50),
Estado bit default 1,
FechaCreacion datetime default getdate()
)

GO

CREATE TABLE PERSONA(
IdPersona int primary key identity,
Nombre varchar(50),
Apellido varchar(50),
Correo varchar(50),
Clave varchar(50),
Codigo varchar(50),
IdTipoPersona int references TIPO_PERSONA(IdTipoPersona),
Estado bit default 1,
FechaCreacion datetime default getdate()
)

go

CREATE TABLE ESTADO_PRESTAMO(
IdEstadoPrestamo int primary key,
Descripcion varchar(50),
Estado bit default 1,
FechaCreacion datetime default getdate()
)
GO

CREATE TABLE PRESTAMO(
IdPrestamo int primary key identity,
IdEstadoPrestamo int references ESTADO_PRESTAMO(IdEstadoPrestamo),
IdPersona int references PERSONA(IdPersona),
IdLibro int references Libro(IdLibro),
FechaDevolucion datetime,
FechaConfirmacionDevolucion datetime,
EstadoEntregado varchar(100),
EstadoRecibido varchar(100),
Estado bit default 1,
FechaCreacion datetime default getdate()
)