CREATE PROC dbo.InsertAccount
(
	@UserId int,
	@Household uniqueidentifier,
	@Name varchar(256)
)
AS

INSERT INTO dbo.Accounts
(
	UserId,
	Household,
	Name
)
VALUES
(
	@UserId,
	@Household,
	@Name
)

SELECT CONVERT(bit, CASE @@ROWCOUNT
						WHEN 0 THEN 0
						ELSE 1
						END)
