CREATE PROCEDURE [Security].[FindUserByUserName] @UserName nvarchar(128)
AS

SELECT * FROM [Security].[Users] 
WHERE UserName = @UserName