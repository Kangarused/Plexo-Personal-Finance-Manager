CREATE PROCEDURE [Security].[FindUserByLogin]
@loginProvider nvarchar(128), @providerKey nvarchar(128)
AS

SELECT u.*
FROM [Security].[Users] AS u
INNER JOIN [Security].[UserLogins] AS l
ON u.Id = l.UserId
WHERE l.LoginProvider = @loginProvider AND l.ProviderKey = @providerKey