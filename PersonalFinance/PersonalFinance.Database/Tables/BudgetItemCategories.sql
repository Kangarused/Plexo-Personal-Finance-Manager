CREATE TABLE dbo.BudgetItemCategories(
	CategoryId int NOT NULL,
	BudgetItemId int NOT NULL
)
GO

ALTER TABLE dbo.BudgetItemCategories
ADD CONSTRAINT [PK_dbo.BudgetItemCategories] PRIMARY KEY CLUSTERED
(
	CategoryId ASC,
	BudgetItemId ASC
)
GO

ALTER TABLE dbo.BudgetItemCategories
WITH CHECK ADD CONSTRAINT [FK_dbo.BudgetItemCategories_dbo.Cateogries_CategoryId]
FOREIGN KEY (CategoryId)
REFERENCES dbo.Categories (Id)
GO

ALTER TABLE dbo.BudgetItemCategories
WITH CHECK ADD CONSTRAINT [FK_dbo.BudgetItemCategories_dbo.BudgetItem_BudgetItemId]
FOREIGN KEY (BudgetItemId)
REFERENCES dbo.BudgetItems (Id)
GO