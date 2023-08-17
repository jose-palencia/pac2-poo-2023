INSERT INTO Transacciones
(UsuarioId, 
 FechaTransaccion, 
 Monto, 
 TipoTransaccionId,
 Nota)
VALUES
(
	'jperez',
	'2023-07-14',
	150,
	1,
	'Prueba de nota de transacción 02'
);

SELECT 
	Id,
	Monto,
	FechaTransaccion
FROM Transacciones;

SELECT
*
FROM Transacciones
ORDER BY Monto DESC

SELECT 'Hola SQL';

INSERT INTO TipoCuenta
(Nombre, UsuarioId, Orden)
VALUES
(@Nombre, @UsuarioId, @Orden);

SELECT SCOPE_IDENTITY();


SELECT
	1
FROM TipoCuenta
WHERE Nombre = 'Préstamos' AND UsuarioId = 1
--WHERE Nombre = @Nombre AND UsuarioId = @UsuarioId

SELECT 
	Id,
	Nombre,
	Orden
FROM TipoCuenta
WHERE UsuarioId = @UsuarioId;

-- Obtener Por Id
DECLARE @id int = 2;
DECLARE @usuarioId int = 1;

SELECT 
	Id,
	Nombre,
	Orden
FROM TipoCuenta 
WHERE Id = @id AND UsuarioId = @usuarioId;

-- Editar Tipo de Cuenta
DECLARE @id int = 2;
DECLARE @nombre nvarchar(50) = 'Ahorros';

UPDATE TipoCuenta
SET Nombre = @nombre
WHERE Id = @id;

-- Borrar Tipo de Cuenta
DECLARE @id int = 6;

DELETE FROM TipoCuenta WHERE Id = @id;


SELECT * FROM TipoCuenta
Order by Orden



UPDATE TipoCuenta SET Orden = @Orden WHERE Id = @id;


-- SP Insertar Tipo Cuenta

CREATE PROCEDURE TiposCuentas_Insertar
	@Nombre NVARCHAR(50),
	@UsuarioId INT
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Orden INT;
	SELECT @Orden = COALESCE(MAX(Orden), 0) + 1
	FROM TipoCuenta 
	WHERE UsuarioId = @UsuarioId;

	INSERT INTO TipoCuenta (Nombre, UsuarioId, Orden)
	VALUES (@Nombre, @UsuarioId, @Orden);

	SELECT SCOPE_IDENTITY();
END

-- Insertar cuenta

INSERT INTO Cuentas (Nombre, TipoCuentaId, Balance, Descripcion)
VALUES (@Nombre, @TipoCuentaId, @Balance, @Descripcion);
SELECT SCOPE_IDENTITY();


SELECT 
	cue.Id,
	cue.Nombre,
	cue.Balance,
	tcue.Nombre AS TipoCuenta
FROM Cuentas AS cue
 INNER JOIN TipoCuenta AS tcue
 ON cue.TipoCuentaId = tcue.Id
WHERE tcue.UsuarioId = @UsuarioId
ORDER BY tcue.Orden;

DECLARE @id int = 4;
DECLARE @UsuarioId int = 1;

SELECT 
	cu.Id,
	cu.Nombre,
	cu.Balance,
	cu.Descripcion,
	cu.TipoCuentaId
FROM Cuentas cu
INNER JOIN TipoCuenta tc 
ON tc.Id = cu.TipoCuentaId
WHERE tc.UsuarioId = @UsuarioId AND cu.Id = @Id;



SELECT * from Cuentas