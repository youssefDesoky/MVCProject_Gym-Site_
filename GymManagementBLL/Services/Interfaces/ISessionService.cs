using System;
using GymManagementBLL.ViewModels.SessionViewModels;

namespace GymManagementBLL.Services.Interfaces;

public interface ISessionService
{
    IEnumerable<SessionViewModel> GetAllSessions();
    SessionViewModel? GetSessionDetails(int sessionId);
    bool CreateSession(CreateSessionViewModel model);
    bool UpdateSession(int sessionId, UpdateSessionViewModel model);
    UpdateSessionViewModel? GetSessionForUpdate(int sessionId);
    bool RemoveSession(int sessionId);
    IEnumerable<CategorySelectViewModel> GetCategoriesDropDown();
    IEnumerable<TrainerSelectViewModel> GetTrainersDropDown();
}
