CREATE TABLE tblTestProduct
(
	ID INT IDENTITY PRIMARY KEY,
	Name VARCHAR(MAX),
	Description VARCHAR(MAX)
)
GO
GO
--EXEC spAddTestProduct @Name = 'P 3', @Description = 'Product 3'
CREATE PROCEDURE spAddTestProduct
(
	@Name VARCHAR(MAX) NULL,
	@Description VARCHAR(100)
)
AS
BEGIN
	INSERT INTO tblTestProduct 
		(Name,Description) 
	VALUES 
		(@Name,@Description)
END
GO
-----------------------------------------------------
-- EXEC spGetTestProductByID @ID = NULL
CREATE PROCEDURE spGetTestProductByID
(
	@ID INT = NULL
)
AS
BEGIN
	IF (@ID IS NULL OR @ID < 1) 
	BEGIN
		RAISERROR('Invalid ID',16,1);
		RETURN;
	END

	IF EXISTS ( SELECT TOP 1 [Name] FROM tblTestProduct WHERE ID =  @ID )
	BEGIN
		SELECT * FROM tblTestProduct WHERE ID =  @ID
	END
	ELSE
	BEGIN
		RAISERROR('ID is not exists.',16,1);
		RETURN;
	END
END
-----------------------------------------------------
GO
--EXEC spGetTestProductList
CREATE PROCEDURE spGetTestProductList
AS
BEGIN
	SELECT * FROM tblTestProduct
END
GO
-----------------------------------------------------
--EXEC spUpdateTestProduct @Name = 'P 1 - Updated', @Description = 'Product 1', @ID = 2
CREATE PROCEDURE spUpdateTestProduct
(
	@ID INT,
	@Name VARCHAR(MAX),
	@Description VARCHAR(MAX)
	 
)
AS
BEGIN
	IF EXISTS ( SELECT TOP 1 Name FROM tblTestProduct WHERE ID = @ID )
	BEGIN
		UPDATE tblTestProduct SET Name = @Name, Description = @Description WHERE ID = @ID
	END
	ELSE
	BEGIN
		RAISERROR('Name is not exists.',16,1);
		RETURN;
	END
END
-----------------------------------------------------
--SELECT TOP 10 * FROM tblTestProduct

--SELECT TOP 10 * FROM tblTestProduct
--EXEC spDeleteTestProduct @ID = 2
GO
CREATE PROCEDURE spDeleteTestProduct
(
	@ID INT
)
AS
BEGIN
	IF EXISTS ( SELECT TOP 1 Name FROM tblTestProduct WHERE ID = @ID )
	BEGIN
		DELETE FROM tblTestProduct WHERE ID = @ID
	END
	ELSE
	BEGIN
		RAISERROR('Product is not exists.',16,1);
		RETURN;
	END
END
-----------------------------------------------------

