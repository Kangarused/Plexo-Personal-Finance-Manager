CREATE PROCEDURE [Security].[RemoveUserClaim]
@userId int, @claimType nvarchar(max)
AS

DELETE FROM [Security].[UserClaims]
WHERE UserId = @userId AND ClaimType = @claimType