using System;

namespace GymManagementBLL.ViewModels.MemberViewModels;

public class MemberDetailsViewModel : MemberViewModel
{
    public string Address { get; set; } = null!;
    public string PlanName { get; set; } = null!;
    public string MemberShipStartDate { get; set; } = null!;
    public string MemberShipEndDate { get; set; } = null!;
}
