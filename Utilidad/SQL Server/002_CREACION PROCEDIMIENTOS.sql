

USE DB_BIBLIOTECA

GO

--PROCEDIMIENTO PARA GUARDAR CATEGORIA
CREATE PROC sp_RegistrarCategoria(
@Descripcion varchar(50),
@Resultado bit output
)as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion = @Descripcion)

		insert into CATEGORIA(Descripcion) values (
		@Descripcion
		)
	ELSE
		SET @Resultado = 0
	
end


go

--PROCEDIMIENTO PARA MODIFICAR CATEGORIA
create procedure sp_ModificarCategoria(
@IdCategoria int,
@Descripcion varchar(60),
@Estado bit,
@Resultado bit output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion =@Descripcion and IdCategoria != @IdCategoria)
		
		update CATEGORIA set 
		Descripcion = @Descripcion,
		Estado = @Estado
		where IdCategoria = @IdCategoria
	ELSE
		SET @Resultado = 0

end

GO


--PROCEDIMIENTO PARA GUARDAR EDITORIAL
CREATE PROC sp_RegistrarEditorial(
@Descripcion varchar(50),
@Resultado bit output
)as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM EDITORIAL WHERE Descripcion = @Descripcion)

		insert into EDITORIAL(Descripcion) values (
		@Descripcion
		)
	ELSE
		SET @Resultado = 0
	
end


go

--PROCEDIMIENTO PARA MODIFICAR EDITORIAL
create procedure sp_ModificarEditorial(
@IdEditorial int,
@Descripcion varchar(60),
@Estado bit,
@Resultado bit output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM EDITORIAL WHERE Descripcion =@Descripcion and IdEditorial != @IdEditorial)
		
		update EDITORIAL set 
		Descripcion = @Descripcion,
		Estado = @Estado
		where IdEditorial = @IdEditorial
	ELSE
		SET @Resultado = 0

end


GO


--PROCEDIMIENTO PARA GUARDAR AUTOR
CREATE PROC sp_RegistrarAutor(
@Descripcion varchar(50),
@Resultado bit output
)as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM AUTOR WHERE Descripcion = @Descripcion)

		insert into AUTOR(Descripcion) values (
		@Descripcion
		)
	ELSE
		SET @Resultado = 0
	
end


go

--PROCEDIMIENTO PARA MODIFICAR AUTOR
create procedure sp_ModificarAutor(
@IdAutor int,
@Descripcion varchar(60),
@Estado bit,
@Resultado bit output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM AUTOR WHERE Descripcion =@Descripcion and IdAutor != @IdAutor)
		
		update AUTOR set 
		Descripcion = @Descripcion,
		Estado = @Estado
		where IdAutor = @IdAutor
	ELSE
		SET @Resultado = 0

end


go

create proc sp_registrarLibro(
@Titulo varchar(100),
@RutaPortada varchar(100),
@NombrePortada varchar(100),
@IdAutor int,
@IdCategoria int,
@IdEditorial int,
@Ubicacion varchar(100),
@Ejemplares int,
@Resultado int output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM LIBRO WHERE Titulo = @Titulo)
	begin
		insert into LIBRO(Titulo,RutaPortada,NombrePortada,IdAutor,IdCategoria,IdEditorial,Ubicacion,Ejemplares) values (
		@Titulo,@RutaPortada,@NombrePortada,@IdAutor,@IdCategoria,@IdEditorial,@Ubicacion,@Ejemplares)

		SET @Resultado = scope_identity()
	end
end

go

create proc sp_modificarLibro(
@IdLibro int,
@Titulo varchar(100),
@IdAutor int,
@IdCategoria int,
@IdEditorial int,
@Ubicacion varchar(100),
@Ejemplares int,
@Estado bit,
@Resultado bit output
)
as
begin
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM LIBRO WHERE Titulo = @Titulo and IdLibro != @IdLibro)
	begin

		update LIBRO set 
		Titulo = @Titulo,
		IdAutor = @IdAutor,
		IdCategoria = @IdCategoria,
		IdEditorial = @IdEditorial,
		Ubicacion = @Ubicacion,
		Ejemplares = @Ejemplares,
		Estado = @Estado
		where IdLibro = @IdLibro

		SET @Resultado = 1
	end
end

GO

create proc sp_actualizarRutaImagen(
@IdLibro int,
@NombrePortada varchar(500)
)
as
begin
	update libro set NombrePortada = @NombrePortada where IdLibro = @IdLibro
end



GO

CREATE FUNCTION fn_obtenercorrelativo(@numero int)

RETURNS varchar(100)
AS
BEGIN
	DECLARE @correlativo varchar(100)

	set @correlativo = 'LE' + RIGHT('000000' + CAST(@numero AS varchar), 6)

	RETURN @correlativo

END

GO

--PROCEDIMIENTO PARA GUARDAR CATEGORIA
create PROC sp_RegistrarPersona(
@Nombre varchar(50),
@Apellido varchar(50),
@Correo varchar(50),
@Clave varchar(50),
@IdTipoPersona int,
@Resultado bit output
)as
begin
	SET @Resultado = 1
	DECLARE @IDPERSONA INT 
	IF NOT EXISTS (SELECT * FROM persona WHERE correo = @Correo)
	begin
		insert into persona(Nombre,Apellido,Correo,Clave,IdTipoPersona) values (
		@Nombre,@Apellido,@Correo,@Clave,@IdTipoPersona)

		SET @IDPERSONA = SCOPE_IDENTITY()
		print @IDPERSONA
		if(@IdTipoPersona = 3)
		begin
			print 'si es igual'
			UPDATE PERSONA SET 
			Codigo = dbo.fn_obtenercorrelativo(@IDPERSONA),
			Clave = dbo.fn_obtenercorrelativo(@IDPERSONA)
			WHERE IdPersona = @IDPERSONA
		end
	end
	ELSE
		SET @Resultado = 0
	
end


go

--PROCEDIMIENTO PARA MODIFICAR CATEGORIA
create procedure sp_ModificarPersona(
@IdPersona int,
@Nombre varchar(50),
@Apellido varchar(50),
@Correo varchar(50),
@Clave varchar(50),
@IdTipoPersona int,
@Estado bit,
@Resultado bit output
)
as
begin
	SET @Resultado = 1
	IF NOT EXISTS (SELECT * FROM persona WHERE correo =@Correo and IdPersona != @IdPersona)
		
		update PERSONA set 
		Nombre = @Nombre,
		Apellido = @Apellido,
		Correo = @Correo,
		IdTipoPersona = @IdTipoPersona,
		Estado = @Estado
		where IdPersona = @IdPersona
	ELSE
		SET @Resultado = 0

end

GO


create PROC sp_RegistrarPrestamo(
@IdEstadoPrestamo int,
@IdPersona int,
@IdLibro int,
@FechaDevolucion datetime,
@EstadoEntregado varchar(500),
@Resultado bit output
)as
begin
	SET DATEFORMAT dmy; 
	INSERT INTO PRESTAMO(IdEstadoPrestamo,IdPersona,IdLibro,FechaDevolucion,EstadoEntregado)
	values(@IdEstadoPrestamo,@IdPersona,@IdLibro,@FechaDevolucion,@EstadoEntregado)

	SET @Resultado = 1
end


go

create PROC sp_existePrestamo(
@IdPersona int,
@IdLibro int,
@Resultado bit output
)as
begin
	SET @Resultado = 0

	if(exists(select * from PRESTAMO where IdPersona = @IdPersona and IdLibro =@IdLibro ))
	begin
		SET @Resultado = 1
	end
	
end


