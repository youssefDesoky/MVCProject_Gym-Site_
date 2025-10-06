using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes;

public class MembershipRepository : IMembershipRepository
{
    private readonly GymContext _context;

    public MembershipRepository(GymContext context)
    {
        _context = context;
    }

    public int Create(Membership entity)
    {
        _context.Memberships.Add(entity);
        return _context.SaveChanges();
    }

    public int Delete(int id)
    {
        var membership = _context.Memberships.Find(id);
        if (membership is null)
            return 0;

        _context.Memberships.Remove(membership);
        return _context.SaveChanges();
    }

    public int Update(Membership entity)
    {
        _context.Memberships.Update(entity);
        return _context.SaveChanges();
    }

    public IEnumerable<Membership> GetAll() => _context.Memberships.ToList();

    public Membership? GetById(int id) => _context.Memberships.Find(id);
}
