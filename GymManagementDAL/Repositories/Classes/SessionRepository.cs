using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Repositories.Classes;

public class SessionRepository : MainCRUDRepository<Session>, ISessionRepository
{
    private readonly GymContext _context;

    public SessionRepository(GymContext context) : base(context)
    {
        _context = context;
    }


    public IEnumerable<Session> GetAllSessionsWithTrainerAndCategory()
    {
        return _context.Sessions
            .Include(s => s.Trainer)
            .Include(s => s.Category)
            .ToList();
    }

    public int GetCountOfBookedSlots(int sessionId)
    {
        return _context.Bookings.Count(b => b.SessionId == sessionId);
    }

    public Session? GetSessionWithTrainerAndCategory(int sessionId)
    {
        return _context.Sessions
            .Include(s => s.Trainer)
            .Include(s => s.Category)
            .FirstOrDefault(s => s.Id == sessionId);
    }
}