using System;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModels;
using GymManagementDAL.Entities;

namespace GymManagementBLL.Services.Classes;

public class PlanService : IPlanService
{
    private readonly IUnitOfWork _unitOfWork;

    public PlanService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public bool EditPlan(int planId, UpdatePlanViewModel model)
    {
        var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
        if (plan is null)
            return false;

        if (HasActiveMembers(planId))
            return false;

        (plan.Name, plan.Description, plan.DurationDays, plan.UpdatedAt) = (model.PlanName, model.Description, model.DurationDays, DateTime.Now);

        _unitOfWork.GetRepository<Plan>().Update(plan);

        return _unitOfWork.SaveChanges() > 0;
    }

    public IEnumerable<PlanViewModel> GetAllPlans()
    {
        var plans = _unitOfWork.GetRepository<Plan>().GetAll() ?? [];

        if (plans is null || !plans.Any())
            return [];

        return plans.Select(p => new PlanViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            DurationDays = p.DurationDays,
            Price = p.Price,
            IsActive = p.IsActive
        }).ToList();
    }

    public PlanViewModel? GetPlanDetails(int planId)
    {
        var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
        if (plan is null)
            return null;

        return new PlanViewModel
        {
            Id = plan.Id,
            Name = plan.Name,
            Description = plan.Description,
            DurationDays = plan.DurationDays,
            Price = plan.Price,
            IsActive = plan.IsActive
        };
    }

    public bool ToggleActivePlanStatus(int planId)
    {
        var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
        if (plan is null)
            return false;

        if (HasActiveMembers(planId))
            return false;

        plan.IsActive = !plan.IsActive;

        _unitOfWork.GetRepository<Plan>().Update(plan);

        return _unitOfWork.SaveChanges() > 0;
    }

    #region Helper Methods
    
    bool HasActiveMembers(int planId) => _unitOfWork.GetRepository<Membership>().GetAll().Any(ms => ms.PlanId == planId && ms.Status == "Active");

    #endregion
}
