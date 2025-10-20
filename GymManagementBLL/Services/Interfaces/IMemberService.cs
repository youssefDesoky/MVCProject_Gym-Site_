using System;
using GymManagementBLL.ViewModels.MemberViewModels;

namespace GymManagementBLL.Services.Interfaces;

public interface IMemberService
{
    IEnumerable<MemberViewModel> GetAllMembers();
    bool CreateMember(CreateMemberViewModel model);
    MemberDetailsViewModel? GetMemberDetails(int memberId);
    MemberHealthDetailsViewModel? GetMemberHealthDetails(int memberId);
    bool UpdateMember(int memberId, UpdateMemberViewModel model);
    UpdateMemberViewModel? GetMemberToUpdate(int memberId);
    bool RemoveMember(int memberId);
}
