using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces;

public interface IMemberRepository : IMainCRUDRepository<Member>
{
    IEnumerable<Session> GetMemberSessions(int memberId);
}
