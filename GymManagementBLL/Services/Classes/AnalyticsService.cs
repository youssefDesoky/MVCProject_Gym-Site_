using System;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.AnalyticsViewModels;
using GymManagementDAL.Entities;

namespace GymManagementBLL.Services.Classes;

public class AnalyticsService : IAnalyticsService
{
    private readonly IUnitOfWork _unitOfWork;
    public AnalyticsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public AnalyticsViewModel GetAnalytics() => new AnalyticsViewModel
        {
            TotalMembers = _unitOfWork.GetRepository<Member>().GetAll().Count(),
            ActiveMembers = _unitOfWork.GetRepository<Membership>().GetAll(x => x.Status == "Active").Count(),
            TotalTrainers = _unitOfWork.GetRepository<Trainer>().GetAll().Count(),
            UpcomingSessions = _unitOfWork.GetRepository<Session>().GetAll(x => x.StartDate > DateTime.Now).Count(),
            OngoingSessions = _unitOfWork.GetRepository<Session>().GetAll(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now).Count(),
            CompletedSessions = _unitOfWork.GetRepository<Session>().GetAll(x => x.EndDate < DateTime.Now).Count()
        };
}
