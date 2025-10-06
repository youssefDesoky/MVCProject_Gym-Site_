using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes;

public class HealthRecordRepository : IHealthRecordRepository
{
    private readonly GymContext _context;

    public HealthRecordRepository(GymContext context)
    {
        _context = context;
    }

    public int Create(HealthRecord entity)
    {
        _context.HealthRecords.Add(entity);
        return _context.SaveChanges();
    }

    public int Delete(int id)
    {
        var healthRecord = _context.HealthRecords.Find(id);
        if (healthRecord is null)
            return 0;

        _context.HealthRecords.Remove(healthRecord);
        return _context.SaveChanges();
    }

    public int Update(HealthRecord entity)
    {
        _context.HealthRecords.Update(entity);
        return _context.SaveChanges();
    }

    public IEnumerable<HealthRecord> GetAll() => _context.HealthRecords.ToList();

    public HealthRecord? GetById(int id) => _context.HealthRecords.Find(id);
}
