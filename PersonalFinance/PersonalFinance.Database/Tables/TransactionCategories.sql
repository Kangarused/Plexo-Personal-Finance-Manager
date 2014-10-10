CREATE TABLE dbo.TransactionCategories(
	TransactionId int NOT NULL,
	CategoryId int NOT NULL
)
GO

ALTER TABLE dbo.TransactionCategories
ADD CONSTRAINT [PK_dbo.TransactionCategories] PRIMARY KEY CLUSTERED 
(
	TransactionId ASC,
	CategoryId ASC
)
GO

ALTER TABLE dbo.TransactionCategories
WITH CHECK ADD CONSTRAINT [FK_dbo.TransactionCategories_dbo.Transactions_TransactionId]
FOREIGN KEY (TransactionId)
REFERENCES dbo.Transactions (Id)
GO

ALTER TABLE dbo.TransactionCategories
WITH CHECK ADD CONSTRAINT [FK_dbo.TransactionCategories_dbo.Categories_CategoryId]
FOREIGN KEY (CategoryId)
REFERENCES dbo.Categories (Id)
GO