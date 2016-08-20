namespace PersonalFinance.Common.Model
{
    public interface IEntityWithAudit : IEntityWithTimeStamp
    {
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
    }
}
