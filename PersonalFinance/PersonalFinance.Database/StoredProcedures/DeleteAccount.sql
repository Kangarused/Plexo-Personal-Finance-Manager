CREATE PROC dbo.DeleteAccount 
	@AccountId int
AS

UPDATE dbo.Accounts
SET Updated_On = SYSDATETIMEOFFSET(),
	isDeleted = 1,
	Deleted_On = SYSDATETIMEOFFSET()
WHERE Id = @AccountId

SELECT CONVERT(bit, CASE @@ROWCOUNT
						WHEN 0 THEN 0
						ELSE 1
						END)