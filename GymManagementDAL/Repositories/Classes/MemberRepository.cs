using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes;

public class MemberRepository : IMemberRepository
{
    private readonly GymContext _context;

    public MemberRepository(GymContext context)
    {
        _context = context;
    }

    public int Create(Member entity)
    {
        _context.Members.Add(entity);
        return _context.SaveChanges();
    }

    public int Delete(int id)
    {
        var member = _context.Members.Find(id);
        if (member is null)
            return 0;

        _context.Members.Remove(member);
        return _context.SaveChanges();
    }

    public int Update(Member entity)
    {
        _context.Members.Update(entity);
        return _context.SaveChanges();
    }

    public IEnumerable<Member> GetAll() => _context.Members.ToList();

    public Member? GetById(int id) => _context.Members.Find(id);
}
