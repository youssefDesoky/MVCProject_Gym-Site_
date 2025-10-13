using System;
using GymManagementBLL.ViewModels.PlanViewModels;

namespace GymManagementBLL.Services.Interfaces;

public interface IPlanService
{
    IEnumerable<PlanViewModel> GetAllPlans();
    PlanViewModel? GetPlanDetails(int planId);
    bool ToggleActivePlanStatus(int planId);
    bool EditPlan(int planId, UpdatePlanViewModel model);
}
