CREATE TABLE dbo.Accounts(
	Id int IDENTITY(1,1) NOT NULL,
	UserId int NOT NULL,
	Household uniqueidentifier NOT NULL,
	Name varchar(256) NOT NULL,
	isReconciled bit default 1 NOT NULL,
	Created_On datetimeoffset(7) default SYSDATETIMEOFFSET() NOT NULL,
	Updated_On datetimeoffset(7) default SYSDATETIMEOFFSET() NOT NULL,
	isDeleted bit default 0 NOT NULL,
	Deleted_On datetimeoffset(7) NULL
)
GO

ALTER TABLE	dbo.Accounts
ADD CONSTRAINT [PK_dbo.Accounts] PRIMARY KEY CLUSTERED (Id ASC)
GO

ALTER TABLE dbo.Accounts
WITH CHECK ADD CONSTRAINT [FK_dbo.Accounts_Security.Users_UserId]
FOREIGN KEY (UserId)
REFERENCES [Security].[Users] (Id)
GO