using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes;

public class PlanRepository : MainCRUDRepository<Plan>, IPlanRepository
{
    public PlanRepository(GymContext context) : base(context) { }
}