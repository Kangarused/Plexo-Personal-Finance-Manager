CREATE PROC dbo.UpdateTransactionCategory
	@TransactionId int,
	@CategoryId int
AS

UPDATE dbo.TransactionCategories
SET CategoryId = @CategoryId
WHERE TransactionId = @TransactionId

SELECT CONVERT(bit, CASE @@ROWCOUNT
						WHEN 0 THEN 0
						ELSE 1
						END)