CREATE PROC dbo.GetAccountById
	@AccountId int
AS

SELECT	A.Id AS Id,
		A.Name AS Name,
		ISNULL(SUM(T.Amount), 0) AS Balance,
		ISNULL(SUM(T.Reconciled), 0) AS Reconciled,
		A.isReconciled AS isReconciled
FROM dbo.Accounts AS A
LEFT JOIN dbo.Transactions AS T
ON A.Id = T.AccountId
WHERE A.Id = @AccountId
GROUP BY A.Id, A.Name, A.isReconciled