using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes;

public class TrainerRepository : MainCRUDRepository<Trainer>, ITrainerRepository
{
    public TrainerRepository(GymContext context) : base(context) { }
}
