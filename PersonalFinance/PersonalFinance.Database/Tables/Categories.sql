CREATE TABLE dbo.Categories(
	Id int IDENTITY(1,1) NOT NULL,
	Name varchar(256) NOT NULL
)
GO

ALTER TABLE dbo.Categories
ADD CONSTRAINT [PK_dbo.Categories] PRIMARY KEY CLUSTERED (Id ASC)
GO