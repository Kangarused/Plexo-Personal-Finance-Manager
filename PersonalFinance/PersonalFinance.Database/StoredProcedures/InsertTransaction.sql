CREATE PROC dbo.InsertTransaction
(
	@AccountId int,
	@Description nvarchar(256),
	@Amount money,
	@Reconciled money,
	@isReconciled bit,
	@TransactionDate datetimeoffset(7),
	@Updated_By int
)
AS

INSERT INTO dbo.Transactions
(
	AccountId,
	[Description],
	Amount,
	Reconciled,
	isReconciled,
	TransactionDate,
	Updated_By
)
VALUES
(
	@AccountId,
	@Description,
	@Amount,
	@Reconciled,
	@isReconciled,
	@TransactionDate,
	@Updated_By
);

SELECT CAST(SCOPE_IDENTITY() AS INT)