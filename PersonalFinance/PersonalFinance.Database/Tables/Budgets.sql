CREATE TABLE dbo.Budgets(
	Id int IDENTITY(1,1) NOT NULL,
	Household uniqueidentifier NOT NULL
)
GO

ALTER TABLE dbo.Budgets
ADD CONSTRAINT [PK_dbo.Budgets] PRIMARY KEY CLUSTERED (Id ASC)
GO