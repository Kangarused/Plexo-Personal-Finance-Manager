using PersonalFinance.Common.Model;

namespace PersonalFinance.Migrations.Profiles.MockData
{
    public class MockBudget
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? HouseholdId { get; set; }
        public string Name { get; set; }
        public BudgetType Type { get; set; }
        public int AllocatedAmount { get; set; }
        public int Balance { get; set; }
    }
}
