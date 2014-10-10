CREATE TABLE dbo.Transactions(
	Id int IDENTITY(1,1) NOT NULL,
	AccountId int NOT NULL,
	[Description] nvarchar(256) NOT NULL,
	Amount money default 0 NOT NULL,
	Reconciled money default 0 NOT NULL,
	isReconciled bit default 1 NOT NULL,
	TransactionDate datetimeoffset(7) default SYSDATETIMEOFFSET() NOT NULL,
	Updated_By int NOT NULL,
	Created_On datetimeoffset(7) default SYSDATETIMEOFFSET() NOT NULL,
	Updated_On datetimeoffset(7) default SYSDATETIMEOFFSET() NOT NULL
)
GO

ALTER TABLE dbo.Transactions
ADD CONSTRAINT [PK_dbo.Transactions] PRIMARY KEY CLUSTERED (Id ASC)
GO

ALTER TABLE dbo.Transactions
WITH CHECK ADD CONSTRAINT [FK_dbo.Transactions_dbo.Accounts_AccountId]
FOREIGN KEY (AccountId)
REFERENCES dbo.Accounts (Id)
GO
