using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagementDAL.Entities;

namespace GymManagementBLL.Services.Classes;

public class TrainerService : ITrainerService
{
    private readonly IUnitOfWork _unitOfWork;

    public TrainerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public bool CreateTrainer(CreateTrainerViewModel model)
    {
        try
        {
            if (IsValueExist(x => x.Email == model.Email || x.Phone == model.Phone))
                return false;

            var trainer = new Trainer
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                Gender = model.Gender,
                Address = new Address
                {
                    BuildingNumber = model.BuildingNumber,
                    City = model.City,
                    Street = model.Street
                },
                Specialties = model.Specialization
            };

            _unitOfWork.GetRepository<Trainer>().Create(trainer);

            return _unitOfWork.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<TrainerViewModel> GetAllTrainers()
    {
        var trainer = _unitOfWork.GetRepository<Trainer>().GetAll() ?? [];

        if (trainer is null || !trainer.Any())
            return [];

        return trainer.Select(x => new TrainerViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Phone = x.Phone,
            Email = x.Email,
            Specialization = x.Specialties.ToString()
        });
    }

    public TrainerDetailViewModel? GetTrainerDetails(int trainerId)
    {
        var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);

        if (trainer is null)
            return null;

        return new TrainerDetailViewModel
        {
            Name = trainer.Name,
            Phone = trainer.Phone,
            Email = trainer.Email,
            Gender = trainer.Gender.ToString(),
            Address = FormatAddress(trainer.Address),
            DateOfBirth = trainer.DateOfBirth.ToShortDateString()
        };
    }

    public bool RemoveTrainer(int trainerId)
    {
        var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);

        if (trainer is not null)
        {
            var sessions = _unitOfWork.GetRepository<Session>().GetAll().Where(s => s.TrainerId == trainerId).ToList();

            if (!sessions.Any())
            {
                _unitOfWork.GetRepository<Trainer>().Delete(trainer);

                return _unitOfWork.SaveChanges() > 0;
            }
        }
        
        return false;
    }

    public bool UpdateTrainer(int trainerId, UpdateTrainerViewModel model)
    {
        var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);

        if (trainer is null)
            return false;

        if (IsValueExist(x => (x.Email == model.Email || x.Phone == model.Phone) && x.Id != trainerId))
            return false;

        (trainer.Name, trainer.Email, trainer.Phone, trainer.Specialties, trainer.UpdatedAt)
        = (model.Name, model.Email, model.Phone, model.Specialization, DateTime.Now);

        _unitOfWork.GetRepository<Trainer>().Update(trainer);

        return _unitOfWork.SaveChanges() > 0;
    }

    public UpdateTrainerViewModel? GetTrainerToUpdate(int trainerId)
    {
        var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);

        if (trainer is null)
            return null;

        return new UpdateTrainerViewModel
        {
            Name = trainer.Name,
            Email = trainer.Email,
            Phone = trainer.Phone,
            BuildingNumber = trainer.Address.BuildingNumber,
            Street = trainer.Address.Street,
            City = trainer.Address.City,
            Specialization = trainer.Specialties
        };
    }

    #region Helper Methods
    private string FormatAddress(Address address)
    {
        if (address is null)
            return "N/A";

        return $"{address.BuildingNumber}, {address.Street}, {address.City}";
    }

    private bool IsValueExist(Func<Member, bool> condition) => _unitOfWork.GetRepository<Member>().GetAll(condition).Any();

    #endregion
}
