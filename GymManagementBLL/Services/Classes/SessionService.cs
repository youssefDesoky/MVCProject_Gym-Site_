using System;
using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entities;

namespace GymManagementBLL.Services.Classes;

public class SessionService : ISessionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public bool CreateSession(CreateSessionViewModel model)
    {
        try
        {
            if (!IsTrainerExists(model.TrainerId))
                return false;

            if (!IsCategoryExists(model.CategoryId))
                return false;

            if (!IsAvailableDateRange(model.StartDate, model.EndDate))
                return false;

            var session = _mapper.Map<CreateSessionViewModel, Session>(model); // Equivalent to _mapper.Map<Session>(model);

            _unitOfWork.GetRepository<Session>().Create(session);

            return _unitOfWork.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<SessionViewModel> GetAllSessions()
    {
        var sessions = _unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory().OrderByDescending(x => x.StartDate);

        if (sessions is null || !sessions.Any())
            return [];

        var mappedSessions = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(sessions); // Equivalent to _mapper.Map<IEnumerable<SessionViewModel>>(sessions);

        mappedSessions
            .ToList()
            .ForEach(session =>
            {
                session.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository
                                                            .GetCountOfBookedSlots(session.Id);
            });

        return mappedSessions;
    }

    public SessionViewModel? GetSessionDetails(int sessionId)
    {
        var session = _unitOfWork.SessionRepository.GetSessionWithTrainerAndCategory(sessionId);
        if (session is null)
            return null;

        var mappedSession = _mapper.Map<Session, SessionViewModel>(session); // Equivalent to _mapper.Map<SessionViewModel>(session);
        mappedSession.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id);

        return mappedSession;
    }

    public UpdateSessionViewModel? GetSessionForUpdate(int sessionId)
    {
        var session = _unitOfWork.GetRepository<Session>().GetById(sessionId);
        if (session is null)
            return null;

        var mappedSession = _mapper.Map<Session, UpdateSessionViewModel>(session); // Equivalent to _mapper.Map<UpdateSessionViewModel>(session);
        return mappedSession;
    }

    public bool UpdateSession(int sessionId, UpdateSessionViewModel model)
    {
        var session = _unitOfWork.GetRepository<Session>().GetById(sessionId);
        if (session is null)
            return false;

        if (!IsSessionAvailableForUpdate(sessionId))
            return false;
        if (!IsTrainerExists(model.TrainerId))
            return false;
        if (!IsAvailableDateRange(model.StartDate, model.EndDate))
            return false;

        _mapper.Map<UpdateSessionViewModel, Session>(model); // Equivalent to _mapper.Map<Session>(model);
        session.UpdatedAt = DateTime.Now;

        _unitOfWork.GetRepository<Session>().Update(session);

        return _unitOfWork.SaveChanges() > 0;
    }

    public bool RemoveSession(int sessionId)
    {
        var session = _unitOfWork.GetRepository<Session>().GetById(sessionId);
        if (session is null)
            return false;

        if (!IsSessionAvailableForRemoval(sessionId))
            return false;

        _unitOfWork.GetRepository<Session>().Delete(session);

        return _unitOfWork.SaveChanges() > 0;
    }

    #region Helper Methods
    bool IsTrainerExists(int trainerId)
    {
        var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
        return trainer is not null;
    }

    bool IsCategoryExists(int categoryId)
    {
        var category = _unitOfWork.GetRepository<Category>().GetById(categoryId);
        return category is not null;
    }

    private bool IsAvailableDateRange(DateTime startDate, DateTime endDate) => startDate < endDate && startDate > DateTime.Now;

    private bool IsSessionAvailableForUpdate(int sessionId)
    {
        var session = _unitOfWork.GetRepository<Session>().GetById(sessionId);
        if (session is null)
            return false;

        if (session.EndDate <= DateTime.Now)
            return false;

        if (session.StartDate <= DateTime.Now)
            return false;

        if (_unitOfWork.SessionRepository.GetCountOfBookedSlots(sessionId) > 0)
            return false;

        return true;
    }

    public IEnumerable<CategorySelectViewModel> GetCategoriesDropDown()
    {
        var categories = _unitOfWork.GetRepository<Category>().GetAll();

        return _mapper.Map<IEnumerable<CategorySelectViewModel>>(categories);
    }

    public IEnumerable<TrainerSelectViewModel> GetTrainersDropDown()
    {
        var trainers = _unitOfWork.GetRepository<Trainer>().GetAll();

        return _mapper.Map<IEnumerable<TrainerSelectViewModel>>(trainers);
    }

    private bool IsSessionAvailableForRemoval(int sessionId)
    {
        var session = _unitOfWork.GetRepository<Session>().GetById(sessionId);
        if (session is null)
            return false;

        if (session.EndDate <= DateTime.Now)
            return false;

        if (session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now)
            return false;

        if (_unitOfWork.SessionRepository.GetCountOfBookedSlots(sessionId) > 0)
            return false;

        return true;
    }
    #endregion
}
