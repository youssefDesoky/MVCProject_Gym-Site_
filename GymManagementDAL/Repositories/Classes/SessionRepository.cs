using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes;

public class SessionRepository : ISessionRepository
{
    private readonly GymContext _context;

    public SessionRepository(GymContext context)
    {
        _context = context;
    }

    public int Create(Session entity)
    {
        _context.Sessions.Add(entity);
        return _context.SaveChanges();
    }

    public int Delete(int id)
    {
        var session = _context.Sessions.Find(id);
        if (session is null)
            return 0;

        _context.Sessions.Remove(session);
        return _context.SaveChanges();
    }

    public int Update(Session entity)
    {
        _context.Sessions.Update(entity);
        return _context.SaveChanges();
    }

    public IEnumerable<Session> GetAll() => _context.Sessions.ToList();

    public Session? GetById(int id) => _context.Sessions.Find(id);
}
