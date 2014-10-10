CREATE PROC dbo.GetAccountsByHousehold
	@Household uniqueidentifier
AS

SELECT	A.Id AS Id,
		A.Name AS Name,
		ISNULL(SUM(T.Amount), 0) AS Balance,
		ISNULL(SUM(T.Reconciled), 0) AS Reconciled,
		A.isReconciled AS isReconciled
FROM dbo.Accounts AS A
LEFT JOIN dbo.Transactions AS T
ON A.Id = T.AccountId
WHERE A.Household = @Household AND A.isDeleted = CONVERT(bit,0)
GROUP BY A.Id, A.Name, A.isReconciled