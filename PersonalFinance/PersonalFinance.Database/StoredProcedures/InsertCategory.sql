CREATE PROC dbo.InsertCategory
	@Name varchar(256)
AS
	
INSERT INTO dbo.Categories (Name) VALUES (@Name);

SELECT CAST(SCOPE_IDENTITY() AS INT)