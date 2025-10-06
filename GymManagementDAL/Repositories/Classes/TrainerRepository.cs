using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes;

public class TrainerRepository : ITrainerRepository
{

    private readonly GymContext _context;

    public TrainerRepository(GymContext context)
    {
        _context = context;
    }

    public int Create(Trainer entity)
    {
        _context.Trainers.Add(entity);
        return _context.SaveChanges();
    }

    public int Delete(int id)
    {
        var trainer = _context.Trainers.Find(id);
        if (trainer is null)
            return 0;

        _context.Trainers.Remove(trainer);
        return _context.SaveChanges();
    }

    public int Update(Trainer entity)
    {
        _context.Trainers.Update(entity);
        return _context.SaveChanges();
    }

    public IEnumerable<Trainer> GetAll() => _context.Trainers.ToList();

    public Trainer? GetById(int id) => _context.Trainers.Find(id);
}
