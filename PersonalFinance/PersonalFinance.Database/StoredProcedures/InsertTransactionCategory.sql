CREATE PROC dbo.InsertTransactionCategory
	@TransactionId int,
	@CategoryId int
AS

INSERT INTO dbo.TransactionCategories
(
	TransactionId,
	CategoryId
)
VALUES
(
	@TransactionId,
	@CategoryId
)

SELECT CONVERT(bit, CASE @@ROWCOUNT
						WHEN 0 THEN 0
						ELSE 1
						END)