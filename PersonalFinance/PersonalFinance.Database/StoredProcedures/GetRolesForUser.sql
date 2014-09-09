CREATE PROCEDURE [Security].[GetRolesForUser] @userId int
AS

SELECT r.Name
FROM [Security].[Roles] AS r
INNER JOIN [Security].[UserRoles] AS ur
ON r.Id = ur.RoleId
WHERE ur.UserId = @userId