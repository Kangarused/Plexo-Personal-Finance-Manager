CREATE PROCEDURE [Security].[GetUserClaims] @userId int
AS

SELECT * 
FROM [Security].[UserClaims]
WHERE UserId = @userId