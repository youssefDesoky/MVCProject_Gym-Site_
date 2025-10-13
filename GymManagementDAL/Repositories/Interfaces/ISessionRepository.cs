using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces;

public interface ISessionRepository : IMainCRUDRepository<Session>
{
    IEnumerable<Session> GetAllSessionsWithTrainerAndCategory();
    Session? GetSessionWithTrainerAndCategory(int sessionId);
    int GetCountOfBookedSlots(int sessionId);
}
