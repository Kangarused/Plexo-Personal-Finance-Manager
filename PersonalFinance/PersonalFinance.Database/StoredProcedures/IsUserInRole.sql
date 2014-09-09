CREATE PROCEDURE [Security].[IsUserInRole] 
@userId int, @roleName nvarchar(max)
AS

SELECT CONVERT(bit, Count(*))
FROM [Security].[UserRoles] as ur
INNER JOIN [Security].[Roles] as r
ON ur.RoleId = r.Id
WHERE ur.UserId = @userId and r.Name = @roleName