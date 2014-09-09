CREATE PROCEDURE [Security].[AddUserToRole]
@userId int, @roleName nvarchar(max)
AS

INSERT INTO [Security].[UserRoles] (UserId, RoleId)
SELECT @userId, r.Id
FROM [Security].[Roles] AS r
WHERE r.Name = @roleName

SELECT CONVERT(bit, CASE @@ROWCOUNT
						WHEN 0 THEN 0
						ELSE 1
						END)