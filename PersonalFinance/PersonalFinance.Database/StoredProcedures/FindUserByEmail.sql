CREATE PROCEDURE [Security].[FindUserByEmail] @Email nvarchar(128)
AS

SELECT * FROM [Security].[Users]
WHERE Email = @Email
