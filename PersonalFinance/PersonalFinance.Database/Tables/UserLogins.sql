CREATE TABLE [Security].[UserLogins](
	[UserId] [int] NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL
)
GO

ALTER TABLE [Security].[UserLogins]
ADD CONSTRAINT [PK_Security.UserLogins] PRIMARY KEY CLUSTERED
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[ProviderKey] ASC
)
GO

ALTER TABLE [Security].[UserLogins]
WITH CHECK ADD CONSTRAINT [FK_Security.UserLogins_Security.Users_UserId]
FOREIGN KEY([UserId])
REFERENCES [Security].[Users] ([Id])
GO

AUTOPROC Insert,Delete [Security].[UserLogins]
GO