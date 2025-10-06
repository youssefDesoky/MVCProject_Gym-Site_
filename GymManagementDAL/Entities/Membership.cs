namespace GymManagementDAL.Entities;

public class Membership : BaseEntity
{
    public DateTime EndDate { get; set; }
    public string Status
    {
        get => DateTime.Now > EndDate ? "Expired" : "Active";
    }
    public int MemberId { get; set; }
    public Member Member { get; set; } = null!;
    public int PlanId { get; set; }
    public Plan Plan { get; set; } = null!;
}
