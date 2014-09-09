CREATE PROCEDURE [Security].[GetLoginsForUser] @userId int
AS

SELECT *
FROM [Security].[UserLogins]
WHERE UserId = @userId
