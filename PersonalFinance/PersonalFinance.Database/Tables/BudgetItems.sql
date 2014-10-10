CREATE TABLE dbo.BudgetItems(
	Id int IDENTITY(1,1) NOT NULL,
	Household uniqueidentifier NOT NULL,
	[Type] varchar(256) NOT NULL,
	[Description] nvarchar(256) NOT NULL,
	Amount money default 0 NOT NULL,
	AnnualFrequency int default 12 NOT NULL
)
GO

ALTER TABLE dbo.BudgetItems
ADD CONSTRAINT [PK_dbo.BudgetItems] PRIMARY KEY CLUSTERED (Id ASC)
GO