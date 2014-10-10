CREATE PROC dbo.DeleteTransaction
	@TransactionId int
AS

DELETE FROM dbo.Transactions
WHERE Id = @TransactionId

SELECT CONVERT(bit, CASE @@ROWCOUNT
						WHEN 0 THEN 0
						ELSE 1
						END)