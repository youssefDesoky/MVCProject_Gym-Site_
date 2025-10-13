using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementDAL.Repositories.Classes;

public class MemberRepository : MainCRUDRepository<Member>, IMemberRepository
{
    public MemberRepository(GymContext context) : base(context) {}

    public IEnumerable<Session> GetMemberSessions(int memberId)
    {
        throw new NotImplementedException();
    }
}
