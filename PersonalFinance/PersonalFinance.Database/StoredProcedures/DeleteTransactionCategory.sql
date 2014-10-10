CREATE PROC dbo.DeleteTransactionCategory
	@TransactionId int,
	@CategoryId int
AS

DELETE FROM dbo.TransactionCategories
WHERE TransactionId = @TransactionId AND CategoryId = @CategoryId

SELECT CONVERT(bit, CASE @@ROWCOUNT
						WHEN 0 THEN 0
						ELSE 1
						END)