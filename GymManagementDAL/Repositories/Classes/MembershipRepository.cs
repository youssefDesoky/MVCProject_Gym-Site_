using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes;

public class MembershipRepository : MainCRUDRepository<Membership>, IMembershipRepository
{
    public MembershipRepository(GymContext context) : base(context) {}
}