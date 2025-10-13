using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entities;

namespace GymManagementBLL.Services.Classes;

public class MemberServices : IMemberService
{
    private readonly IUnitOfWork _unitOfWork;

    public MemberServices(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public bool CreateMember(CreateMemberViewModel model)
    {
        try
        {
            if (IsEmailExist(model.Email)) 
                return false;
            if (IsPhoneNumberExist(model.Phone)) 
                return false;

            var member = new Member
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Address = new Address
                {
                    BuildingNumber = model.BuildingNumber,
                    City = model.City,
                    Street = model.Street
                },
                HealthRecord = new HealthRecord
                {
                    Weight = model.HealthRecordViewModel.Weight,
                    Height = model.HealthRecordViewModel.Height,
                    BloodType = model.HealthRecordViewModel.BloodType,
                    Note = model.HealthRecordViewModel.Note
                }
            };

            _unitOfWork.GetRepository<Member>().Create(member);

            return _unitOfWork.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<MemberViewModel> GetAllMembers()
    {
        var members = _unitOfWork.GetRepository<Member>().GetAll() ?? [];

        if (members is null || !members.Any())
            return [];

        return members.Select(x => new MemberViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Phone = x.Phone,
            Email = x.Email,
            Photo = x.Photo,
            Gender = x.Gender.ToString()
        });
    }

    public MemberDetailsViewModel? GetMemberDetails(int memberId)
    {
        var member = _unitOfWork.GetRepository<Member>().GetById(memberId);

        if (member is not null)
        {
            var activeMembership = _unitOfWork.GetRepository<Membership>()
                                   .GetAll(x => x.MemberId == memberId && x.Status == "Active")
                                   .FirstOrDefault();

            if (activeMembership is not null)
            {
                var plan = _unitOfWork.GetRepository<Plan>().GetById(activeMembership.PlanId);

                if (plan is not null)
                {
                    return new MemberDetailsViewModel
                    {
                        Photo = member.Photo,
                        Name = member.Name,
                        Phone = member.Phone,
                        Email = member.Email,
                        Gender = member.Gender.ToString(),
                        Address = FormatAddress(member.Address),
                        DateOfBirth = member.DateOfBirth.ToShortDateString(),
                        PlanName = plan.Name,
                        MemberShipStartDate = activeMembership.CreatedAt.ToShortDateString(),
                        MemberShipEndDate = activeMembership.EndDate.ToShortDateString(),
                    };
                }
            }
        }

        return null;
    }

    public MemberHealthDetailsViewModel? GetMemberHealthDetails(int memberId)
    {
        var healthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(memberId);

        if (healthRecord is null)
            return null;

        return new MemberHealthDetailsViewModel
        {
            Weight = healthRecord.Weight,
            Height = healthRecord.Height,
            BloodType = healthRecord.BloodType,
            Note = healthRecord.Note
        };
    }

    public bool RemoveMember(int memberId)
    {
        var member = _unitOfWork.GetRepository<Member>().GetById(memberId);

        if (member is null)
            return false;

        var activeBookings = _unitOfWork.GetRepository<Booking>().GetAll(x => x.MemberId == memberId && x.Session.StartDate > DateTime.Now);

        if (activeBookings.Any())
            return false;

        var memberships = _unitOfWork.GetRepository<Membership>().GetAll(x => x.MemberId == memberId).ToList();

        try
        {
            if (memberships.Any())
            {
                foreach (var membership in memberships)
                {
                    _unitOfWork.GetRepository<Membership>().Delete(membership);
                }

            }

            _unitOfWork.GetRepository<Member>().Delete(member);

            return _unitOfWork.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool UpdateMember(int memberId, UpdateMemberViewModel model)
    {
        var member = _unitOfWork.GetRepository<Member>().GetById(memberId);

        if (member is null)
            return false;

        if (IsEmailExist(model.Email)) 
            return false;
        if (IsPhoneNumberExist(model.Phone))
            return false;

        (member.Email, member.Phone, member.Address.BuildingNumber, member.Address.Street, member.Address.City, member.UpdatedAt)
        = (model.Email, model.Phone, model.BuildingNumber, model.Street, model.City, DateTime.Now);

        _unitOfWork.GetRepository<Member>().Update(member);

        return _unitOfWork.SaveChanges() > 0;
    }

    #region Helper Methods

    private string FormatAddress(Address address)
    {
        if (address is null)
            return "N/A";

        return $"{address.BuildingNumber}, {address.Street}, {address.City}";
    }

    private bool IsEmailExist(string email) => _unitOfWork.GetRepository<Member>().GetAll().Any(x => x.Email == email);

    private bool IsPhoneNumberExist(string phone) => _unitOfWork.GetRepository<Member>().GetAll().Any(x => x.Phone == phone);

    #endregion
}
