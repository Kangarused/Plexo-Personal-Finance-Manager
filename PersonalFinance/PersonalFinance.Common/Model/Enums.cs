using System.ComponentModel;
using PersonalFinance.Common.Utils;
using T4TS;

namespace PersonalFinance.Common.Model
{
    [TypescriptEnum]
    public enum Role
    {
        PublicUser,
        HouseholdMember,
        HouseholdAdmin,
        AnonymousOverPrivateApi,
        SystemAdministrator
    }

    [TypescriptEnum]
    public enum BudgetType
    {
        Savings,
        Spendings
    }

    [TypescriptEnum]
    public enum BudgetItemType
    {
        Expense,
        Income
    }

    [TypescriptEnum]
    public enum PaymentFrequency
    {
        Daily,
        Weekly,
        Fortnightly,
        Monthly,
        Yearly
    }
}
