using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes;

public class HealthRecordRepository : MainCRUDRepository<HealthRecord>, IHealthRecordRepository
{
    public HealthRecordRepository(GymContext context) : base(context) {}
}