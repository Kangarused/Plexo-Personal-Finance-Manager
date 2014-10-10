CREATE PROC dbo.GetTransactionsByAccountId
	@AccountId int
AS

SELECT	T.Id AS Id,
		T.AccountId AS AccountId,
		T.[Description] AS [Description],
		C.Name AS Category,
		T.Amount AS Amount,
		T.Reconciled AS Reconciled,
		T.isReconciled AS isReconciled,
		T.TransactionDate AS TransactionDate,
		T.Created_On AS CreatedOn,
		U.Name AS UpdatedBy,
		T.Updated_On AS UpdatedOn
FROM dbo.Transactions AS T

INNER JOIN dbo.TransactionCategories AS TC 
ON T.Id = TC.TransactionId

INNER JOIN dbo.Categories AS C
ON TC.CategoryId = C.Id

INNER JOIN [Security].[Users] AS U
ON T.Updated_By = U.Id

WHERE T.AccountId = @AccountId