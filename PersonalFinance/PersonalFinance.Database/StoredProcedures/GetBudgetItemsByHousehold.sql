CREATE PROC dbo.GetBudgetItemsByHousehold
	@Household uniqueidentifier
AS

SELECT	BI.Id AS Id,
		BI.Household AS Household,
		BI.[Type] AS [Type],
		BI.[Description] AS [Description],
		C.Name AS Category,
		BI.Amount AS Amount,
		BI.AnnualFrequency AS AnnualFrequency

FROM dbo.BudgetItems AS BI

INNER JOIN dbo.BudgetItemCategories AS BIC
ON BI.Id = BIC.BudgetItemId

INNER JOIN dbo.Categories AS C
ON BIC.CategoryId = C.Id

WHERE BI.Household = @Household

