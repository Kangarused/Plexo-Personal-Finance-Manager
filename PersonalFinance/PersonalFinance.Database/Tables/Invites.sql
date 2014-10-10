CREATE TABLE dbo.Invites(
	Id int IDENTITY(1,1) NOT NULL,
	FromUserId int NOT NULL,
	ToEmail nvarchar(128) NOT NULL,
	Created_On datetimeoffset(7) default SYSDATETIMEOFFSET() NOT NULL,
	Pending bit default 1 NOT NULL,
	Accepted bit default 0 NOT NULL,
	DateAccepted datetimeoffset(7) NULL
)
GO

ALTER TABLE dbo.Invites
ADD CONSTRAINT [PK_dbo.Invites] PRIMARY KEY (Id ASC)
GO

ALTER TABLE dbo.Invites
WITH CHECK ADD CONSTRAINT [FK_dbo.Invites_dbo.Users_FromUserId]
FOREIGN KEY (FromUserId)
REFERENCES [Security].[Users] (Id)
GO