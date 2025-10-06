using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes;

public class PlanRepository : IPlanRepository
{
    private readonly GymContext _context;

    public PlanRepository(GymContext context)
    {
        _context = context;
    }

    public int Create(Plan entity)
    {
        _context.Plans.Add(entity);
        return _context.SaveChanges();
    }

    public int Delete(int id)
    {
        var plan = _context.Plans.Find(id);
        if (plan is null)
            return 0;

        _context.Plans.Remove(plan);
        return _context.SaveChanges();
    }

    public int Update(Plan entity)
    {
        _context.Plans.Update(entity);
        return _context.SaveChanges();
    }

    public IEnumerable<Plan> GetAll() => _context.Plans.ToList();

    public Plan? GetById(int id) => _context.Plans.Find(id);
}
