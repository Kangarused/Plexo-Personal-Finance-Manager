CREATE PROC dbo.GetCategoryByName
	@categoryName varchar(256)
AS

SELECT * FROM dbo.Categories
WHERE Name = @categoryName