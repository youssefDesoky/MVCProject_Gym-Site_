using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes;

public class SessionRepository : MainCRUDRepository<Session>, ISessionRepository
{
    public SessionRepository(GymContext context) : base(context) { }
}