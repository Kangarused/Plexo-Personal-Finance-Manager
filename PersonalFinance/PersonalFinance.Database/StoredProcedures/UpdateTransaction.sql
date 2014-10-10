CREATE PROC dbo.UpdateTransaction
	@Id int,
	@Description nvarchar(256),
	@Amount money,
	@Reconciled money,
	@isReconciled bit,
	@Updated_By int
AS

UPDATE dbo.Transactions
SET	[Description] = @Description,
	Amount = @Amount,
	Reconciled = @Reconciled,
	isReconciled = @isReconciled,
	Updated_By = @Updated_By
WHERE Id = @Id

SELECT CONVERT(bit, CASE @@ROWCOUNT
						WHEN 0 THEN 0
						ELSE 1
						END)