CREATE PROCEDURE [Security].[RemoveUserFromRole] 
@userId int, @roleName nvarchar(max)
AS

DELETE ur 
	FROM [Security].[UserRoles] AS ur
	INNER JOIN [Security].[Roles] AS r
	ON ur.RoleId = r.Id
	WHERE ur.UserId = @userId AND r.Name = @roleName
